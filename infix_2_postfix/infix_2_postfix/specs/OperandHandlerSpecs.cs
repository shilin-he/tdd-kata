using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class OperandHandlerSpecs
  {
    class concern : Observes<OperandHandler>
    {
    }

    class when_handling : concern
    {
      static IEnumerable<IToken> result;
      static IEnumerable<IToken> output;
      static IToken input;

      Establish c = () =>
      {
        input = fake.an<IToken>();
        output = new[] {input};
      };

      Because b = () =>
        result = sut.handle(input);

      It shuold_produce_output = () =>
        result.ShouldEqual(output);
    }
  }
}