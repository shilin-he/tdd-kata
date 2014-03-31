namespace sql_string_generator
{
  public interface IBuildDeleteSqlStatements<in Model>
  {
    string build(Model model);
  }
}