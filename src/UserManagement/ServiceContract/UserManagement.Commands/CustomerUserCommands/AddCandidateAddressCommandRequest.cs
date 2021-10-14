using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class AddCandidateAddressCommandRequest:IUserManagementRequest<AddCandidateAddressCommandResponse>
    {
        public decimal Long { get; set; }
        public decimal Lat { get; set; }
        public string Address { get; set; }
    }
}