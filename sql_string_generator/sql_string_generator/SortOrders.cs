namespace sql_string_generator
{
  public class SortOrders
  {
    public static ICustomSortOrder ascending = new OrderByAscending();
    public static ICustomSortOrder descending = new OrderByDescending();
  }
}