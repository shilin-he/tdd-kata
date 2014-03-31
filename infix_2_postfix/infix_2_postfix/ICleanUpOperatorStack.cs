using System.Collections.Generic;

namespace tdd_kata
{
  public interface ICleanUpOperatorStack
  {
    IEnumerable<IToken> clean_up();
  }
}