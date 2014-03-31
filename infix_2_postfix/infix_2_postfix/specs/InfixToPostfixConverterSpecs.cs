using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class InfixToPostfixConverterSpecs
  {
    class concern : Observes<InfixToPostfixConverter>
    {
    }

    class when_converting : concern
    {
      static string result;
      static string postfix_str;
      static string infix_str;
      static IParseInfixExpressionString parse_infix_expression_string;
      static IPrintExpressions print_expressions;
      static IEnumerable<IToken> input_tokens;
      static IEnumerable<IToken> output_tokens;
      static IConvertExpression converter;

      It should_convert_the_infix_expression_to_postfix_expression = () =>
        result.ShouldEqual(postfix_str);

      Establish c = () =>
      {
        infix_str = "3 + 5";
        postfix_str = "3 5 +";
        parse_infix_expression_string = depends.on<IParseInfixExpressionString>();
        input_tokens = Enumerable.Range(1, 3).Select(x => fake.an<IToken>());
        parse_infix_expression_string.setup(x => x.read(infix_str)).Return(input_tokens);
        print_expressions = depends.on<IPrintExpressions>();
        print_expressions.setup(x => x.write(output_tokens)).Return(postfix_str);
        converter = depends.on<IConvertExpression>();
        converter.setup(x => x.convert(input_tokens)).Return(output_tokens);
      };

      Because b = () =>
        result = sut.convert(infix_str);
    }
  }
}