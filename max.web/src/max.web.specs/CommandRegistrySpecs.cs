using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace max.web.specs
{

  [Subject(typeof(CommandRegistry))]
  public class CommandRegistrySpecs
  {
    public abstract class concern : Observes<CommandRegistry>
    {

    }

    public class when_find_a_command_to_handle_a_request : concern
    {
      class and_the_registry_has_a_command_which_can_handle_the_request
      {
        Establish c = () =>
        {
          the_request = fake.an<IContainRequestInfo>();
          the_command = fake.an<IProcessOneWebRequest>();
          the_command.setup(x => x.can_process(the_request)).Return(true);
          commands = Enumerable.Range(1, 10).Select(x => fake.an<IProcessOneWebRequest>()).ToList();
          commands.Add(the_command);
          depends.on<IEnumerable<IProcessOneWebRequest>>(commands);
        };

        Because b = () =>
          result = sut.find_command_can_handle(the_request);

        It returns_the_command = () =>
          result.can_process(the_request).ShouldBeTrue();

        static IProcessOneWebRequest result;
        static IContainRequestInfo the_request;
        static IProcessOneWebRequest the_command;
        static IList<IProcessOneWebRequest> commands;
      }

      class and_the_registry_does_not_have_a_command_which_can_handle_the_request
      {
        Establish c = () =>
        {
          the_request = fake.an<IContainRequestInfo>();
          special_case_command = fake.an<IProcessOneWebRequest>();
          commands = Enumerable.Range(1, 10).Select(x => fake.an<IProcessOneWebRequest>()).ToList();
          depends.on<IEnumerable<IProcessOneWebRequest>>(commands);
          depends.on<ICreateOneSpecialCaseWhenACommandIsNotFound>(x =>
          {
            x.ShouldEqual(the_request);
            return special_case_command;
          });
        };

        Because b = () =>
          result = sut.find_command_can_handle(the_request);

        It returns_the_result_of_running_the_special_case_handler = () =>
          result.ShouldEqual(special_case_command);

        static IProcessOneWebRequest result;
        static IContainRequestInfo the_request;
        static IList<IProcessOneWebRequest> commands;
        static IProcessOneWebRequest special_case_command;
      }
    }
  }
}
