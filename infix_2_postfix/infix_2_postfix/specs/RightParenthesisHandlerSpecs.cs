using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class RightParenthesisHandlerSpecs
  {
    class and_no_left_parenthesis_in_the_operator_stack
    {
    }

    class concern : Observes<RightParenthesisHandler>
    {
    }

    class when_handling : concern
    {
      class until_the_token_at_the_top_of_the_stack_is_a_left_parenthesis
      {
        static IToken input;
        static Stack<object> operator_stack;
        static IEnumerable<IToken> output;
        static IEnumerable<IToken> result;
        static IOperator token1;
        static IOperator token2;

        Because b = () =>
          result = sut.handle(input);

        Establish c = () =>
        {
          input = fake.an<IToken>();
          token1 = fake.an<IOperator>();
          token2 = fake.an<IOperator>();
          operator_stack = new Stack<object>();
          operator_stack.Push(new LeftParenthesis());
          operator_stack.Push(token1);
          operator_stack.Push(token2);
          depends.on<IProvideOperatorStack>(() => operator_stack);
          output = new[] {token2, token1};
        };

        It should_pop_operators_off_the_stack_onto_the_output_queue = () =>
          result.ShouldEqual(output);

        It should_pop_the_left_parenthesis_from_the_stack_at_last = () =>
          operator_stack.ShouldBeEmpty();
      }
    }
  }
}