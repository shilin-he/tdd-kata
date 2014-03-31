namespace sql_string_generator
{
  public class UpdateClauseBuilder<TableModel> : IBuildUpdateClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;

    public UpdateClauseBuilder(IMapModelToTable<TableModel> mapping)
    {
      this.mapping = mapping;
    }

    public string build()
    {
      return "update " + mapping.get_table_name();
    }
  }
}