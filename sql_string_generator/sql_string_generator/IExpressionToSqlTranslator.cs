using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public interface IExpressionToSqlTranslator<TModel>
  {
    string translate(Expression<Func<TModel, bool>> expr);
  }
}