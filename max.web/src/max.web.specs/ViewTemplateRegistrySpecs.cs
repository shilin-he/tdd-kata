using System;
using System.Collections.Generic;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace max.web.specs
{

  [Subject(typeof(ViewTemplateRegistry))]
  public class ViewTemplateRegistrySpecs
  {
    public abstract class concern : Observes<ViewTemplateRegistry>
    {

    }

    public class when_find_template_for_a_view_model : concern
    {
      class and_it_has_the_template
      {
        Establish c = () =>
        {
          template = fake.an<IContainTemplateInfo>();
          templates = new Dictionary<Type, IContainTemplateInfo>
          {
            {typeof(AnItem), template}
          };
          depends.on(templates);
        };

        Because b = () =>
          result = sut.find_view_template_for<AnItem>();

        It returns_the_template = () =>
          result.ShouldEqual(template);

        static IContainTemplateInfo result;
        static IContainTemplateInfo template;
        static IDictionary<Type, IContainTemplateInfo> templates;
      }

      class and_it_does_not_have_a_template_for_the_view_model
      {
        Establish c = () =>
        {
          template = "blah";
          template_id = "templates\\blah.tmpl";
          templates = new Dictionary<Type, string>();
          depends.on(templates);
        };

        Because b = () =>
          spec.catch_exception(() => sut.find_view_template_for<AnItem>());

        It throws_an_invalid_argument_exception = () =>
          spec.exception_thrown.ShouldBeOfType<ArgumentOutOfRangeException>();

        static string template;
        static string template_id;
        static IDictionary<Type, string> templates;
      }
    }
  }
}
