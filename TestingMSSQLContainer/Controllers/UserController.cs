using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingMSSQLContainer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly MyDbContext _dbContext;
		public UserController(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult Get() 
		{
			var users = _dbContext.Users.ToList();
			return Ok(users);
		}
	}
}
