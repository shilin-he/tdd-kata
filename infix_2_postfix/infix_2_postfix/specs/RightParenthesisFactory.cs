namespace tdd_kata.specs
{
  public class RightParenthesisFactory
  {
    static readonly RightParenthesis instance = new RightParenthesis();

    public static IToken create()
    {
      return instance;
    }
  }
}