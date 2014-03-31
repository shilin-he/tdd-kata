using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class OperatorHandlerSpecs
  {
    class concern : Observes<OperatorHandler>
    {
    }

    class when_handling : concern
    {
      class while_there_is_an_operator_token_o2_at_the_top_of_the_stack
      {
        class and_o2_has_higher_precedence
        {
          static IOperator multiplication_operator;
          static Stack<object> operator_stack;
          static IEnumerable<IToken> output;
          static IEnumerable<IToken> result;
          static IOperator the_operator;

          Because b = () =>
            result = sut.handle(the_operator);

          Establish c = () =>
          {
            the_operator = fake.an<IOperator>();
            the_operator.setup(x => x.precedence).Return(2);
            multiplication_operator = fake.an<IOperator>();
            multiplication_operator.setup(x => x.precedence).Return(3);
            operator_stack = new Stack<object>();
            operator_stack.Push(multiplication_operator);
            depends.on<IProvideOperatorStack>(() => operator_stack);
            output = new[] {multiplication_operator};
          };

          It should_pop_o2_off_the_stack_onto_the_output_queue_and_push_the_current_operator_to_the_stack = () =>
          {
            result.ShouldEqual(output);
            operator_stack.ShouldContainOnly(the_operator);
          };
        }

        class and_the_current_operator_is_left_associative_and_its_precedence_is_equal_to_that_of_o2
        {
          static IOperator addition_operator;
          static Stack<object> operator_stack;
          static IEnumerable<IToken> output;
          static IEnumerable<IToken> result;
          static IOperator the_operator;

          Because b = () =>
            result = sut.handle(the_operator);

          Establish c = () =>
          {
            the_operator = fake.an<IOperator>();
            the_operator.setup(x => x.precedence).Return(2);
            the_operator.setup(x => x.associativity).Return(Associativity.Left);
            addition_operator = fake.an<IOperator>();
            addition_operator.setup(x => x.precedence).Return(2);
            operator_stack = new Stack<object>();
            operator_stack.Push(addition_operator);
            depends.on<IProvideOperatorStack>(() => operator_stack);
            output = new[] {addition_operator};
          };

          It should_pop_o2_off_the_stack_onto_the_output_queue_and_push_the_current_operator_to_the_stack = () =>
          {
            result.ShouldEqual(output);
            operator_stack.ShouldContainOnly(the_operator);
          };
        }
      }
    }
  }
}