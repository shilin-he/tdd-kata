 using System.Collections.Generic;
 using System.Linq;
 using System.Net.Cache;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace max.web.specs
{   

  [Subject(typeof(WebCommandCollection))]
  public class WebCommandCollectionSpecs
  {
    public abstract class concern : Observes<WebCommandCollection>
    {
        
    }

    public class when_add_a_command : concern
    {

      Establish c = () =>
      {
        request_spec = fake.an<IMatchOneRequest>();
        feature = fake.an<IImplementOneAppFeature>();
      };

      Because b = () =>
      {
        sut.add_command(request_spec, feature);
        result = sut.First();
      };

      It adds_a_command_to_the_collection = () =>
      {
        result.ShouldNotBeNull();
        result.ShouldBeOfType<WebCommand>();
      };

      static IProcessOneWebRequest result;
      static IImplementOneAppFeature feature;
      static IMatchOneRequest request_spec;
    }

    public class when_add_a_view_command : concern
    {
      Establish c = () =>
      {
        resource = "people";
        operation = "view";
        report = new AnItem();
        request = fake.an<IContainRequestInfo>();
        request.setup(x => x.method).Return("get");
        query = req =>
        {
          req.ShouldEqual(request);
          return report;
        };
        content = "blah";
        display_engine = depends.on<IDisplayInformation>();
        display_engine.setup(x => x.render(report)).Return(content);

        route_parameters = new Dictionary<string, string>
        {
          {"resource", "people"},
          {"operation", "view"}
        };
        route = fake.an<IContainRouteInfo>();
        route.setup(x => x.get_route_parameters(request)).Return(route_parameters);

        route_registry = depends.on<IFindRoutesThatMatchRequests>();
        route_registry.setup(x => x.find_route_match(request)).Return(route);
      };

      Because b = () =>
      {
        sut.add_view_command(resource, operation, query);
        result = sut.First();
      };

      It adds_a_commond_that_handles_a_view_request_to_the_collection = () =>
      {
        result.can_process(request).ShouldBeTrue();
        var resp = result.process(request);
        resp.content.ShouldEqual(content);
      };

      static IGetAReportUsingARequest<AnItem> query;
      static IContainRequestInfo request;
      static AnItem report;
      static IProcessOneWebRequest result;
      static IDisplayInformation display_engine;
      static string content;
      static string resource;
      static string operation;
      static IFindRoutesThatMatchRequests route_registry;
      static IContainRouteInfo route;
      static IDictionary<string, string> route_parameters;
    }

    public class when_add_an_edit_command : concern
    {
      Establish c = () =>
      {
        path = "/request/path";
        redirect_url = "/anitem/11";
        request = fake.an<IContainRequestInfo>();
        request.setup(x => x.method).Return("post");
        request.setup(x => x.path).Return(path);
        data_handler = req =>
        {
          req.ShouldEqual(request);
          return 11;
        };
        redirect_url_factory = (req, output) =>
        {
          req.ShouldEqual(request);
          output.ShouldEqual(11);
          return redirect_url;
        };
      };

      Because b = () =>
      {
        sut.add_edit_command(path, data_handler, redirect_url_factory);
        result = sut.First();
      };

      It adds_a_command_that_handles_an_edit_request_to_the_collection = () =>
      {
        result.can_process(request).ShouldBeTrue();
        var response = result.process(request);
        response.redirect_url.ShouldEqual(redirect_url);
      };

      static IProcessOneWebRequest result;
      static IContainRequestInfo request;
      static string redirect_url;
      static string path;
      static ICreateRedirectUrls<object> redirect_url_factory;
      static IProcessDataModels<object> data_handler;
    } 
  }
}
