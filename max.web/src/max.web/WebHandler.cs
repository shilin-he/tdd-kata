namespace max.web
{
  public class WebHandler : IHandleWebRequests
  {
    readonly IFindCommandsToProcessRequests command_registery;

    public WebHandler(IFindCommandsToProcessRequests command_registery)
    {
      this.command_registery = command_registery;
    }

    public IContainResponseInfo handle(IContainRequestInfo the_request)
    {
      IProcessOneWebRequest command = command_registery.find_command_can_handle(the_request);
      return command.process(the_request);
    }
  }
}