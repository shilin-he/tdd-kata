namespace max.web
{
  public interface IFindCommandsToProcessRequests
  {
    IProcessOneWebRequest find_command_can_handle(IContainInfoForOneWebRequest the_request);
  }
}