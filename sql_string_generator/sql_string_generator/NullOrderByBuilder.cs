namespace sql_string_generator
{
  public class NullOrderByBuilder : IBuildAnOrderBy
  {
    public string build()
    {
      return string.Empty;
    }
  }
}