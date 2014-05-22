namespace max.web
{
  public interface IImplementOneAppFeature
  {
    IContainResponseInfo process(IContainInfoForOneWebRequest the_request);
  }
}