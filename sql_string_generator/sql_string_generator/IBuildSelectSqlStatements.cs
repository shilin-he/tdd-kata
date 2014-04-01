namespace sql_string_generator
{
  public interface IBuildSelectSqlStatements<Model>
  {
    string build();
  }
}