using System;
using System.Linq.Expressions;
using System.Net.Configuration;

namespace sql_string_generator
{
  public class OrderByBuilder<Model, PropertyType> : IBuildAnOrderBy
  {
    Expression<Func<Model, PropertyType>> prop_expr;
    ICustomSortOrder sort_order;
    IMapModelToTable<Model> mapping;

    public OrderByBuilder(Expression<Func<Model, PropertyType>> prop_expr, ICustomSortOrder sort_order, IMapModelToTable<Model> mapping)
    {
      this.prop_expr = prop_expr;
      this.sort_order = sort_order;
      this.mapping = mapping;
    }

    public string build()
    {
      var column_name = mapping.get_column_name(prop_expr);
      return sort_order.apply_to(column_name);
    }
  }
}