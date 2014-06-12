using System.Runtime.InteropServices;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace max.web.specs
{

  [Subject(typeof(UpdateModel<AnItem>))]
  public class UpdateModelSpecs
  {
    public abstract class concern : Observes<UpdateModel<AnItem>>
    {

    }

    public class when_process : concern
    {
      Establish c = () =>
      {
        input_model = new AnItem();
        output_model = new AnItem();

        request = fake.an<IContainRequestInfo>();
        request.setup(x => x.map<AnItem>()).Return(input_model);

        data_handler = depends.on<IProcessDataModels<AnItem>>(input =>
        {
          input.ShouldEqual(input_model);
          return output_model;
        });

        redirect_url = "/people/11";
        redirect_url_factory = depends.on<ICreateRedirectUrls<AnItem>>(model =>
        {
          model.ShouldEqual(output_model);
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
