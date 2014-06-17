namespace max.web
{
  public class ViewTable
  {
    private static ViewCollection _instance = new ViewCollection();

    public static ViewCollection views
    {
      get { return _instance; }
    }

    private ViewTable() { }
  }
}