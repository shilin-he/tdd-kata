namespace sql_string_generator
{
  public class OrderByDescending : ICustomSortOrder
  {
    public string apply_to(string column_name)
    {
      return column_name + " desc";
    }
  }
}