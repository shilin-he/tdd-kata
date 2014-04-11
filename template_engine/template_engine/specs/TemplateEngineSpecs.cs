using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace template_engine.specs
{

  [Subject(typeof(TemplateEngine))]
  public class TemplateEngineSpecs
  {
    public abstract class concern : Observes<TemplateEngine>
    {

    }

    public class when_render : concern
    {
      Establish c = () =>
      {
        template = "Hello, {$first_name} {$last_name}";
        view_model = new Person { first_name = "Foo", last_name = "Bar" };

        first_template_string = fake.an<ITemplateString>();
        first_template_string.setup(x => x.start).Return(7);
        first_template_string.setup(x => x.end).Return(19);
        first_template_string.setup(x => x.value).Return("first_name");

        second_template_string = fake.an<ITemplateString>();
        second_template_string.setup(x => x.start).Return(21);
        second_template_string.setup(x => x.end).Return(32);
        second_template_string.setup(x => x.value).Return("last_name");

        template_strings = new[] {first_template_string, second_template_string};
        template_string_finder = depends.on<IFindTemplateStrings>();
        template_string_finder.setup(x => x.find(template)).Return(template_strings);

        value_provider = depends.on<IProvideInstanceValues>();
        value_provider.setup(x => x.convert(view_model, first_template_string.value)).Return(view_model.first_name);
        value_provider.setup(x => x.convert(view_model, second_template_string.value)).Return(view_model.last_name);
      };

      Because b = () =>
        result = sut.render(template, view_model);

      It translates_template_strings_into_instanced_strings = () =>
        result.ShouldEqual("Hello, Foo Bar");

      static string result;
      static Person view_model;
      static string template;
      static IFindTemplateStrings template_string_finder;
      static IEnumerable<ITemplateString> template_strings;
      static ITemplateString first_template_string;
      static ITemplateString second_template_string;
      static IProvideInstanceValues value_provider;
    }
  }

  class Person
  {
    public string first_name { get; set; }
    public string last_name { get; set; }
  }
}
