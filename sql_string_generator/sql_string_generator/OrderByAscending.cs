namespace sql_string_generator
{
  public class OrderByAscending : ICustomSortOrder
  {
    public string apply_to(string column_name)
    {
      return column_name;
    }
  }
}