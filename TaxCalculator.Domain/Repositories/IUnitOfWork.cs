namespace TaxCalculator.Domain.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
