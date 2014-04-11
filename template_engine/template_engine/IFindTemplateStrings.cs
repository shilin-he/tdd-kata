using System.Collections.Generic;

namespace template_engine
{
  public interface IFindTemplateStrings
  {
    IEnumerable<ITemplateString> find(string template);
  }
}