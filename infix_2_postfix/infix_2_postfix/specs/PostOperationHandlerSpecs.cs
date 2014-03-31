using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class PostOperationHandlerSpecs
  {
    class concern : Observes<PostOperationHandler>
    {
    }

    class when_cleaning_up : concern
    {
      class and_there_is_no_parenthesis_in_the_stack
      {
        static Stack<object> operator_stack;
        static IEnumerable<IToken> output;
        static IEnumerable<IToken> result;
        static OperatorToken token1;
        static OperatorToken token2;

        Because b = () =>
          result = sut.clean_up();

        Establish c = () =>
        {
          token1 = new OperatorToken("*", 3, Associativity.Left);
          token2 = new OperatorToken("*", 3, Associativity.Left);
          operator_stack = new Stack<object>();
          operator_stack.Push(token1);
          operator_stack.Push(token2);
          output = new[] {token2, token1};
          depends.on<IProvideOperatorStack>(() => operator_stack);
        };

        It should_get_the_rest_operators_in_the_operator_stack = () =>
          result.ShouldEqual(output);
      }

      class and_find_parenthesis_in_the_stack
      {
      }
    }
  }
}