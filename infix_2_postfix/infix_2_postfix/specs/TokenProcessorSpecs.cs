using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace tdd_kata.specs
{
  public class TokenProcessorSpecs
  {
    class concern : Observes<TokenProcessor>
    {
    }

    class when_determining_if_can_process_the_token : concern
    {
      static bool result;
      static IToken input_token;

      It should_make_the_decision_using_the_input_token = () =>
        result.ShouldBeTrue();

      Establish c = () =>
      {
        input_token = fake.an<IToken>();
        depends.on<IDetermineIfCanProcessAToken>(token =>
        {
          token.ShouldEqual(input_token);
          return true;
        });
      };

      Because b = () =>
        result = sut.can_process(input_token);
    }

    class when_processing_a_token : concern
    {
      static IToken input_token;
      static IHandleAToken token_handler;

      It should_delegate_the_processing_to_a_handler = () =>
        token_handler.received(x => x.handle(input_token));

      Establish c = () =>
      {
        input_token = fake.an<IToken>();
        token_handler = depends.on<IHandleAToken>();
      };

      Because b = () =>
        sut.process(input_token);
    }
  }
}