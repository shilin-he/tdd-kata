using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdd_kata
{
  class ExpressionPrinter : IPrintExpressions
  {
    public string write(IEnumerable<IToken> output_tokens)
    {
      var output = new StringBuilder();
      IToken[] tokens = output_tokens.ToArray();

      int last_idx = tokens.Length - 1;
      for (int idx = 0; idx <= last_idx; idx++)
      {
        IToken token = tokens[idx];
        output.Append(token.write());
        bool is_the_last_token = idx == last_idx;
        bool is_a_left_parenthesis = token.GetType() == typeof(LeftParenthesis);
        bool is_followed_by_right_parenthesis = idx < last_idx &&
                                                tokens[idx + 1].GetType() == typeof(RightParenthesis);
        if (!(is_the_last_token
              || is_a_left_parenthesis
              || is_followed_by_right_parenthesis)) output.Append(" ");
      }

      return output.ToString();
    }
  }
}