using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class LeftParenthesisHandler : IHandleAToken
  {
    readonly IProvideOperatorStack get_operator_stack;

    public LeftParenthesisHandler(IProvideOperatorStack get_operator_stack)
    {
      this.get_operator_stack = get_operator_stack;
    }

    public IEnumerable<IToken> handle(IToken input_token)
    {
      get_operator_stack().Push(input_token);
      return Enumerable.Empty<IToken>();
    }
  }
}