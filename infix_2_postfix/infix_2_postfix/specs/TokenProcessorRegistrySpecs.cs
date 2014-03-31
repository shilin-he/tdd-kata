using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class TokenProcessorRegistrySpecs
  {
    class concern : Observes<TokenProcessorRegistry>
    {
    }

    class when_finding_token_processor : concern
    {
      static IProcessAToken result;
      static IToken input_token;
      static IProcessAToken processor;

      It should_get_a_procesor_that_can_process_the_token = () =>
        result.can_process(input_token).ShouldBeTrue();

      Establish c = () =>
      {
        processor = fake.an<IProcessAToken>();
        processor.setup(x => x.can_process(input_token)).Return(true);
        depends.on<IEnumerable<IProcessAToken>>(new[] {processor});
      };

      Because b = () =>
        result = sut.find_processor(input_token);
    }
  }
}