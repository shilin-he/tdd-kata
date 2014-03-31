using System.Collections.Generic;

namespace tdd_kata
{
  public class TokenProcessor : IProcessAToken
  {
    readonly IDetermineIfCanProcessAToken can_process_a_token;
    readonly IHandleAToken handler;

    public TokenProcessor(IDetermineIfCanProcessAToken can_process_a_token, IHandleAToken handler)
    {
      this.can_process_a_token = can_process_a_token;
      this.handler = handler;
    }

    public IEnumerable<IToken> process(IToken input)
    {
      return handler.handle(input);
    }

    public bool can_process(IToken input)
    {
      return can_process_a_token(input);
    }
  }
}