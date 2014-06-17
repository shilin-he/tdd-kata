 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using max.web.razor;

namespace max.web.specs
{   

  [Subject(typeof(ViewCollection))]
  public class ViewCollectionSpecs
  {
    public abstract class concern : Observes<ViewCollection>
    {
        
    }

    public class when_add_view_from_file : concern
    {
      Establish c = () =>
      {
        template_path = "/path/to/template";
      };

      Because b = () =>
      {
        sut.add_view_from_file(typeof(AnItem), template_path);
        result = sut[typeof(AnItem)];
      };

      It adds_a_view_model_and_template_pair_to_the_collection = () =>
      {
        result.ShouldNotBeNull();
        result.ShouldBeOfType<RazorFileTemplate>();
      };

      static IContainTemplateInfo result;
      static string template_path;
    }
  }
}
