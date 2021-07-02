using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fast.API.Services;
using Fast.API.Models;

namespace Fast.API.Controllers {
	[Route("Points")]
	[ApiController]
	public class PointsController : ControllerBase {
		private readonly Domain _Domain;

		public PointsController(Domain domain) => _Domain = domain;

		[HttpGet("GetForAmount/{amount}")]
		public async Task<ActionResult<int>> GetPointsForAmount(decimal amount) {
			try {
				return Ok(await _Domain.GetPointsForAmount(amount));
			}
			catch(Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("GetPastNMonthsByUser/{numOfMonths}")]
		public async Task<ActionResult<IEnumerable<UserPoints>>> GetPointsPastNMonthsByUser(int numOfMonths) {
			try {
				return Ok(await _Domain.GetPointsPastNMonthsByUser(numOfMonths));
			}
			catch(Exception e) {
				return StatusCode(500, e.Message);
			}
		}
	}
}
