using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class PostOperationHandler : ICleanUpOperatorStack
  {
    readonly IProvideOperatorStack get_operator_stack;

    public PostOperationHandler(IProvideOperatorStack get_operator_stack)
    {
      this.get_operator_stack = get_operator_stack;
    }

    public IEnumerable<IToken> clean_up()
    {
      var output = new List<IToken>();
      Stack<object> operator_stack = get_operator_stack();
      while (operator_stack.Any())
        output.Add((IToken) operator_stack.Pop());
      return output;
    }
  }
}