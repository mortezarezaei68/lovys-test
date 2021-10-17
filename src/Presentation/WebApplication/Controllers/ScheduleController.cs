using System.Threading.Tasks;
using Framework.Controller.Extensions;
using Framework.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Commands;

namespace WebApplication.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
    public class ScheduleController:BaseController
    {
        private readonly IEventBus _eventBus;

        public ScheduleController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddBookingDateTimeCommandRequest request)
        {
            var result = _eventBus.Issue(request);
            return Ok(await result);
        }
    }
}