using System.Threading;
using System.Threading.Tasks;
using Framework.Controller.Extensions;
using Framework.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.AdminUserQuery;
using Service.Query.Model.AdminUserQuery;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Commands.CustomerUserCommands;

namespace WebApplication.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IEventBus _eventBus;
        private readonly IGetAllAdminUsersQueryHandler _getAllAdminUsersQueryHandler;


        public UserController(IEventBus eventBus, IGetAllAdminUsersQueryHandler getAllAdminUsersQueryHandler)
        {
            _eventBus = eventBus;
            _getAllAdminUsersQueryHandler = getAllAdminUsersQueryHandler;
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
            var data = _getAllAdminUsersQueryHandler.Handle(new GetAllAdminUserQueryRequest(), cancellationToken);
            return Ok(await data);
        }
    }
}