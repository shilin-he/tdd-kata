 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator
{   

  [Subject(typeof(ValuesClauseBuilder))]
  public class ValuesClauseBuilderSpecs
  {
    public abstract class concern : Observes<ValuesClauseBuilder>
    {
        
    }

    public class when_build : concern
    {
      It builds_the_values_clause = () =>
        result.ShouldEqual("(product_name, discontinued) values ('test_product', 0)");

      static string result;

        
    }
  }
}
