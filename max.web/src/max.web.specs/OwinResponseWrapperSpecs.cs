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


    public class when_finish : concern
    {
      public class and_no_redirect
      {

        Establish c = () =>
        {
          owin_response = fake.an<IOwinResponse>();
          content_type = "text/plain";
          content = "blah";
        };

        Because b = () =>
        {
          sut.content = content;
          sut.content_type = content_type;
          sut.finish(owin_response);
        };

        It calls_owin_response_methods = () =>
        {
          owin_response.received(x => x.WriteAsync(sut.content));
          owin_response.AssertWasCalled(x => x.ContentType = content_type);
          owin_response.AssertWasNotCalled(x => x.Redirect(sut.redirect_url));
        };

        static IOwinResponse owin_response;
        static string content;
        static string content_type;
      }

      public class and_redirect
      {

        Establish c = () =>
        {
          owin_response = fake.an<IOwinResponse>();
          content_type = "text/plain";
          content = "blah";
        };

        Because b = () =>
        {
          sut.content = content;
          sut.content_type = content_type;
          sut.redirect_url = "/redirect/to";
          sut.finish(owin_response);
        };

        It calls_owin_response_methods = () =>
        {
          owin_response.received(x => x.WriteAsync(sut.content));
          owin_response.AssertWasCalled(x => x.ContentType = content_type);
          owin_response.received(x => x.Redirect(sut.redirect_url));
        };

        static IOwinResponse owin_response;
        static string content;
        static string content_type;
      }
    }

  }
}
