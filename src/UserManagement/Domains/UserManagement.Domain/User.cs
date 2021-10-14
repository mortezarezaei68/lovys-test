using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.PersistGrants;

namespace UserManagement.Domain
{
    public class User : IdentityUser<int>
    {
        public Guid SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public UserType UserType { get; set; }


        public static User Add(string userName, string firstName, string lastName, string email, UserType userType)
        {
            return new()
            {
                UserName = userName,
                FirstName = firstName,
                Email = email,
                LastName = lastName,
                SubjectId = Guid.NewGuid(),
                UserType = userType
            };
        }

        public void Update(string firstName, string lastName, string userName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            EmailConfirmed = false;
        }

        public void UpdateCustomer(string firstName, string lastName, string userName,
            string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
        }

        public void UpdatePersistGrants()
        {
            var lastGrants = _grantsList.Where(a => !a.IsRevoke);
            foreach (var items in lastGrants)
            {
                items.Update();
            }
        }

        public void Delete()
        {
            IsDeleted = true;
            var persistGrant = PersistGrants.Where(a => !a.IsRevoke).ToList();
            foreach (var items in persistGrant)
            {
                items.Update();
            }
        }

        public void AddPersistGrant(string subjectId, string refreshToken, string ipAddress, DateTime expiredTime,
            UserType userType)
        {
            var persistGrants = new PersistGrant(subjectId, refreshToken, ipAddress, expiredTime, userType);
            _grantsList.Add(persistGrants);
        }

        private readonly List<PersistGrant> _grantsList = new();
        public IReadOnlyCollection<PersistGrant> PersistGrants => _grantsList;
    }
}