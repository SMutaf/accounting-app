using AccountingApp.Core.DTOS;
using AccountingApp.Core.Exceptions;
using AccountingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetByIdAsync(id);
                return Ok(invoice);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return BadRequest($"VALIDATION HATASI: {errors}");
                }

                var createdInvoice = await _invoiceService.CreateAsync(invoiceDto);
                return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.Id }, createdInvoice);
            }
            catch (Exception ex)
            {
                string errorMessage = $"SUNUCU HATASI: {ex.Message}";

                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDETAY (Inner): {ex.InnerException.Message}";
                }

                if (ex.InnerException?.InnerException != null)
                {
                    errorMessage += $"\n\nDAHA DERİN DETAY: {ex.InnerException.InnerException.Message}";
                }

                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _invoiceService.UpdateAsync(id, invoiceDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                await _invoiceService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveInvoice(int id)
        {
            try
            {
                await _invoiceService.ApproveAsync(id);

                return Ok($"Invoice {id} approved successfully.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelInvoice(int id)
        {
            try
            {
                await _invoiceService.CancelAsync(id);
                return Ok($"Invoice {id} cancelled successfully.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}