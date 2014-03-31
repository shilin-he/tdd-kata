using System;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class OperatorFactorySpecs
  {
    class concern : Observes
    {
    }

    class when_creating : concern
    {
      class and_the_symbol_is_valid
      {
        static Operator result;

        Because b = () =>
          result = OperatorFactory.create('*');

        It should_create_a_operator_using_the_symbol_passed_in = () =>
        {
          result.value.ShouldEqual("*");
          result.associativity.ShouldEqual(Associativity.Left);
          result.precedence.ShouldEqual(3);
        };
      }

      class and_the_symbol_is_invalid
      {
        Because b = () =>
          spec.catch_exception(() => OperatorFactory.create('$'));

        It should_throw_an_argument_exception = () =>
          spec.exception_thrown.ShouldBeOfType<ArgumentException>();
      }
    }
  }
}