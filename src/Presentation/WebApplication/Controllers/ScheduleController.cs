using System.Threading;
using System.Threading.Tasks;
using Framework.Controller.Extensions;
using Framework.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Commands;
using ScheduleManagement.Query.Commands;
using ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate;
using ScheduleManagement.Query.Handlers;

namespace WebApplication.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
    public class ScheduleController : BaseController
    {
        private readonly IEventBus _eventBus;
        private readonly IGetCurrentUserScheduleQueryHandler _getCurrentUserScheduleQueryHandler;
        private readonly IGetAllUserScheduleQueryHandler _getAllUserScheduleQueryHandler;

        public ScheduleController(IEventBus eventBus,
            IGetCurrentUserScheduleQueryHandler getCurrentUserScheduleQueryHandler, IGetAllUserScheduleQueryHandler getAllUserScheduleQueryHandler)
        {
            _eventBus = eventBus;
            _getCurrentUserScheduleQueryHandler = getCurrentUserScheduleQueryHandler;
            _getAllUserScheduleQueryHandler = getAllUserScheduleQueryHandler;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddBookingDateTimeCommandRequest request)
        {
            var result = _eventBus.Issue(request);
            return Ok(await result);
        }

        [HttpGet("current-user-schedule")]
        [Authorize]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result =
                _getCurrentUserScheduleQueryHandler.Handle(
                    new GetCurrentInterviewerCandidateScheduleQueryRequest(),cancellationToken);
            return Ok(await result);
        }
        
        [HttpGet("interview-schedule")]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result =
                _getAllUserScheduleQueryHandler.Handle(
                    new GetAllUserScheduleQueryRequest(),cancellationToken);
            return Ok(await result);
        }
    }
}