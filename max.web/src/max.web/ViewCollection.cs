using System;
using System.Collections;
using System.Collections.Generic;
using max.web.razor;

namespace max.web
{
  public class ViewCollection : Dictionary<Type, IContainTemplateInfo>
  {
    public void add_view_from_file(Type view_model_type, string template_path)
    {
      add_view_from_file(view_model_type, template_path, () => AppDomain.CurrentDomain.BaseDirectory);
    }

    public void add_view_from_file(Type view_model_type, string template_path, IProvideAppBaseDirectory app_base_directory_provider)
    {
      Add(view_model_type, new RazorFileTemplate(template_path, app_base_directory_provider));
    }
  }
}