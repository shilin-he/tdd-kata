using System;
using System.Linq.Expressions;
using System.Net.Configuration;

namespace sql_string_generator
{
  public interface ICreateAnOrderByBuilder<Model>
  {
    IBuildAnOrderBy create<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr, ICustomSortOrder order);
  }
}