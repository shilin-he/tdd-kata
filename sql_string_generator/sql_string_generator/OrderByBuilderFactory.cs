using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class OrderByBuilderFactory<Model> : ICreateAnOrderByBuilder<Model>
  {
    IMapModelToTable<Model> mapping;

    public OrderByBuilderFactory(IMapModelToTable<Model> mapping)
    {
      this.mapping = mapping;
    }

    public IBuildAnOrderBy create<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr, ICustomSortOrder order)
    {
      return new OrderByBuilder<Model, PropertyType>(prop_expr, order, mapping);
    }
  }
}