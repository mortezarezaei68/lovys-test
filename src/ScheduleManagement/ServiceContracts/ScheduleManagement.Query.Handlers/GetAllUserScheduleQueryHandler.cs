using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Domain;
using ScheduleManagement.Persistence.EF.Context;
using ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate;
using UserManagement.Domain;

namespace ScheduleManagement.Query.Handlers
{
    public class GetAllUserScheduleQueryHandler : IGetAllUserScheduleQueryHandler
    {
        private readonly ScheduleManagementDbContext _context;

        public GetAllUserScheduleQueryHandler(ScheduleManagementDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllUserScheduleQueryResponse> Handle(GetAllUserScheduleQueryRequest request,
            CancellationToken cancellationToken)
        {

            var candidateBookingDateTime = await
                _context.BookingDateBookingTimeViews.Where(a => a.UserType == (int)UserType.Candidate)
                    .ToListAsync(cancellationToken: cancellationToken);

            var interviewBookingDateTime = await
                _context.BookingDateBookingTimeViews.Where(a => a.UserType == (int)UserType.Interviewer)
                    .ToListAsync(cancellationToken: cancellationToken);
            
            var objects = new List<InterviewerScheduleQueryModel>();
            foreach (var interviewBookingTime in interviewBookingDateTime)
            {
                var selectedCandidate =
                    candidateBookingDateTime.Where(a => a.DateOfBooking == interviewBookingTime.DateOfBooking &&
                                                        a.StartedBookingTime.IsOverlap(
                                                            interviewBookingTime.StartedBookingTime,
                                                            interviewBookingTime.EndedBookingTime) &&
                                                        a.EndedBookingTime.IsOverlap(
                                                            interviewBookingTime.StartedBookingTime,
                                                            interviewBookingTime.EndedBookingTime));
                objects.Add(new InterviewerScheduleQueryModel
                {
                    CandidateSchedule = selectedCandidate.Select(a => new CandidateScheduleQueryModel
                    {
                        EndedDate = a.EndedBookingTime,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        StartedDate = a.StartedBookingTime
                    }),
                    EndedDate = interviewBookingTime.EndedBookingTime,
                    FirstName = interviewBookingTime.FirstName,
                    LastName = interviewBookingTime.LastName,
                    StartedDate = interviewBookingTime.StartedBookingTime,
                    BookingDate = interviewBookingTime.DateOfBooking
                });
            }

            var object2 = new List<object>();
            foreach (var interviewBookingTime in objects)
            {
                var selectedCandidate =
                    objects.Where(a => a.BookingDate == interviewBookingTime.BookingDate &&
                                                        a.StartedDate.IsOverlap(
                                                            interviewBookingTime.StartedDate,
                                                            interviewBookingTime.EndedDate) &&
                                                        a.EndedDate.IsOverlap(
                                                            interviewBookingTime.StartedDate,
                                                            interviewBookingTime.EndedDate)).ToList();
                object2.AddRange(selectedCandidate);
            }

            return new GetAllUserScheduleQueryResponse(true, objects);
        }
    }
}