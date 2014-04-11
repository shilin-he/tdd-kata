namespace template_engine
{
  public class TemplateString : ITemplateString
  {
    public TemplateString(string value, int start, int end)
    {
      this.value = value;
      this.start = start;
      this.end = end;
    }

    public string value { get; private set; }
    public int start { get; private set; }
    public int end { get; private set; }
  }
}