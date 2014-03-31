using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public interface IRetrieveColumnName<TableModel>
  {
    string get_column_name<PropertyType>(Expression<Func<TableModel, PropertyType>> column_expr);
  }
}