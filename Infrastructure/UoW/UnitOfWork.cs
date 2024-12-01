using System.Data;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _disposed;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
    }

    public IDbConnection Connection => _connection;
    public IDbTransaction Transaction => _transaction;

    public void Begin()
    {
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}