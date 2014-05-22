 using System.CodeDom;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using max.web.razor;

namespace max.web.specs
{   

  [Subject(typeof(DisplayEngine))]
  public class DisplayEngineSpecs
  {
    public abstract class concern : Observes<DisplayEngine>
    {
        
    }

    public class when_render_a_view_model : concern
    {
      Establish c = () =>
      {
        // step 1: find the view template;
        // step 2: generate the view result from the template using the view model passed in.

        template = fake.an<IContainTemplateInfo>();
        view_template_registry = depends.on<IFindViewTemplates>();
        view_template_registry.setup(x => x.find_view_template_for<AnItem>()).Return(template);

        view_model = new AnItem();
        rendered_template = "blah";
        template_render_engine = depends.on<IRenderTemplates>();
        template_render_engine.setup(x => x.render(template, view_model)).Return(rendered_template);
      };

      Because b = () =>
        result = sut.render(view_model);

      It returns_the_rendering_result = () =>
        result.ShouldEqual(rendered_template);

      static string result;
      static string rendered_template;
      static AnItem view_model;
      static IFindViewTemplates view_template_registry;
      static IContainTemplateInfo template;
      static IRenderTemplates template_render_engine;
    }
  }
}
