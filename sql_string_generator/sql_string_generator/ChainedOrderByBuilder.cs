using System;

namespace sql_string_generator
{
  public class ChainedOrderByBuilder<Model> : IBuildAnOrderBy
  {
    IBuildAnOrderBy first;
    IBuildAnOrderBy second;

    public ChainedOrderByBuilder(IBuildAnOrderBy first, IBuildAnOrderBy second)
    {
      this.first = first;
      this.second = second;
    }

    public string build()
    {
      var first_order = first.build();
      return string.IsNullOrEmpty(first_order) ? second.build() : first_order + ", " + second.build();
    }
  }
}