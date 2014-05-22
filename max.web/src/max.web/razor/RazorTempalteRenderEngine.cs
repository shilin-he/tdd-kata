using RazorEngine;

namespace max.web.razor
{
  public class RazorTempalteRenderEngine : IRenderTemplates
  {
    public string render<ViewModel>(IContainTemplateInfo template, ViewModel view_model)
    {
      return Razor.Parse(template.content, view_model);
    }
  }
}