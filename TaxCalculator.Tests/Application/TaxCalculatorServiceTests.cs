using AutoMapper;
using Moq;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Application.Services;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;
using TaxCalculator.Domain.Core.Repositories;

namespace TaxCalculator.Tests.Application
{
    [TestFixture]
    internal class TaxCalculatorServiceTests
    {
        #region Setup

        private Mock<ITaxRepository> _taxRepository;
        private ITaxCalculatorService _service;
        private Mock<IMapper> _mapper;
        private readonly CancellationTokenSource cts = new();

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _taxRepository = new Mock<ITaxRepository>();
            _service = new TaxCalculatorService(_taxRepository.Object, _mapper.Object);
        }

        #endregion


        #region Calculate Tax

        [TestCase("Tax type not found.")]
        public async Task CalculateTax_ShouldFail_InvalidTaxType_Null(string errorMessage)
        {
            // Arrange
            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _service.CalculateTax(new CalculateTaxRequest(), cts.Token);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsTrue(result.IsFailure);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase("Flat Value tax rate not found.", TaxTypeEnum.FlatValue)]
        [TestCase("Flat Rate tax rate not found.", TaxTypeEnum.FlatRate)]
        [TestCase("Progressive tax rate not found.", TaxTypeEnum.Progressive)]
        public async Task CalculateTax_ShouldFail_InvalidTaxType_ProgressiveTax_NotFound(string errorMessage, TaxTypeEnum taxType)
        {
            // Arrange
            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => TaxType.Create("100A", taxType));

            // Act
            var result = await _service.CalculateTax(new CalculateTaxRequest(), cts.Token);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsTrue(result.IsFailure);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
        }

        [TestCase(100000, 5000, 95000)]
        [TestCase(200000, 10000, 190000)]
        public async Task CalculateTax_ShouldSucceed_FlatValueTax(decimal annualIncome, decimal taxAmount, decimal incomeAfterTax)
        {
            // Arrange
            var request = new CalculateTaxRequest
            {
                AnnualIncome = annualIncome,
                PostalCode = "A100"
            };

            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => TaxType.Create("A100", TaxTypeEnum.FlatValue));

            _taxRepository.Setup(x => x.GetFlatValueTax(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => FlatValueTax.Create(10000, 200000, 5));

            // Act
            var result = await _service.CalculateTax(request, cts.Token);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.IsFailure, Is.False);
                Assert.That(result.ErrorMessage, Is.Not.Null);
                Assert.That(result.Data, Is.Not.Null);
                Assert.That(result.Data.AnnualIncome, Is.EqualTo(request.AnnualIncome));
                Assert.That(result.Data.TaxAmount, Is.EqualTo(taxAmount));
                Assert.That(result.Data.IncomeAfterTax, Is.EqualTo(incomeAfterTax));
            });
        }

        [TestCase(100000, 17500, 82500)]
        [TestCase(200000, 35000, 165000)]
        public async Task CalculateTax_ShouldSucceed_FlatRateTax(decimal annualIncome, decimal taxAmount, decimal incomeAfterTax)
        {
            // Arrange
            var request = new CalculateTaxRequest
            {
                AnnualIncome = annualIncome,
                PostalCode = "7000"
            };

            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => TaxType.Create("7000", TaxTypeEnum.FlatRate));

            _taxRepository.Setup(x => x.GetFlatRateTax(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => FlatRateTax.Create(17.5M));

            // Act
            var result = await _service.CalculateTax(request, cts.Token);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.IsFailure, Is.False);
                Assert.That(result.ErrorMessage, Is.Not.Null);
                Assert.That(result.Data, Is.Not.Null);
                Assert.That(result.Data.AnnualIncome, Is.EqualTo(request.AnnualIncome));
                Assert.That(result.Data.TaxAmount, Is.EqualTo(taxAmount));
                Assert.That(result.Data.IncomeAfterTax, Is.EqualTo(incomeAfterTax));
            });
        }

        [TestCase(8350, 835, 7515)]
        [TestCase(33950, 4675, 29275)]
        [TestCase(82250, 15915, 66335)]
        [TestCase(171550, 39650, 131900)]
        [TestCase(372950, 99884.50, 273065.50)]
        [TestCase(372951, 99884.83, 273066.17)]
        public async Task CalculateTax_ShouldSucceed_ProgressiveTax(decimal annualIncome, decimal taxAmount, decimal incomeAfterTax)
        {
            // Arrange
            var request = new CalculateTaxRequest
            {
                AnnualIncome = annualIncome,
                PostalCode = "1000"
            };

            var progrssiveTaxRates = new List<ProgressiveTax>()
            {
                ProgressiveTax.Create(10M, 0M, 8350M, string.Empty),
                ProgressiveTax.Create(15M, 8351M, 33950M, "0 to 8 350 at 10%"),
                ProgressiveTax.Create(25M, 33951M, 82250M, "8 351 to 33 950 at 15%"),
                ProgressiveTax.Create(28M, 82251M, 171550M, "33 951 to 82 250 at 25%"),
                ProgressiveTax.Create(33M, 171551M, 372950M, "82 251 to 171 550 at 28%"),
                ProgressiveTax.Create(35M, 372951M, 0M, "171 551 to 372 950 at 33%")
            };

            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => TaxType.Create("1000", TaxTypeEnum.Progressive));

            _taxRepository.Setup(x => x.GetProgressiveTaxRates(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => progrssiveTaxRates);

            // Act
            var result = await _service.CalculateTax(request, cts.Token);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.IsFailure, Is.False);
                Assert.That(result.ErrorMessage, Is.Not.Null);
                Assert.That(result.Data, Is.Not.Null);
                Assert.That(result.Data.AnnualIncome, Is.EqualTo(request.AnnualIncome));
                Assert.That(result.Data.TaxAmount, Is.EqualTo(taxAmount));
                Assert.That(result.Data.IncomeAfterTax, Is.EqualTo(incomeAfterTax));
            });
        }

        #endregion
    }
}
