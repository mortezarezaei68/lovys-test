using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmailManagement.SendMessage;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using UserManagement.Commands.CustomerUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.CandidateCommandHandlers
{
    public class SendEmailToResetPasswordCommandHandler : IUserManagementCommandHandlerMediatR<
        SendEmailToResetPasswordCommandRequest, SendEmailToResetPasswordCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ISendingEmail _sendingEmail;

        public SendEmailToResetPasswordCommandHandler(UserManager<User> userManager, ISendingEmail sendingEmail)
        {
            _userManager = userManager;
            _sendingEmail = sendingEmail;
        }

        public async Task<SendEmailToResetPasswordCommandResponse> Handle(
            SendEmailToResetPasswordCommandRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<SendEmailToResetPasswordCommandResponse> next)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "can not find your email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var message = GenerateInvoice(token, request.Email);
            await _sendingEmail.SendEmailWithToSenderAsync(request.Email,null, "CONFIRM RESET PASSWORD", message);

            return new SendEmailToResetPasswordCommandResponse(true, ResultCode.Success);
        }

        private string GenerateInvoice(string token, string email)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"token", token},
                {"email", email}
            };

            string url = QueryHelpers.AddQueryString("http://halfmilelogistics.com/resetpassword", queryParams);
            StringBuilder invoiceHtml = new StringBuilder();
            invoiceHtml.Append("<div style=\"display: grid; flex-direction: column; justify-content: flex-start;\">")
                .Append("<br />");
            invoiceHtml.Append("<div style=\"margin-bottom: 20px;\">Hi, </div>").Append("<br />");

            invoiceHtml.Append("<div style=\"margin-bottom: 10px; \"> WE HAVE RECEIVED A REQUEST TO RESET THE PASSWORD FOR YOUR ACCOUNT AT");
            invoiceHtml
                .Append(
                    $"<span style=\"margin-bottom: 30px; background: #008CBA; color: #fff; width: 65px; padding: 0 5px;\"><b>{DateTime.UtcNow.ToShortDateString()}</b></span>TO RESET YOUR PASSWORD, PLEASE CLICK</div>")
                .Append("<br />");
            invoiceHtml.Append(
                    $"<a href=\"{url}\" style=\"background-color: #008CBA;border: none;color: white;padding: 15px 32px;text-align: center;text-decoration: none;display: inline-block;margin: 4px 2px;cursor: pointer; width: 10%; margin-bottom: 30px;margin-left:10%;\">RESET PASSWORD</a>")
                .Append("<br />");
            invoiceHtml.Append(
                "<div style=\"margin-bottom: 10px; \">NOTICE: AFTER 2 HOURS YOUR RESET PASSWORD LINK IS EXPIRED</div>");
            invoiceHtml.Append("<div style=\"margin-bottom: 10px; \">BEST REGARDS </div>").Append("<br />");
            invoiceHtml.Append("<div style=\"margin-bottom: 10px; color: #00A3EF\">THE HALFMILELOGISTICS TEAM </div>").Append("</div>");
            return invoiceHtml.ToString();
        }
    }
}