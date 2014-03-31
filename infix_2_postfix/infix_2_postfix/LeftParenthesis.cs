namespace tdd_kata
{
  public class LeftParenthesis : IToken
  {
    public string write()
    {
      return value;
    }

    public string value
    {
      get { return "("; }
    }
  }
}