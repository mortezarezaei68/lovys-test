using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Framework.Common;
using Framework.Domain.Core;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using ScheduleManagement.Commands;
using ScheduleManagement.Domain;
using ScheduleManagement.Handlers;
using ScheduleManagement.Validations;

namespace ScheduleManagement.Command.Handlers
{
    public class AddBookingDateTimeCommandHandler : IScheduleManagementCommandHandlerMediatR<
        AddBookingDateTimeCommandRequest,
        AddBookingDateTimeCommandResponse>
    {
        private readonly IBookingDateRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IBookingDateValidationService _validation;

        public AddBookingDateTimeCommandHandler(IBookingDateRepository repository, ICurrentUser currentUser,
            IBookingDateValidationService validation)
        {
            _repository = repository;
            _currentUser = currentUser;
            _validation = validation;
        }

        public async Task<AddBookingDateTimeCommandResponse> Handle(AddBookingDateTimeCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<AddBookingDateTimeCommandResponse> next)
        {
            var userId = _currentUser.GetUserId();
            var timeSlots = CreateTimeSlots(request.StartedTime, request.EndedTime);

            _validation.CheckValidTimeRange(request.StartedTime, request.EndedTime);
            var existBookDate = await _repository.GetByBookDate(request.BookingDate);

            if (existBookDate is not null)
            {
                existBookDate.Update(timeSlots, userId);
                return new AddBookingDateTimeCommandResponse(true, ResultCode.Success);
            }


            var bookingDate = BookingDate.Add(request.BookingDate,userId, timeSlots);
            _repository.Add(bookingDate);
            return new AddBookingDateTimeCommandResponse(true, ResultCode.Success);
        }

        private List<BookingTimeOption> CreateTimeSlots(TimeSpan requestStartedTime, TimeSpan requestEndedTime)
        {
            var timeSlots = new List<BookingTimeOption>();
            var timeSlotsLong = requestEndedTime - requestStartedTime;
            for (int i = 0; i < timeSlotsLong.Hours; i++)
            {
                var endedTime = requestStartedTime.Add(new TimeSpan(i + 1, 0, 0));
                var startedTime = requestStartedTime.Add(new TimeSpan(i, 0, 0));
                timeSlots.Add(new BookingTimeOption
                {
                    StartedTime = endedTime,
                    EndedTime = startedTime
                });
            }

            return timeSlots;
        }
    }

    public class AddBookingDateTimeCommandRequestValidator : AbstractValidator<AddBookingDateTimeCommandRequest>
    {
        public AddBookingDateTimeCommandRequestValidator()
        {
            RuleFor(p => p.BookingDate).NotEmpty().NotNull();
            RuleFor(p => p.EndedTime).NotEmpty().NotNull();
            RuleFor(p => p.StartedTime).NotEmpty().NotNull();
        }
    }
}