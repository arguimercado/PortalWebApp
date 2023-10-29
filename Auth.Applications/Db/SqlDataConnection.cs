using System.Data.SqlClient;

namespace Auth.Applications.Db;

public class SqlDataConnection : IDisposable
{
    private readonly SqlConnection _sqlConnection;

    public SqlDataConnection(string connectionString)
    {
        _sqlConnection = new SqlConnection(connectionString);
    }

    public void Open()
    {
        if(_sqlConnection.State == System.Data.ConnectionState.Closed)
        {
            _sqlConnection.Open();
        }
    }

    public void Close()
    {
        if (_sqlConnection.State == System.Data.ConnectionState.Open) {
            _sqlConnection.Close();
        }
    }

    public SqlConnection GetConnection()
    {
        return _sqlConnection;
    }

    public void Dispose()
    {
        _sqlConnection.Dispose();
    }
}