namespace tdd_kata
{
  public class OperatorToken : IToken
  {
    readonly Associativity _associativity;
    readonly int _precedence;
    readonly string _symbol;

    public OperatorToken(string value, int precedence, Associativity associativity)
    {
      _symbol = symbol;
      _precedence = precedence;
      _associativity = associativity;
      this.value = value;
    }

    public string symbol
    {
      get { return _symbol; }
    }

    public int precedence
    {
      get { return _precedence; }
    }

    public Associativity associativity
    {
      get { return _associativity; }
    }

    public string write()
    {
      return value;
    }

    public string value { get; private set; }
  }
}