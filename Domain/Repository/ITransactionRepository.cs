namespace Domain.Repository
{
    public interface ITransactionRepository
    {
        void BeginTransaction();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
    }
}
