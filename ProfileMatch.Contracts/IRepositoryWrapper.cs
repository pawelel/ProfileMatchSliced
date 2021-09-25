namespace ProfileMatch.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }
    }
}
