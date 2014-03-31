using System.Collections.Generic;

namespace tdd_kata
{
  public interface IPrintExpressions
  {
    string write(IEnumerable<IToken> output_tokens);
  }
}