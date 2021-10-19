using System.Threading;
using System.Threading.Tasks;
using Framework.Controller.Extensions;
using Framework.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.GetAllUserQuery;
using Service.Query.Model.GetAllUserQuery;
using UserManagement.Commands.CandidateUserCommands;

namespace WebApplication.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IEventBus _eventBus;
        private readonly IGetAllUsersQueryHandler _getAllUsersQueryHandler;


        public UserController(IEventBus eventBus, IGetAllUsersQueryHandler getAllUsersQueryHandler)
        {
            _eventBus = eventBus;
            _getAllUsersQueryHandler = getAllUsersQueryHandler;
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
            var data = await _eventBus.Issue(command);
            return Ok(data);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllAdminUser(CancellationToken cancellationToken)
        {
            var data = _getAllUsersQueryHandler.Handle(new GetAllUserQueryRequest(), cancellationToken);
            return Ok(await data);
        }
    }
}