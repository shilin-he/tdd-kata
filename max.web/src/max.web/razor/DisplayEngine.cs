namespace max.web.razor
{
  public class DisplayEngine : IDisplayInformation
  {
    IFindViewTemplates template_registry;
    IRenderTemplates template_render_engine;

    public DisplayEngine(IFindViewTemplates template_registry, IRenderTemplates template_render_engine)
    {
      this.template_registry = template_registry;
      this.template_render_engine = template_render_engine;
    }

    public string render<ViewModel>(ViewModel view_model)
    {
      var template = template_registry.find_view_template_for<ViewModel>();
      return template_render_engine.render(template, view_model);
    }
  }
}