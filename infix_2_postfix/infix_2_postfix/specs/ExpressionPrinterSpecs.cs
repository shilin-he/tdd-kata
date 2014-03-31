using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class ExpressionPrinterSpecs
  {
    class convern : Observes<ExpressionPrinter>
    {
    }

    class when_writing : convern
    {
      static string result;
      static IEnumerable<IToken> tokens;
      static IToken one;
      static IToken two;
      static IToken three;
      static IToken add;
      static IToken multiply;
      static IToken left_parenthesis;
      static IToken right_parenthesis;

      It should_output_the_string_representation_of_the_expression = () =>
        result.ShouldEqual("3 * (2 + 1)");

      Establish c = () =>
      {
        one = new Operand("1");
        two = new Operand("2");
        three = new Operand("3");
        add = new Operator(2, Associativity.Left, "+");
        multiply = new Operator(3, Associativity.Left, "*");
        left_parenthesis = new LeftParenthesis();
        right_parenthesis = new RightParenthesis();
        tokens = new List<IToken>
        {
          three,
          multiply,
          left_parenthesis,
          two,
          add,
          one,
          right_parenthesis
        };
      };

      Because b = () =>
        result = sut.write(tokens);
    }
  }
}