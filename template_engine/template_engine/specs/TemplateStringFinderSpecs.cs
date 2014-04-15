 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text.RegularExpressions;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace template_engine.specs
{   
  [Subject(typeof(RegexTemplateStringFinder))]
  public class RegexTemplateStringFinderSpecs
  {
    public abstract class concern : Observes<RegexTemplateStringFinder>
    {
        
    }

    public class when_find : concern
    {
      Establish c = () =>
      {
        template = "Hello, {$first_name} {$last_name}";
        pattern = new Regex(@"\{\$(.+?)\}");
        depends.on(pattern);
      };

      Because b = () =>
        result = sut.find(template);

      It returns_template_strings_found_in_the_template = () =>
      {
        result.Count().ShouldEqual(2);
        var first_name = result.First();
        first_name.value.ShouldEqual("first_name");
        first_name.start.ShouldEqual(7);
        first_name.end.ShouldEqual(19);

        var last_name = result.Last();
        last_name.value.ShouldEqual("last_name");
        last_name.start.ShouldEqual(21);
        last_name.end.ShouldEqual(32);
      };

      static IEnumerable<ITemplateString> result;
      static string template;
      static Regex pattern;
    }
  }
}
