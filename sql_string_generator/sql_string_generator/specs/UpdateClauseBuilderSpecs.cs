 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(UpdateClauseBuilder<Product>))]
  public class UpdateClauseBuilderSpecs
  {
    public abstract class concern : Observes<UpdateClauseBuilder<Product>>
    {
        
    }

    public class when_building : concern
    {
      Establish c = () =>
      {
        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_table_name()).Return("product_tbl");
      };

      Because b = () =>
        result = sut.build();

      It should_build_a_udpate_clause = () =>
        result.ShouldEqual("update product_tbl");

      static string result;
      static IMapModelToTable<Product> mapping;
    }
  }
}
