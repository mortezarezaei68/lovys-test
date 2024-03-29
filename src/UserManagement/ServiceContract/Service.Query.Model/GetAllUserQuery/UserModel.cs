using UserManagement.Domain;

namespace Service.Query.Model.GetAllUserQuery
{
    public class UserModel
    {
        public string Id { get; set; }
        public string SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
    }
}