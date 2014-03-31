using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class OperatorHandler : IHandleAToken
  {
    readonly IProvideOperatorStack get_operator_stack;

    public OperatorHandler(IProvideOperatorStack get_operator_stack)
    {
      this.get_operator_stack = get_operator_stack;
    }

    public IEnumerable<IToken> handle(IToken input_token)
    {
      var output = new List<IToken>();

      var current_operator = input_token as IOperator;
      Stack<object> operator_stack = get_operator_stack();
      while (operator_stack.Any())
      {
        var op = operator_stack.Peek() as IOperator;
        if (op != null && (current_operator.precedence < op.precedence
                           ||
                           current_operator.associativity == Associativity.Left &&
                           current_operator.precedence == op.precedence))
          output.Add((IToken) operator_stack.Pop());
        else
          break;
      }
      operator_stack.Push(current_operator);
      return output;
    }
  }
}