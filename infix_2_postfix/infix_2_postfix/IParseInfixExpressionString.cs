using System.Collections.Generic;

namespace tdd_kata
{
  public interface IParseInfixExpressionString
  {
    IEnumerable<IToken> read(string infix_str);
  }
}