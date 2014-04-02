namespace sql_string_generator
{
  public interface ICustomSortOrder
  {
    string apply_to(string column_name);
  }
}