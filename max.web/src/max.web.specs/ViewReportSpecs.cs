 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace max.web.specs
{   

  [Subject(typeof(ViewReport<AnItem>))]
  public class ViewReportSpecs
  {
    public abstract class concern : Observes<ViewReport<AnItem>>
    {
        
    }

    public class when_process_a_request : concern
    {
      Establish c = () =>
      {
        the_request = fake.an<IContainRequestInfo>();
        report = new AnItem();
        depends.on<IGetAReportUsingARequest<AnItem>>(x =>
        {
          x.ShouldEqual(the_request);
          return report;
        });

        display_result = "blah...";
        display_engine = depends.on<IDisplayInformation>();
        display_engine.setup(x => x.render(report)).Return(display_result);

        response = fake.an<IContainResponseInfo>();
        depends.on<ICreateResponses>(data =>
        {
          data.ShouldEqual(display_result);
          return response;
        });
      };

      Because b = () =>
        result = sut.process(the_request);

      It returns_a_response = () =>
        result.ShouldEqual(response);

      static AnItem report;
      static IContainRequestInfo the_request;
      static IDisplayInformation display_engine;
      static string display_result;
      static IContainResponseInfo response;
      static IContainResponseInfo result;
    }
  }
}
