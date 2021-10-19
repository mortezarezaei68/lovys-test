using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Commands.CustomerUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.CandidateCommandHandlers
{
    public class AddCandidateCommandHandler : IUserManagementCommandHandlerMediatR<AddCandidateCommandRequest,
        AddCandidateCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public AddCandidateCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddCandidateCommandResponse> Handle(AddCandidateCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<AddCandidateCommandResponse> next)
        {
            if (request.Password != request.ConfirmPassword)
                throw new AppException(ResultCode.BadRequest, "confirm password not the same");
            var user = User.Add(request.UserName, request.FirstName, request.LastName, request.Email, (UserType)request.UserType);
            var result = await _userManager.CreateAsync(user,
                request.Password);
            if (result.Succeeded)
            {
                return new AddCandidateCommandResponse(true, ResultCode.Success);
            }
            throw new AppException(ResultCode.BadRequest, "you have a problem in signup");
        }
    }

    public class AddCustomerCommandRequestValidator : AbstractValidator<AddCandidateCommandRequest>
    {
        public AddCustomerCommandRequestValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p => p.UserType).NotNull();
            RuleFor(p => p.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword).NotNull().NotEmpty();
            RuleFor(a => a.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$").NotNull().NotEmpty()
                .WithMessage("Minimum 8 Characters, at least 1 letter, one number and one special character");
        }
    }
}