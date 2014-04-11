namespace template_engine
{
  public interface ITemplateString
  {
    string value { get; }
    int start { get; }
    int end { get; }
  }
}