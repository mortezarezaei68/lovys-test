using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var interviewerBookingDateTime = await
                _context.BookingDateBookingTimeViews.Where(a => a.UserType == (int)UserType.Interviewer)
                    .ToListAsync(cancellationToken: cancellationToken);

            var candidateBookingDateTime = await
                _context.BookingDateBookingTimeViews.Where(a => a.UserType == (int)UserType.Candidate)
                    .ToListAsync(cancellationToken: cancellationToken);

            // get intersect from interviewer and candidate
            var intersectResult = interviewerBookingDateTime
                .Intersect(interviewerBookingDateTime, new InterviewerCandidateComparer())
                .Intersect(candidateBookingDateTime, new InterviewerCandidateComparer())
                .Select(a => new
                {
                    bookingDate = a.DateOfBooking,
                    endedTime = a.EndedBookingTime,
                    startedTime = a.StartedBookingTime
                }).ToList();

            //map interviewer and candidate together by time and date
            var bookingDateTimeResult =
                //get interviewer filter by intersect value
                interviewerBookingDateTime.Where(b => intersectResult.Any(a =>
                        a.bookingDate == b.DateOfBooking && a.endedTime == b.EndedBookingTime &&
                        a.startedTime == b.StartedBookingTime))
                    //join with candidate filter by intersect value
                    .Join(
                        candidateBookingDateTime.Where(b => intersectResult.Any(a => a.bookingDate == b.DateOfBooking &&
                            a.endedTime == b.EndedBookingTime &&
                            a.startedTime == b.StartedBookingTime)),
                        //join by date of booking and ended booking time and started booking time
                        interviewer => new
                            { interviewer.DateOfBooking, interviewer.EndedBookingTime, interviewer.StartedBookingTime },
                        candidate => new
                            { candidate.DateOfBooking, candidate.EndedBookingTime, candidate.StartedBookingTime },
                        (interviewer, candidate) => new
                        {
                            interviewer,
                            candidate
                        })
                    //group by interviewers list
                    .GroupBy(a => a.interviewer).Select(a => new InterviewerScheduleQueryModel
                    {
                        BookingDate = a.Key.DateOfBooking,
                        EndedDate = a.Key.EndedBookingTime,
                        FirstName = a.Key.FirstName,
                        LastName = a.Key.LastName,
                        StartedDate = a.Key.StartedBookingTime,
                        CandidateSchedule = a.Select(b => new CandidateScheduleQueryModel
                        {
                            EndedDate = b.candidate.EndedBookingTime,
                            FirstName = b.candidate.FirstName,
                            LastName = b.candidate.LastName,
                            StartedDate = b.candidate.StartedBookingTime
                        })
                    }).ToList();

            return new GetAllUserScheduleQueryResponse(true, bookingDateTimeResult);
        }
    }
}