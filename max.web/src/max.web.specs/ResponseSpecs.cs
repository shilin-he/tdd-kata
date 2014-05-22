 using System.Net.Mime;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace max.web.specs
{   

  [Subject(typeof(ResponseInfo))]
  public class ResponseSpecs
  {
    public abstract class concern : Observes<ResponseInfo>
    {
        
    }

    public class when_get_content : concern
    {
      Establish c = () =>
      {
        content = "test content";
        depends.on(content);
      };

      Because b = () =>
        result = sut.content;

      It returns_content_passed_in_from_constructor = () =>
        result.ShouldEqual(content);

      static string result;
      static string content;
    }
  }
}
