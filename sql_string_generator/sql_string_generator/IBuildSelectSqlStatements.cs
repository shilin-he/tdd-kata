using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public interface IBuildSelectSqlStatements<Model>
  {
    string build();
    IBuildSelectSqlStatements<Model> order_by<PropertyType>(Expression<Func<Model, PropertyType>> property_accessor, ICustomSortOrder sort_order);
  }
}