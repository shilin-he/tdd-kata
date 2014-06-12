using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using Machine.Fakes;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;
using Microsoft.Owin;
using Rhino.Mocks;

namespace max.web.specs
{

  [Subject(typeof(OwinResponseWrapper))]
  public class OwinResponseWrapperSpecs
  {
    public abstract class concern : Observes<OwinResponseWrapper>
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

    public class when_set_redirect_url : concern
    {
      Establish c = () =>
      {
        location = "/foo/bar";
        owin_response = depends.on<IOwinResponse>();
      };

      Because b = () =>
        sut.redirect_url = location;

      It calls_the_redirec_method_of_the_owin_response = () =>
        owin_response.received(x => x.Redirect(location));

      static IOwinResponse owin_response;
      static string location;
    }

    public class when_set_content_type : concern
    {
      Establish c = () =>
      {
        content_type = "text/html";
        owin_response = depends.on<IOwinResponse>();
      };

      Because b = () =>
        sut.content_type = content_type;

      It sets_content_type_of_the_owin_response = () =>
      {
        owin_response.AssertWasCalled(x => x.ContentType = content_type);
      };

      static IOwinResponse owin_response;
      static string content_type;
    }

    public class when_write : concern
    {
      Establish c = () =>
      {
        owin_response = depends.on<IOwinResponse>();
        content = "blah";
      };

      Because b = () =>
        sut.write();

      It calls_write_async_method_of_the_owin_response = () =>
        owin_response.received(x => x.WriteAsync(sut.content));

      static IOwinResponse owin_response;
      static string content;
    }

  }
}
