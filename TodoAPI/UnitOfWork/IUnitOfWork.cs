namespace TodoAPI.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<int> CompleteAsync();
}