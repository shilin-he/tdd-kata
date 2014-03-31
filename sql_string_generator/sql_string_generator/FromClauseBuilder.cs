namespace sql_string_generator
{
  public class FromClauseBuilder<TableModel> : IBuildFromClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;

    public FromClauseBuilder(IMapModelToTable<TableModel> mapping)
    {
      this.mapping = mapping;
    }

    public string build()
    {
      return "from " + mapping.get_table_name();
    }
  }
}