using FluentAssertions;
using Xunit;

namespace UserManagement.Domain.Tests
{
    public class UserTest
    {
        [Fact]
        public void Should_create_user()
        {
            const string userName = "userNameTest";
            const string firstName = "firstNameTest";
            const string lastName = "lastNameTest";
            const string email = "emailAddress@test.com";
            const UserType userType = UserType.Candidate;
            var user = User.Add(userName, firstName, lastName, email, userType);
            user.UserName.Should().Be(userName);
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
            user.Email.Should().Be(email);
            user.UserType.Should().Be(userType);
        }
    }
}