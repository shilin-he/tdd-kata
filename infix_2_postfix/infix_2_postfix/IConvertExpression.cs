using System.Collections.Generic;

namespace tdd_kata
{
  public interface IConvertExpression
  {
    IEnumerable<IToken> convert(IEnumerable<IToken> input_tokens);
  }
}