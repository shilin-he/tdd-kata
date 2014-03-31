using System.Collections.Generic;
using System.Linq;

namespace tdd_kata
{
  public class ExpressionConverter : IConvertExpression
  {
    readonly ICleanUpOperatorStack post_handler;
    readonly IFindTokenProcessors processor_registry;

    public ExpressionConverter(IFindTokenProcessors processor_registry, ICleanUpOperatorStack post_handler)
    {
      this.processor_registry = processor_registry;
      this.post_handler = post_handler;
    }

    public IEnumerable<IToken> convert(IEnumerable<IToken> input_tokens)
    {
      var output = new List<IToken>();

      output.AddRange(input_tokens.SelectMany(
        x => processor_registry.find_processor(x).process(x)).ToList());

      output.AddRange(post_handler.clean_up());

      return output;
    }
  }
}