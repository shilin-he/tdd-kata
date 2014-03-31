using System.Collections.Generic;

namespace tdd_kata
{
  public class OperandHandler : IHandleAToken
  {
    public IEnumerable<IToken> handle(IToken input_token)
    {
      return new[] {input_token};
    }
  }
}