using System.Threading.Tasks;
using Framework.Controller.Extensions;
using Framework.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Commands.CustomerUserCommands;

namespace WebApplication.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController:BaseController
    {
        private readonly IEventBus _eventBus;

        public UserController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(AddCandidateCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPost("login-user")]
        public async Task<ActionResult> LoginUser(LoginCandidateUserCommandRequest command)
        {
            var data=await _eventBus.Issue(command);
            return Ok(data);
        }
    }
}