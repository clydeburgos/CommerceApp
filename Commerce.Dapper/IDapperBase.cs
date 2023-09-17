
using System.Data;

namespace Commerce.Dapper
{
    public interface IDapperBase
    {
        string ConnString { get; set; }

        Task<int> ExecuteNonQueryAsync(string queryToExec, object? parameters,
            CommandType commandType = CommandType.Text, int? timeOut = 0);
        Task<T> ExecuteScalarAsync<T>(string queryToExec, object? parameters, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> QueryListAsync<T>(string queryToExec, object? parameters = null, CommandType commandType = CommandType.Text);
        Task<List<T>> QueryAsListAsync<T>(string queryToExec, object? parameters = null, CommandType commandType = CommandType.Text);
        Task<T> QuerySingleOrDefaultAsync<T>(string queryToExec, object? parameters = null,
            CommandType commandType = CommandType.Text);

        Task<T> QueryFirstOrDefaultAsync<T>(string queryToExec, object? parameters = null,
            CommandType commandType = CommandType.Text);
    }
}
