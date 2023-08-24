using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Application.Services;

namespace TaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        #region Fields

        private readonly ITaxCalculatorService _taxCalculatorService;

        #endregion

        #region Constructors

        public TaxCalculatorController(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService ?? throw new ArgumentNullException(nameof(taxCalculatorService));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates tax based on provided income and postal code.
        /// </summary>
        /// <param name="request">The calculate tax request</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpPost]
        [Route("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest($"{nameof(request)} cannot be null");
            }

            var result = await _taxCalculatorService.CalculateTax(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
