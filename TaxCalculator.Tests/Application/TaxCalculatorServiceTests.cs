using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private CancellationToken cancellationToken = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            _taxRepository = new Mock<ITaxRepository>();
            _service = new TaxCalculatorService(_taxRepository.Object);
        }

        #endregion


        #region Calculate Tax

        [Test]
        public async Task CalculateTax_ShouldFail_InvalidTaxType()
        {
            // Arrange
            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(() => null);

            // Act
            var result = await _service.CalculateTax(new CalculateTaxRequest(), cancellationToken);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsTrue(result.IsFailure);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        [Test]
        public async Task CalculateTax_ShouldFail_InvalidTaxRate()
        {
            // Arrange
            _taxRepository.Setup(x => x.GetTaxType(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(() => TaxType.Create("1000", TaxTypeEnum.Progressive));

            _taxRepository.Setup(x => x.GetTaxRateByIncome(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
                .Returns(() => null);

            // Act
            var result = await _service.CalculateTax(new CalculateTaxRequest(), cancellationToken);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsTrue(result.IsFailure);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        #endregion
    }
}
