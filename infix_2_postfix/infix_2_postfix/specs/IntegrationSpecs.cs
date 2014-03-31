using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class IntegrationSpecs
  {
    class concern : Observes
    {
    }

    class when_testing : concern
    {
      static string result;
      static Stack<object> operator_stack;
      static InfixToPostfixConverter infix_to_postfix_converter;
      static TokenProcessorRegistry processor_registry;

      It should_convert_infix_expression_to_postfix_express = () =>
        result.ShouldEqual("3 2 1 + *");

      Establish c = () =>
      {
        var parser = new InfixExpressionStringParser(
          val => new Operand(val),
          OperatorFactory.create,
          LeftParenthesisFactory.create,
          RightParenthesisFactory.create);

        operator_stack = new Stack<object>();

        IProvideOperatorStack get_operator_stack = () => operator_stack;

        IEnumerable<IProcessAToken> processors = new List<IProcessAToken>
        {
          new TokenProcessor(Instance.is_a<IOperator>, new OperatorHandler(get_operator_stack)),
          new TokenProcessor(Instance.is_a<Operand>, new OperandHandler()),
          new TokenProcessor(Instance.is_a<LeftParenthesis>, new LeftParenthesisHandler(get_operator_stack)),
          new TokenProcessor(Instance.is_a<RightParenthesis>, new RightParenthesisHandler(get_operator_stack)),
        };

        processor_registry = new TokenProcessorRegistry(processors);
        var converter = new ExpressionConverter(new TokenProcessorRegistry(processors),
          new PostOperationHandler(get_operator_stack));
        var expression_printer = new ExpressionPrinter();

        infix_to_postfix_converter = new InfixToPostfixConverter(parser, converter, expression_printer);
      };

      Because b = () =>
        result = infix_to_postfix_converter.convert("3 * (2 + 1)");
    }
  }

  public class Instance
  {
    public static bool is_a<T>(object instance)
    {
      return instance is T;
    }
  }
}