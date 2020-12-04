using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using hello.transaction.core.Services;
using hello.transaction.core.Models;

namespace hello.transaction.api.Controllers
{
    //[Authorize]
    //[ServiceFilter(typeof(EnsureUserAuthorizeInAsync))]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //private readonly ICacheService _cacheService;
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService) //, ICacheService cacheService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            //_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }


        //
        // Summary:
        //      return basic list of transction
        //
        // Returns:
        //     list of transction
        //
        [EnableCors("AllowCors")]
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListAsync()
        {
            var entities = await _transactionService.ListAsync();
            return Ok(entities);
        }

        [EnableCors("AllowCors")]
        [Route("list/currency")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListByCurrencyAsync(string code)
        {
            var entities = await _transactionService.ListByCurrencyAsync(code);
            return Ok(entities);
        }

        [EnableCors("AllowCors")]
        [Route("list/status")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListByStatusAsync(string type)
        {
            var entities = await _transactionService.ListByStatusAsync(type);
            return Ok(entities);
        }

        [EnableCors("AllowCors")]
        [Route("list/date")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListByDateAsync(DateTime from, DateTime to)
        {
            var entities = await _transactionService.ListByDateAsync(from, to);
            return Ok(entities);
        }

    }
}