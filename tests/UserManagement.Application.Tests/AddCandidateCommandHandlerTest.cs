using System;
using System.Collections.Generic;
using System.Threading;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation.TestHelper;
using Framework.Exception.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using UserManagement.Command.Handlers.CandidateCommandHandlers;
using UserManagement.Commands.CustomerUserCommands;
using UserManagement.Domain;
using Xunit;

namespace UserManagement.Application.Tests
{
    public class AddCandidateCommandHandlerTest
    {
        private readonly AddCustomerCommandRequestValidator _validator;

        public AddCandidateCommandHandlerTest()
        {
            _validator = new AddCustomerCommandRequestValidator();
        }

        [Fact]
        public void Should_users_created()
        {
            var userCommand = new AddCandidateCommandRequest
            {
                Email = "morteza.rezaei68@gmail.com",
                Password = "@Daneshgah65411887",
                ConfirmPassword = "@Daneshgah65411887",
                FirstName = "Morteza",
                LastName = "Rezaei",
                UserName = "morteza.rezaei68"
            };
            var userManager = Substitute.For<FakeUserManager>();
            var handler = new AddCandidateCommandHandler(userManager);
            var mockPipelineBehaviourDelegate = Substitute.For<RequestHandlerDelegate<AddCandidateCommandResponse>>();
            userManager.CreateAsync(Verify.That<User>(command =>
            {
                command.FirstName.Should().Be(userCommand.FirstName);
                command.LastName.Should().Be(userCommand.LastName);
                command.UserName.Should().Be(userCommand.UserName);
                command.Email.Should().Be(userCommand.Email);
            }), userCommand.Password).Returns(info => IdentityResult.Success);
            handler.Handle(userCommand, default(CancellationToken), mockPipelineBehaviourDelegate).Wait();
        }

        [Fact]
        public void Should_exception_confirmPassword_and_password_not_match()
        {
            var userCommand = new AddCandidateCommandRequest
            {
                Email = "morteza.rezaei68@gmail.com",
                Password = "@Daneshgah65411887",
                ConfirmPassword = "@Daneshgah6541188",
                FirstName = "Morteza",
                LastName = "Rezaei",
                UserName = "morteza.rezaei68"
            };
            var userManager = Substitute.For<FakeUserManager>();
            var handler = new AddCandidateCommandHandler(userManager);
            var mockPipelineBehaviourDelegate = Substitute.For<RequestHandlerDelegate<AddCandidateCommandResponse>>();
            Action act = () =>
                handler.Handle(userCommand, default(CancellationToken), mockPipelineBehaviourDelegate).Wait();
            act.Should().Throw<AppException>();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_have_error_when_properties_are_not_filled(string email, string password,
            string confirmPassword, string firstName, string lastName, string userName)
        {
            var model = new AddCandidateCommandRequest
            {
                Email = email, Password = password, ConfirmPassword = confirmPassword, FirstName = firstName,
                LastName = lastName, UserName = userName
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { "", "", "", "", "", "" },
                new object[]
                    { "morteza.com", "@Daneshgah6541", "@Daneshgah6541", "morteza", "rezaei", "morteza.rezaei" },
                new object[] { "morteza.com", "@Daneshgah654", "@Daneshgah6541", "", "", "" },
                new object[] { "morteza@a.com", "daneshgah", "daneshgah", "morteza", "rezaei", "morteza.rezaei" },
            };
    }
}