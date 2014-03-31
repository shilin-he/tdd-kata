using System.Collections.Generic;

namespace tdd_kata
{
  public interface IProcessAToken
  {
    IEnumerable<IToken> process(IToken input);
    bool can_process(IToken input);
  }
}