 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using max.web.razor;

namespace max.web.specs
{   

  [Subject(typeof(RazorTempalteRenderEngine))]
  public class RazorTempalteRenderEngineSpecs
  {
    public abstract class concern : Observes<RazorTempalteRenderEngine>
    {
        
    }

    public class when_render : concern
    {
      Establish c = () =>
      {
        template = fake.an<IContainTemplateInfo>();
        template.setup(x => x.content).Return("blah: @Model.foo -- @Model.bar");
        view_model = new AnItem {foo = "foo", bar = new DateTime(2014, 1, 1)};
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
