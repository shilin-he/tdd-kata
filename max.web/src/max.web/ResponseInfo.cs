namespace max.web
{
  public class ResponseInfo : IContainResponseInfo
  {
    public ResponseInfo(string content)
    {
      this.content = content;
    }

    public string content { get; private set; }
  }
}