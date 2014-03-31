using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public interface ICreateSelectClauses<TableModel>
  {
    string create(params Expression<Func<TableModel, object>>[] columns);
  }
}