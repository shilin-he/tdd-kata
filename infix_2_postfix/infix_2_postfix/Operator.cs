namespace tdd_kata
{
  public class Operator : IOperator
  {
    public Operator(int precedence, Associativity associativity, string value)
    {
      this.precedence = precedence;
      this.associativity = associativity;
      this.value = value;
    }

    public int precedence { get; private set; }
    public Associativity associativity { get; private set; }
    public string value { get; private set; }

    public string write()
    {
      return value;
    }
  }
}