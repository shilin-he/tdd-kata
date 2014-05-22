 using System.Diagnostics.CodeAnalysis;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace max.web.specs
{   

  [Subject(typeof(WebHandler))]
  public class WebHandlerSpecs
  {
    public abstract class concern : Observes<IHandleWebRequests, WebHandler>
    {
        
    }

    public class when_handling_web_requests : concern
    {
      Establish c = () =>
      {
        command = fake.an<IProcessOneWebRequest>();
        the_request = fake.an<IContainInfoForOneWebRequest>();
        command_registry = depends.on<IFindCommandsToProcessRequests>();
        command_registry.setup(x => x.find_command_can_handle(the_request)).Return(command);
      };

      Because b = () =>
        sut.handle(the_request);

      It dispatches_a_command_to_process_the_request = () =>
        command.received(x => x.process(the_request));

      static IProcessOneWebRequest command;
      static IContainInfoForOneWebRequest the_request;
      static IFindCommandsToProcessRequests command_registry;
    }
  }
}
