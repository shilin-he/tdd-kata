using System;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace sql_string_generator.query_play_ground
{
  public class DbQueryProvider : QueryProvider
  {
    readonly IDbConnection connection;

    public DbQueryProvider(IDbConnection connection)
    {
      this.connection = connection;
    }

    public override string GetQueryText(Expression expression)
    {
      return Translate(expression);
    }

    public override object Execute(Expression expression)
    {
      var cmd = connection.CreateCommand();

      cmd.CommandText = Translate(expression);

      IDataReader reader = cmd.ExecuteReader();

      Type elementType = TypeSystem.GetElementType(expression.Type);

      return Activator.CreateInstance(
        typeof(ObjectReader<>).MakeGenericType(elementType),
        BindingFlags.Instance | BindingFlags.NonPublic, null,
        new object[] {reader},
        null);
    }

    string Translate(Expression expression)
    {
      return new QueryTranslator().Translate(expression);
    }
  }
}