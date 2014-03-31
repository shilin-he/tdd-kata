using System;
using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  class InfixExpressionStringParser : IParseInfixExpressionString
  {
    const char left_parenthesis = '(';
    const char right_parenthesis = ')';
    static readonly char[] operators = {'*', '/', '+', '-'};
    readonly ICreateLeftParenthesisToken create_left_parenthesis_token;
    readonly ICreateOperandToken create_operand_token;
    readonly ICreateOperatorToken create_operator_token;
    readonly ICreateRightParenthesisToken create_right_parenthesis_token;

    public InfixExpressionStringParser(
      ICreateOperandToken create_operand_token,
      ICreateOperatorToken create_operator_token,
      ICreateLeftParenthesisToken create_left_parenthesis_token,
      ICreateRightParenthesisToken create_right_parenthesis_token)
    {
      this.create_operand_token = create_operand_token;
      this.create_operator_token = create_operator_token;
      this.create_left_parenthesis_token = create_left_parenthesis_token;
      this.create_right_parenthesis_token = create_right_parenthesis_token;
    }

    public IEnumerable<IToken> read(string infix_str)
    {
      int idx = 0;
      int len = infix_str.Length;
      string number_buff = string.Empty;
      while (idx < len)
      {
        char chr = infix_str[idx];

        if (Char.IsDigit(chr) || chr.Equals('.'))
        {
          number_buff += chr;
        }
        else
        {
          if (!string.IsNullOrEmpty(number_buff))
          {
            yield return create_operand_token(number_buff);
            number_buff = string.Empty;
          }

          if (operators.Any(x => x.Equals(chr)))
            yield return create_operator_token(chr);
          else if (chr.Equals(left_parenthesis))
            yield return create_left_parenthesis_token();
          else if (chr.Equals(right_parenthesis))
            yield return create_right_parenthesis_token();
        }

        idx++;
      }
    }
  }
}