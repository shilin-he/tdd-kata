using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class ColumnNameRetriever<Model> : IRetrieveColumnName<Model>
  {
    IMapModelToTable<Model> mapping;

    public ColumnNameRetriever(IMapModelToTable<Model> mapping)
    {
      this.mapping = mapping;
    }

    public string get_column_name<PropertyType>(Expression<Func<Model, PropertyType>> column_expr)
    {
      return mapping.get_column_name(column_expr);
    }
  }
}