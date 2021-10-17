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

            _validation.CheckValidTimeRange(request.StartedTime, request.EndedTime);
            var existBookDate = await _repository.GetByBookDate(request.BookingDate);

            if (existBookDate is not null)
            {
                existBookDate.Update(request.StartedTime, request.EndedTime, userId);
                return new AddBookingDateTimeCommandResponse(true, ResultCode.Success);
            }

            var bookingDate = BookingDate.Add(request.BookingDate, request.StartedTime, request.EndedTime, userId);
            _repository.Add(bookingDate);
            return new AddBookingDateTimeCommandResponse(true, ResultCode.Success);
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