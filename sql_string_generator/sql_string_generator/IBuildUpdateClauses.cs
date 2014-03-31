namespace sql_string_generator
{
  public interface IBuildUpdateClauses<TableModel>
  {
    string build();
  }
}