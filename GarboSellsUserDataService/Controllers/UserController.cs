using System.Linq;
using GarboSellsUserDataService.Database;
using GarboSellsUserDataService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GarboSellsUserDataService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private IConfiguration configuration;
    private readonly UserDataContext context;

    public UserController(IConfiguration configuration, UserDataContext context)
    {
      this.configuration = configuration;
      this.context = context;
    }

    [HttpGet]
    [Route("GetUserByUserLoginId")]
    public ActionResult<User> GetUserByUserLoginId(string userLoginId)
    {
      var userDto = context.Users.FirstOrDefault(u => u.UserLoginId == userLoginId);
      return userDto.ToUser();
    }

    [HttpGet("GetUserByUserId")]
    public ActionResult<User> GetUserByUserId(long userId)
    {
      var userDto = context.Users.FirstOrDefault(u => u.UserId == userId);
      return userDto.ToUser();
    }
  }
}
