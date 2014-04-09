using System;

namespace sql_string_generator
{
  public class OrderByClauseBuilder<Model>:IBuildOrderByClauses<Model>
  {
    public string build(IBuildAnOrderBy order_builder)
    {
      var order = order_builder.build();
      return string.IsNullOrEmpty(order) ? string.Empty : "order by " + order;
    }
  }
}