namespace template_engine
{
  public interface IRenderTemplates
  {
    string render<ViewModel>(string template, ViewModel view_model);
  }
}