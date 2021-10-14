using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;

namespace UserManagement.Domain.PersistGrants
{
    public interface IPersistGrantRepository : IRepository
    {
        void Add(PersistGrant persistGrant);
        Task<PersistGrant> GetByUser(string subjectId);
    }
}