using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class DeleteCandidateAddressCommandRequest:IUserManagementRequest<DeleteCandidateAddressCommandResponse>
    {
        public int Id { get; set; }
    }
}