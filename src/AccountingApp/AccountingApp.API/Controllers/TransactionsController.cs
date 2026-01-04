using AccountingApp.Core.DTOS;
using AccountingApp.Core.Exceptions;
using AccountingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _transactionService.GetByIdAsync(id);
                return Ok(transaction);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(id);
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

        [HttpPost("income")]
        public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _transactionService.CreateIncomeAsync(dto);

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        [HttpPost("expense")]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transaction = await _transactionService.CreateExpenseAsync(dto);

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transaction = await _transactionService.CreateTransferAsync(dto);

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }
    }
}