namespace max.web.razor
{
  public interface IRenderTemplates
  {
    string render<ViewModel>(IContainTemplateInfo template, ViewModel view_model);
  }
}