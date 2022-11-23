using System.Data;

namespace Model.Context;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}