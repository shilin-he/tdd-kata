using System.Collections.Generic;
using System.Linq;

namespace max.web
{
  public class CommandRegistry : IFindCommandsToProcessRequests
  {
    readonly IEnumerable<IProcessOneWebRequest> commands;
    readonly ICreateOneSpecialCaseWhenACommandIsNotFound special_case_command_factory;

    public CommandRegistry(IEnumerable<IProcessOneWebRequest> commands,
      ICreateOneSpecialCaseWhenACommandIsNotFound special_case_command_factory)
    {
      this.commands = commands;
      this.special_case_command_factory = special_case_command_factory;
    }

    public IProcessOneWebRequest find_command_can_handle(IContainInfoForOneWebRequest the_request)
    {
      IProcessOneWebRequest command = commands.FirstOrDefault(x => x.can_process(the_request));
      return command ?? special_case_command_factory(the_request);
    }
  }
}