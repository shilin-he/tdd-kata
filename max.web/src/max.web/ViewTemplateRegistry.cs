using System;
using System.Collections.Generic;

namespace max.web
{
  public class ViewTemplateRegistry : IFindViewTemplates
  {
    IDictionary<Type, IContainTemplateInfo> templates_mapping;

    public ViewTemplateRegistry(IDictionary<Type, IContainTemplateInfo> templates_mapping)
    {
      this.templates_mapping = templates_mapping;
    }

    public IContainTemplateInfo find_view_template_for<ViewModel>()
    {
      var view_model_type = typeof(ViewModel);

      if (!templates_mapping.ContainsKey(view_model_type)) 
        throw new ArgumentOutOfRangeException(string.Format(
          "The template for the view model '{0}' hasn't been registered.", view_model_type.FullName));

      return templates_mapping[view_model_type];
    }
  }
}