using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class InfixExpressionStringParserSpecs
  {
    class concern : Observes<InfixExpressionStringParser>
    {
    }

    class when_reading : concern
    {
      static IEnumerable<IToken> result;
      static IEnumerable<IToken> output;
      static string expr_str;

      It should_convert_the_input_string_into_a_list_of_tokens = () =>
      {
        List<IToken> temp = result.ToList();
        temp.Count.ShouldEqual(7);
        temp[0].ShouldBeOfType<Operand>();
        temp[3].ShouldBeOfType<Operand>();
        temp[5].ShouldBeOfType<Operand>();
        temp[1].ShouldBeOfType<IOperator>();
        temp[4].ShouldBeOfType<IOperator>();
        temp[2].ShouldBeOfType<LeftParenthesis>();
        temp[6].ShouldBeOfType<RightParenthesis>();
      };

      Establish c = () =>
      {
        expr_str = "3 * (2 + 1)";
        depends.on<ICreateOperandToken>(x => new Operand("1"));
        depends.on<ICreateOperatorToken>(x => fake.an<IOperator>());
        depends.on<ICreateLeftParenthesisToken>(() => new LeftParenthesis());
        depends.on<ICreateRightParenthesisToken>(() => new RightParenthesis());
      };

      Because b = () =>
        result = sut.read(expr_str);
    }
  }
}