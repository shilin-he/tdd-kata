namespace max.web
{
  public class WebCommand : IProcessOneWebRequest
  {
    readonly IImplementOneAppFeature feature;
    readonly IMatchOneRequest request_spec;

    public WebCommand(IMatchOneRequest request_spec, IImplementOneAppFeature feature)
    {
      this.request_spec = request_spec;
      this.feature = feature;
    }

    public IContainResponseInfo process(IContainInfoForOneWebRequest the_request)
    {
      return feature.process(the_request);
    }

    public bool can_process(IContainInfoForOneWebRequest the_request)
    {
      return request_spec(the_request);
    }
  }
}