using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.PersistGrants;
using UserManagement.Persistance.Context;
using UserManagement.Persistence.EF.UnitOfWork;

namespace UserManagement.Persistence.EF.Repository
{
    public class PersistGrantsRepository : IPersistGrantRepository
    {
        private readonly IdentityUnitOfWork _identityUnitOfWork;
        private readonly UserManagementDbContext _userManagementDbContext;

        public PersistGrantsRepository(IdentityUnitOfWork identityUnitOfWork,
            UserManagementDbContext userManagementDbContext)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _userManagementDbContext = userManagementDbContext;
        }

        public IUnitOfWork UnitOfWork => _identityUnitOfWork;

        public void Add(PersistGrant persistGrants)
        {
            _userManagementDbContext.PersistGrants.Add(persistGrants);
        }

        public async Task<PersistGrant> GetByUser(string subjectId)
        {
            var persistGrants =
                await _userManagementDbContext.PersistGrants.FirstOrDefaultAsync(a => a.SubjectId == subjectId);
            return persistGrants;
        }
    }
}