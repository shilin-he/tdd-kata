namespace tdd_kata
{
  public class Operand : IToken
  {
    public Operand(string value)
    {
      this.value = value;
    }

    public string value { get; private set; }

    public string write()
    {
      return value;
    }
  }
}