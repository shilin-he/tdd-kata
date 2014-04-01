using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public interface IBuildWhereClauses<TableModel>
  {
    string build(TableModel model);
    string build(Expression<Func<TableModel, bool>> filter);
  }
}