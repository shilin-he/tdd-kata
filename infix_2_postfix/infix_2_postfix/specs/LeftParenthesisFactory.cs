namespace tdd_kata.specs
{
  public class LeftParenthesisFactory
  {
    static readonly LeftParenthesis instance = new LeftParenthesis();

    public static IToken create()
    {
      return instance;
    }
  }
}