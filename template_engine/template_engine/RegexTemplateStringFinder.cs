using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace template_engine
{
  public class RegexTemplateStringFinder : IFindTemplateStrings
  {
    Regex pattern;

    public RegexTemplateStringFinder(Regex pattern)
    {
      this.pattern = pattern;
    }

    public IEnumerable<ITemplateString> find(string template)
    {
      MatchCollection match_collection = pattern.Matches(template);

      foreach (Match match in match_collection)
      {
        yield return new TemplateString(match.Groups[1].Value, match.Index, match.Index + match.Length - 1);
      }
    }
  }
}