using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Query;
using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Persistence.EF.Context;
using ScheduleManagement.Query.Commands;
using Service.Query.AdminUserQuery;
using Service.Query.Model.AdminUserQuery;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domain;

namespace ScheduleManagement.Query.Handlers
{
    public class GetCurrentUserScheduleQueryHandler : IGetCurrentUserScheduleQueryHandler
    {
        private readonly ScheduleManagementDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserScheduleQueryHandler(
            ScheduleManagementDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        public async Task<GetCurrentInterviewerCandidateScheduleQueryResponse> Handle(
            GetCurrentInterviewerCandidateScheduleQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var data = _context.BookingDateBookingTimeViews
                .Where(a => a.SubjectId.ToString() == userId)
                .GroupBy(a => new { a.SubjectId, a.FirstName, a.LastName, a.DateOfBooking })
                .ToListAsync(cancellationToken: cancellationToken);

            var result = (await data).Select(a => new ScheduleQueryModel
            {
                BookingDate = a.Key.DateOfBooking,
                FirstName = a.Key.FirstName,
                LastName = a.Key.LastName,
                SubSchedules = a.Select(a => new SubScheduleQueryModel
                {
                    EndedDate = a.EndedBookingTime,
                    StartedDate = a.StartedBookingTime,
                })
            });
            return new GetCurrentInterviewerCandidateScheduleQueryResponse(true, result);
        }
    }
}