 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace max.web.specs
{   

  [Subject(typeof(WebCommand))]
  public class WebCommandSpecs
  {
    public abstract class concern : Observes<IProcessOneWebRequest, WebCommand>
    {
        
    }

    public class when_determine_if_it_can_process_a_request : concern
    {
      Establish c = () =>
      {
        the_request = fake.an<IContainInfoForOneWebRequest>();
        depends.on<IMatchOneRequest>(x =>
        {
          x.ShouldEqual(the_request);
          return true;
        });
      };

      Because b = () =>
        result = sut.can_process(the_request);

      It makes_decision_based_on_the_request_specification = () =>
        result.ShouldBeTrue();

      static bool result;
      static IContainInfoForOneWebRequest the_request;
    }

    public class when_process_the_request : concern
    {
      Establish c = () =>
      {
        the_request = fake.an<IContainInfoForOneWebRequest>();
        feature = depends.on<IImplementOneAppFeature>();
      };

      Because b = () =>
        sut.process(the_request);

      It delegates_the_processing_to_the_application_feature_for_the_request = () => 
        feature.received(x => x.process(the_request));

      static IImplementOneAppFeature feature;
      static IContainInfoForOneWebRequest the_request;
    }
  }
}
