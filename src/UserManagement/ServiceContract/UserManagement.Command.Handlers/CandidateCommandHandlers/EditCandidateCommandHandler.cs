using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Framework.Common;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Commands.CustomerUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.CandidateCommandHandlers
{
    public class EditCandidateCommandHandler : IUserManagementCommandHandlerMediatR<EditCandidateCommandRequest,
        EditCandidateCommandResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<User> _userManager;

        public EditCandidateCommandHandler(ICurrentUser currentUser, UserManager<User> userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<EditCandidateCommandResponse> Handle(EditCandidateCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<EditCandidateCommandResponse> next)
        {
            var userId = _currentUser.GetUserId();
            var customer = await _userManager.Users
                .FirstOrDefaultAsync(a => a.SubjectId.ToString() == userId, cancellationToken: cancellationToken);
            if (customer is null)
                throw new AppException(ResultCode.BadRequest, "customer not exist");
            
            customer.UpdateCustomer(request.FirstName, request.LastName, request.UserName,
                request.Email);

            await _userManager.UpdateAsync(customer);
            return new EditCandidateCommandResponse(true, ResultCode.Success);
        }
    }

    public class EditCustomerCommandRequestValidator : AbstractValidator<EditCandidateCommandRequest>
    {
        public EditCustomerCommandRequestValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p => p.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$");
            RuleFor(p => p.GenderId).NotEqual(0);
        }
    }
}