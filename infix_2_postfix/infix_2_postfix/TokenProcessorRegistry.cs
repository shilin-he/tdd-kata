using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class TokenProcessorRegistry : IFindTokenProcessors
  {
    readonly IEnumerable<IProcessAToken> processors;

    public TokenProcessorRegistry(IEnumerable<IProcessAToken> processors)
    {
      this.processors = processors;
    }

    public IProcessAToken find_processor(IToken token_to_process)
    {
      return processors.First(x => x.can_process(token_to_process));
    }
  }
}