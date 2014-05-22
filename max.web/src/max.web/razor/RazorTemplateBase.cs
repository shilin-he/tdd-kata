using System.Text;

namespace max.web.razor
{
  public abstract class RazorTemplateBase
  {
    StringBuilder buffer;

    protected RazorTemplateBase()
    {
      buffer = new StringBuilder();
    }

    public dynamic view_model { get; set; }

    public abstract void Execute();

    // Writes the results of expressions like: "@foo.Bar"
    public virtual void Write(object value)
    {
      // Don't need to do anything special
      // Razor for ASP.Net does HTML encoding here.
      WriteLiteral(value);
    }

    // Writes literals like markup: "<p>Foo</p>"
    public virtual void WriteLiteral(object value)
    {
      buffer.Append(value);
    }

    public string render<ViewModel>(ViewModel vm)
    {
      view_model = vm;
      Execute();
      return buffer.ToString();
    }
  }
}