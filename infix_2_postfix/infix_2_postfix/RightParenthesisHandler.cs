using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class RightParenthesisHandler : IHandleAToken
  {
    readonly IProvideOperatorStack get_operator_stack;

    public RightParenthesisHandler(IProvideOperatorStack get_operator_stack)
    {
      this.get_operator_stack = get_operator_stack;
    }

    public IEnumerable<IToken> handle(IToken input_token)
    {
      Stack<object> operator_stack = get_operator_stack();
      var output = new List<IToken>();
      while (operator_stack.Any())
      {
        object op = operator_stack.Peek();
        if (op is LeftParenthesis)
        {
          operator_stack.Pop();
          break;
        }
        output.Add((IToken) operator_stack.Pop());
      }

      return output;
    }
  }
}