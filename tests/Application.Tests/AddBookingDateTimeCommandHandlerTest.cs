using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using FluentValidation.TestHelper;
using Framework.Common;
using MediatR;
using NSubstitute;
using ScheduleManagement.Command.Handlers;
using ScheduleManagement.Commands;
using ScheduleManagement.Domain;
using ScheduleManagement.Validations;
using Xunit;

namespace Application.Tests
{
    public class AddBookingDateTimeCommandHandlerTest
    {
        private readonly AddBookingDateTimeCommandRequestValidator _validator;

        public AddBookingDateTimeCommandHandlerTest()
        {
            _validator = new AddBookingDateTimeCommandRequestValidator();
        }

        [Fact]
        public void Should_users_created()
        {
            var userCommand = new AddBookingDateTimeCommandRequest
            {
                BookingDate = DateTime.Now,
                EndedTime = new TimeSpan(13, 0, 0),
                StartedTime = new TimeSpan(14, 0, 0)
            };
            var repository = Substitute.For<IBookingDateRepository>();
            var bookingValidationService = Substitute.For<IBookingDateValidationService>();
            var handler =
                new AddBookingDateTimeCommandHandler(repository, new FakeCurrentUser(), bookingValidationService);
            var mockPipelineBehaviourDelegate =
                Substitute.For<RequestHandlerDelegate<AddBookingDateTimeCommandResponse>>();
            bookingValidationService.CheckValidTimeRange(userCommand.StartedTime, userCommand.EndedTime);
            repository.GetByBookDate(DateTime.Now).Returns(new BookingDate());
            repository.Add(Verify.That<BookingDate>(bookingDate =>
            {
                bookingDate.DateOfBooking.Should().Be(userCommand.BookingDate);
                bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
                {
                    first.EndedBookingTime.Should().Be(userCommand.EndedTime);
                    first.StartedBookingTime.Should().Be(userCommand.StartedTime);
                    first.SubjectId.Should().Be(ConstValues.User.UserId);
                });
            }));
            handler.Handle(userCommand, default, mockPipelineBehaviourDelegate).Wait();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_have_error_when_properties_are_not_filled(DateTime bookingDate, TimeSpan startedTime,
            TimeSpan endedTime)
        {
            var model = new AddBookingDateTimeCommandRequest
            {
                BookingDate = bookingDate,
                EndedTime = endedTime,
                StartedTime = startedTime
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { null, null, null },
                new object[] { null, new TimeSpan(14, 0, 0), new TimeSpan(14, 0, 0) },
            };
    }
}