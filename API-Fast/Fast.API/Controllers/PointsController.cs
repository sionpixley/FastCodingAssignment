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

		[HttpGet("User/GetForPastNMonths/{numOfMonths}")]
		public async Task<ActionResult<IEnumerable<UserPoints>>> GetUserPointsForPastNMonths(int numOfMonths) {
			try {
				// I want to include the current month, so I subract 1 from the input.
				return Ok(await _Domain.GetUserPointsForPastNMonths(numOfMonths - 1));
			}
			catch(Exception e) {
				return StatusCode(500, e.Message);
			}
		}
	}
}
