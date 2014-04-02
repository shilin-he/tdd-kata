 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(ChainedOrderByBuilder<Product>))]
  public class ChainedOrderByBuilderSpecs
  {
    public abstract class concern : Observes<ChainedOrderByBuilder<Product>>
    {
        
    }

    public class when_build : concern
    {
      Establish c = () =>
      {
        first = fake.an<IBuildAnOrderBy>();
        second = fake.an<IBuildAnOrderBy>();
        sut_factory.create_using(() => new ChainedOrderByBuilder<Product>(first, second));
      };

      Because b = () =>
        sut.build();

      It builds_from_both_builders = () =>
      {
        first.received(x => x.build());
        second.received(x => x.build());
      };

      static IBuildAnOrderBy first;
      static IBuildAnOrderBy second;
    }
  }
}
