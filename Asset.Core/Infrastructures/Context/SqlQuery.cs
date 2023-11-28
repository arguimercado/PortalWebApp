using Applications.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Asset.Core.Infrastructures.Context;

internal sealed class SqlQuery : ISqlQuery
{
    private readonly SqlConnection _sqlConnection;
    public SqlQuery(string connectionString)
    {
        _sqlConnection = new SqlConnection(connectionString);
    }
    public void Open()
    {
        if(_sqlConnection.State == ConnectionState.Closed)
            _sqlConnection.Open();
    }

    public void Close()
    {
        if (_sqlConnection.State == ConnectionState.Open)
            _sqlConnection.Close();
    }

    public async Task DynamicExecute(string sql, object parameters, CommandType commandType = CommandType.Text)
    {
        try
        {
            Open();
            var results = await _sqlConnection.ExecuteAsync(sql, parameters,commandType: commandType);
            Close();

        }
        catch(Exception ex)
        {
            Close();
            throw;
        }
    }

    public async Task<IEnumerable<TResult>> DynamicQuery<TResult>(string sql, object? parameters, CommandType commandType = CommandType.Text)
    {
        try
        {
            Open();
            var results = await _sqlConnection.QueryAsync<TResult>(sql, parameters, commandType: commandType);
            Close();
            return results;
        }
        catch(Exception ex)
        {
            Close();
            throw;
        }
    }

    public async Task<IEnumerable<TResult>> DynamicQuery<TFirst, TSecond, TResult>(string sql, object parameters, Func<TFirst, TSecond, TResult> map, string splitOn, CommandType commandType = CommandType.Text)
    {
        try
        {
            Open();
            var results = await _sqlConnection.QueryAsync<TFirst, TSecond, TResult>(sql, map: map, param: parameters, splitOn: splitOn, commandType: commandType);
            Close();
            return results;

        }
        catch(Exception ex)
        {
            Close();
            throw;
        }
    }

    public async Task<TResult> DynamicQueryScalar<TResult>(string sql, object? parameters, CommandType commandType = CommandType.Text) where TResult : struct
    {
        try
        {
            Open();
            var results = await _sqlConnection.ExecuteScalarAsync<TResult>(sql, parameters, commandType: commandType);
            Close();

            return results;

        }catch(Exception ex)
        {
            Close();
            throw;
        }
    }

    public async Task<IEnumerable<TResult>> DynamicQuery<TFirst, TSecond, TThird, TResult>(string sql, object parameters, Func<TFirst, TSecond, TThird, TResult> map, string splitOn, CommandType commandType = CommandType.Text)
    {
        try
        {
            Open();
            var results = await _sqlConnection.QueryAsync<TFirst, TSecond,TThird, TResult>(sql, map: map, param: parameters, splitOn: splitOn, commandType: commandType);
            Close();
            return results;

        }catch(Exception ex)
        {
            Close();
            throw;
        }
    }

    public async Task<TResult> DynamicExecute<TResult>(string sql, object parameters, CommandType commandType = CommandType.Text) where TResult : struct
    {
        try
        {
            Open();
            var results = await _sqlConnection.ExecuteScalarAsync<TResult>(sql, 
                parameters, commandType: commandType);
            Close();

            return results;

        }
        catch (Exception ex)
        {
            Close();
            throw;
        }
    }
}
