using System;
using System.Collections.Generic;

namespace max.web
{
  public class ViewTemplateRegistry : IFindViewTemplates
  {
    IDictionary<Type, IContainTemplateInfo> templates;

    public ViewTemplateRegistry(IDictionary<Type, IContainTemplateInfo> templates)
    {
      this.templates = templates;
    }

    public IContainTemplateInfo find_view_template_for<ViewModel>()
    {
      var view_model_type = typeof(ViewModel);

      if (!templates.ContainsKey(view_model_type)) 
        throw new ArgumentOutOfRangeException(string.Format(
          "The template for the view model '{0}' hasn't been registered.", view_model_type.FullName));

      return templates[view_model_type];
    }
  }
}