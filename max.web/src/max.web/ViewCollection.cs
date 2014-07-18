using System;
using System.Collections;
using System.Collections.Generic;
using max.web.razor;

namespace max.web
{
  public class ViewCollection : Dictionary<Type, IContainTemplateInfo>
  {
    public void add_view_from_file<ViewModel>(string template_path)
    {
      add_view_from_file<ViewModel>(template_path, () => AppDomain.CurrentDomain.BaseDirectory);
    }

    public void add_view_from_file<ViewModel>(string template_path, IProvideAppBaseDirectory app_base_directory_provider)
    {
      Add(typeof(ViewModel), new RazorFileTemplate(template_path, app_base_directory_provider));
    }
  }
}