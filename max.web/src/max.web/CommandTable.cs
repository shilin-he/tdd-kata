using max.web.razor;

namespace max.web
{
  public class CommandTable
  {
    static WebCommandCollection _instance = new WebCommandCollection(
      new DisplayEngine(new ViewTemplateRegistry(ViewTable.views), new RazorTempalteRenderEngine()));

    public static WebCommandCollection commands
    {
      get { return _instance; }
    }

    private CommandTable() { }
  }
}