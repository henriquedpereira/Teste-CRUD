using Core.Interfaces;
using Dapper.FluentMap.Dommel.Mapping;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using RepositoryHelpers.Mapping;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

public class DataContext
{
    private readonly IConfiguration _configuration;
    public IUnitOfWork UnitOfWork { get; private set; }

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var connection = CreateConnection();
        UnitOfWork = new UnitOfWork(connection);
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public void Dispose()
    {
        UnitOfWork?.Dispose();
    }
}