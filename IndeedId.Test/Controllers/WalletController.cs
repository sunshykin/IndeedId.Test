using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndeedId.Test.Database;
using IndeedId.Test.Dto;
using IndeedId.Test.Models;
using IndeedId.Test.Services;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IndeedId.Test.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalletController : ControllerBase
	{
		private readonly IWalletService walletService;

		public WalletController(IWalletService walletService)
		{
			this.walletService = walletService;
		}


		[HttpGet("{userid}")]
		public async Task<FinancesViewModel> GetFinances([FromRoute] int userId)
		{
			return await walletService.GetUserFinances(userId);
		}

		[HttpPost("{userid}/deposit")]
		public async Task<OperationResult> Deposit([FromRoute] int userId, [FromBody] MoneyForm form)
		{
			if (form.Amount <= 0)
			{
				return new OperationResult(false, Constants.AmountShouldBePositive);
			}

			return await walletService.Deposit(userId, form.Amount, form.Currency);
		}

		[HttpPost("{userid}/withdraw")]
		public async Task<OperationResult> Withdraw([FromRoute] int userId, [FromBody] MoneyForm form)
		{
			if (form.Amount <= 0)
			{
				return new OperationResult(false, Constants.AmountShouldBePositive);
			}

			return await walletService.Withdraw(userId, form.Amount, form.Currency);
		}

		[HttpPost("{userid}/[action]")]
		public async Task<OperationResult> Convert([FromRoute] int userId, [FromBody] ConvertForm form)
		{
			if (form.Amount <= 0)
			{
				return new OperationResult(false, Constants.AmountShouldBePositive);
			}

			return await walletService.Convert(userId, form.Amount, form.FromCurrency, form.ToCurrency);
		}
	}
}
