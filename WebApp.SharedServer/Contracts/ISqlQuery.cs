using System.Data;

namespace Applications.Interfaces;

public interface ISqlQuery {
    Task DynamicExecute(string sql, object parameters, CommandType commandType = CommandType.Text);
    Task<TResult> DynamicExecute<TResult?>(string sql, object parameters, CommandType commandType = CommandType.Text);
    Task<IEnumerable<TResult>> DynamicQuery<TResult>(string sql, object? parameters, CommandType commandType = CommandType.Text);
    Task<IEnumerable<TResult>> DynamicQuery<TFirst, TSecond, TResult>(string sql, object parameters, Func<TFirst, TSecond, TResult> map, string splitOn,CommandType commandType = CommandType.Text);

    Task<IEnumerable<TResult>> DynamicQuery<TFirst, TSecond,TThird, TResult>(string sql, object parameters, Func<TFirst, TSecond, TThird, TResult> map, string splitOn, CommandType commandType = CommandType.Text);

    Task<TResult> DynamicQueryScalar<TResult>(string sql, object? parameters,CommandType commandType = CommandType.Text) where TResult : struct;

}
