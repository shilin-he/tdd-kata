namespace tdd_kata
{
  public class RightParenthesis : IToken
  {
    public string write()
    {
      return value;
    }

    public string value
    {
      get { return ")"; }
    }
  }
}