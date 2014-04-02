using System;
using System.Collections;

namespace sql_string_generator
{
  public interface IBuildOrderByClauses<Model>
  {
    string build(IBuildAnOrderBy order_builder);
  }
}