using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_engine
{
  public class TemplateEngine : IRenderTemplates
  {
    IFindTemplateStrings template_string_finder;
    IProvideInstanceValues instance_value_provider;

    public TemplateEngine(IFindTemplateStrings template_string_finder, IProvideInstanceValues instance_value_provider)
    {
      this.template_string_finder = template_string_finder;
      this.instance_value_provider = instance_value_provider;
    }

    public string render<ViewModel>(string template, ViewModel view_model)
    {
      var sb = new StringBuilder();
      int index = 0;

      IEnumerable<ITemplateString> template_strings = template_string_finder.find(template).OrderBy(x => x.start);
      foreach (var template_string in template_strings)
      {
        sb.Append(template.Substring(index, template_string.start - index));
        sb.Append(instance_value_provider.convert(view_model, template_string.value));
        index = template_string.end + 1;
      }

      return sb.ToString();
    }
  }
}
