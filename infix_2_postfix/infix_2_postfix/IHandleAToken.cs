using System.Collections.Generic;

namespace tdd_kata
{
  public interface IHandleAToken
  {
    IEnumerable<IToken> handle(IToken input_token);
  }
}