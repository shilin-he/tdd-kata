using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;
using max.web.owin;
using Microsoft.Owin;

namespace max.web.specs
{

  [Subject(typeof(OwinRequestWrapper))]
  public class OwinRequestWrapperSpecs
  {
    public abstract class concern : Observes<OwinRequestWrapper>
    {

    }

    public class when_get_path_of_the_request : concern
    {
      Establish c = () =>
      {
        path = "/foo/bar";
        path_string = new PathString(path);
        depends.on<IFindRoutesThatMatchRequests>();
        owin_request = depends.on<IOwinRequest>();
        owin_request.setup(x => x.Path).Return(path_string);
      };

      Because b = () =>
        result = sut.path;

      It returns_the_request_path = () =>
        result.ShouldEqual(path);

      static string result;
      static string path;
      static IOwinRequest owin_request;
      static PathString path_string;
    }

    public class when_get_the_method_of_the_request : concern
    {
      Establish c = () =>
      {
        method = "GET";
        owin_request = depends.on<IOwinRequest>();
        owin_request.setup(x => x.Method).Return(method);
      };

      Because b = () =>
        result = sut.method;

      It returns_the_request_method = () =>
        result.ShouldEqual(method);

      static string result;
      static string method;
      static IOwinRequest owin_request;
    }

    public class when_map_request_to_a_input_model : concern
    {
      Establish c = () =>
      {
        var foo = "fooooooooo";
        var bar = new DateTime(2099, 1, 1);
        input_model = new AnItem() { foo = foo, bar = bar };
        form_collection = fake.an<IFormCollection>();
        form_collection.setup(x => x.Get("foo")).Return(foo);
        form_collection.setup(x => x.Get("bar")).Return(bar.ToString(CultureInfo.InvariantCulture));
        owin_request = depends.on<IOwinRequest>();
        owin_request.setup(x => x.ReadFormAsync()).Return(Task.FromResult(form_collection));
      };

      Because b = () =>
        result = sut.map<AnItem>();

      It instantiates_an_instance_of_the_input_model_using_data_from_the_request = () =>
      {
        result.foo.ShouldEqual(input_model.foo);
        result.bar.ShouldEqual(input_model.bar);
      };

      static AnItem result;
      static AnItem input_model;
      static IOwinRequest owin_request;
      static IFormCollection form_collection;
    }

  }
}
