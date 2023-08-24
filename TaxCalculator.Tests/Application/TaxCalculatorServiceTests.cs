using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Application.Services;
using TaxCalculator.Domain.Core.Repositories;

namespace TaxCalculator.Tests.Application
{
    [TestFixture]
    internal class TaxCalculatorServiceTests
    {
        #region Setup

        private Mock<ITaxTypeRepository> _taxTypeRepository;
        private ITaxCalculatorService _service;
        private CancellationToken cancellationToken = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            _taxTypeRepository = new Mock<ITaxTypeRepository>();
            _service = new TaxCalculatorService(_taxTypeRepository.Object);
        }

        #endregion


        #region Calculate Tax

        [Test]
        public async Task CalculateTax_ShouldFail_InvalidTaxType()
        {
            // Arrange
            _taxTypeRepository.Setup(x => x.GetTaxType(It.IsAny<string>()))
                .Returns(() => null);

            // Act
            var result = await _service.CalculateTax(new CalculateTaxRequest(), cancellationToken);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsTrue(result.IsFailure);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        #endregion
    }
}
