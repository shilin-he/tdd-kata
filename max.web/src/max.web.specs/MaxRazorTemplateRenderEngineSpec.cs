using System;
using System.Reflection;
using developwithpassion.specifications.extensions;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using max.web.razor;

namespace max.web.specs
{

  [Subject(typeof(MaxRazorTemplateRenderEngine))]
  public class MaxRazorTemplateRenderEngineSpec
  {
    public abstract class concern : Observes<MaxRazorTemplateRenderEngine>
    {

    }

    public class when_compile_template : concern
    {
      Establish c = () =>
      {
        template = fake.an<IContainTemplateInfo>();
        template.setup(x => x.content).Return("blah: @Model.foo -- @Model.bar");
        view_model = new AnItem { foo = "foo", bar = new DateTime(2014, 1, 1) };
        rendered_template = "blah: foo -- 2014-01-01 12:00:00 AM";
      };

      Because b = () =>
        result = sut.render(template, view_model);

      It returns_rendered_template = () =>
        result.ShouldEqual(rendered_template);

      static string result;
      static string rendered_template;
      static AnItem view_model;
      static IContainTemplateInfo template;
    }
  }
}
