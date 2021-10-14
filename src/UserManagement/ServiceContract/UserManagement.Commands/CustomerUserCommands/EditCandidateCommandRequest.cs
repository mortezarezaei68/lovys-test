using System;
using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class EditCandidateCommandRequest:IUserManagementRequest<EditCandidateCommandResponse>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public DateTime Birthday { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string UserName { get; set; }
        public string Address { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int AddressId { get; set; }
        
        public int GenderId { get; set; }

    }
}