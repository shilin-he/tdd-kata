namespace tdd_kata
{
  public class InfixToPostfixConverter : IInfixToPostfixConverter
  {
    readonly IConvertExpression converter;
    readonly IParseInfixExpressionString parse_infix_expression_string;
    readonly IPrintExpressions print_expressions;

    public InfixToPostfixConverter(IParseInfixExpressionString parse_infix_expression_string,
      IConvertExpression converter, IPrintExpressions print_expressions)
    {
      this.parse_infix_expression_string = parse_infix_expression_string;
      this.converter = converter;
      this.print_expressions = print_expressions;
    }

    public string convert(string infix_expr)
    {
      return print_expressions.write(converter.convert(parse_infix_expression_string.read(infix_expr)));
    }
  }
}