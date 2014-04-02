namespace sql_string_generator
{
  public class OrderByClauseBuilder<Model>:IBuildOrderByClauses<Model>
  {
    public string build(IBuildAnOrderBy order_builder)
    {
      return "order by " + order_builder.build();
    }
  }
}