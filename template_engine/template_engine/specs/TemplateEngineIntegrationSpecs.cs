using System.Text.RegularExpressions;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace template_engine.specs
{
  [Subject(typeof(TemplateEngine))]
  public class TemplateEngineIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_rendering : concern
    {
      Establish c = () =>
      {
        var pattern = new Regex(@"\{\$(.+?)\}");
        template_engine = new TemplateEngine(new RegexTemplateStringFinder(pattern), new InstanceValueResolver());
        person = new Person
        {
          first_name = "Joe",
          last_name = "Doe",
          address = new Address {city = "meanwhile", street = "123 street"}
        };
        template = "{$first_name} {$last_name}: {$address.street} {$address.city}";
        rendered_templ = "Joe Doe: 123 street meanwhile";
      };

      Because b = () =>
        result = template_engine.render(template, person);

      It replaces_the_template_strings_with_instance_values = () =>
        result.ShouldEqual(rendered_templ);

      static string result;
      static string rendered_templ;
      static TemplateEngine template_engine;
      static Person person;
      static string template;
    }
  }
}
