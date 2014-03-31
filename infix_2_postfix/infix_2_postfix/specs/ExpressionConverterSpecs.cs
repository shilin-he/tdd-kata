using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class ExpressionConverterSpecs
  {
    class concern : Observes<ExpressionConverter>
    {
    }

    class when_converting : concern
    {
      static IList<IToken> output;
      static IEnumerable<IToken> result;
      static IEnumerable<IToken> input_tokens;
      static IFindTokenProcessors token_processor_registry;
      static IToken input_token;
      static IProcessAToken processor;
      static IEnumerable<IToken> generated_tokens;
      static IToken generated_token;
      static ICleanUpOperatorStack post_operation;
      static IToken rest_token;

      It should_convert_infix_expression_to_postfix_expression = () =>
        result.ShouldEqual(output);

      Establish c = () =>
      {
        input_token = fake.an<IToken>();
        input_tokens = new[] {input_token};
        generated_token = fake.an<IToken>();
        generated_tokens = new[] {generated_token};
        rest_token = fake.an<IOperator>();
        output = new List<IToken>(generated_tokens) {rest_token};
        processor = fake.an<IProcessAToken>();
        processor.setup(x => x.process(input_token)).Return(generated_tokens);
        token_processor_registry = depends.on<IFindTokenProcessors>();
        token_processor_registry.setup(x => x.find_processor(input_token)).Return(processor);
        post_operation = depends.on<ICleanUpOperatorStack>();
        post_operation.setup(x => x.clean_up()).Return(new[] {rest_token});
      };

      Because b = () =>
        result = sut.convert(input_tokens);
    }
  }
}