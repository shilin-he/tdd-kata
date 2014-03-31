namespace tdd_kata
{
  public interface IOperator : IToken
  {
    int precedence { get; }
    Associativity associativity { get; }
  }
}