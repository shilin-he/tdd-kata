using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class LeftParenthesisHandlerSpecs
  {
    class concern : Observes<LeftParenthesisHandler>
    {
    }

    class when_handling : concern
    {
      static IEnumerable<IToken> result;
      static IEnumerable<IToken> output;
      static Stack<object> operator_stack;
      static IToken left_parenthesis;

      Establish c = () =>
      {
        left_parenthesis = fake.an<IToken>();
        operator_stack = new Stack<object>();
        depends.on<IProvideOperatorStack>(() => operator_stack);
      };

      Because b = () =>
        result = sut.handle(left_parenthesis);

      It shuold_produce_nothing_and_put_the_left_parenthesis_onto_the_operator_stack = () =>
      {
        result.ShouldBeEmpty();
        operator_stack.ShouldContainOnly(left_parenthesis);
      };
    }
  }
}