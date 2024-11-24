namespace ManageIt.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
