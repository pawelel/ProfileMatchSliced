using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }

        void Save();
        Task SaveAsync();
    }
}
