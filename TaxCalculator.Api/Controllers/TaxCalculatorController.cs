﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Dtos;
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
            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets tax information.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpGet]
        [Route("information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaxInformation(CancellationToken cancellationToken)
        {
            var result = await _taxCalculatorService.GetTaxInformation(cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all taxes calculated.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpGet]
        [Route("calculated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCalculatedTax(CancellationToken cancellationToken)
        {
            var result = await _taxCalculatorService.GetCalculatedTax(cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all postal codes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpGet]
        [Route("codes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPostalCodes(CancellationToken cancellationToken)
        {
            var result = await _taxCalculatorService.GetPostalCodes(cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all postal codes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpPut]
        [Route("tax/progressive/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProgressiveTax(int id, UpdateProgressiveTaxDto model, CancellationToken cancellationToken)
        {
            if (id != model.Id)
            {
                return BadRequest("Invalid Id provided. Model Id does not match path id.");
            }

            await _taxCalculatorService.UpdateProgressiveTax(model, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Gets all postal codes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpDelete]
        [Route("tax/progressive/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProgressiveTax(int id, CancellationToken cancellationToken)
        {
            await _taxCalculatorService.DeleteProgressiveTax(id, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Gets all postal codes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The calculated tax.</returns>
        [HttpDelete]
        [Route("calculated/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCalculatedTax(int id, CancellationToken cancellationToken)
        {
            await _taxCalculatorService.DeleteCalculatedTax(id, cancellationToken);
            return Ok();
        }

        #endregion
    }
}
