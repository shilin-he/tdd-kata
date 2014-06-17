using System.Runtime.InteropServices;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace max.web.specs
{

  [Subject(typeof(EditModel<AnItem>))]
  public class EditModelSpecs
  {
    public abstract class concern : Observes<EditModel<AnItem>>
    {

    }

    public class when_process : concern
    {
      Establish c = () =>
      {
        input_model = new AnItem();
        output_model = new AnItem();

        request = fake.an<IContainRequestInfo>();

        data_handler = depends.on<IProcessDataModels<AnItem>>(req =>
        {
          req.ShouldEqual(request);
          return output_model;
        });

        redirect_url = "/people/11";
        redirect_url_factory = depends.on<ICreateRedirectUrls<AnItem>>((req, ouput) =>
        {
          req.ShouldEqual(request);
          ouput.ShouldEqual(output_model);
          return redirect_url;
        });

        response = fake.an<IContainResponseInfo>();
        response_factory = depends.on<ICreateResponses>(content =>
        {
          content.ShouldEqual(redirect_url);
          return response;
        });
      };

      Because b = () =>
        result = sut.process(request);

      It generates_the_response = () =>
        result.ShouldEqual(response);

      static IContainResponseInfo result;
      static IContainResponseInfo response;
      static IContainRequestInfo request;
      static AnItem input_model;
      static IProcessDataModels<AnItem> data_handler;
      static AnItem output_model;
      static ICreateResponses response_factory;
      static string redirect_url;
      static ICreateRedirectUrls<AnItem> redirect_url_factory;
    }
  }
}
