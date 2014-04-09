 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(OrderByClauseBuilder<Product>))]
  public class OrderByClauseBuilderSpecs
  {
    public abstract class concern : Observes<OrderByClauseBuilder<Product>>
    {
        
    }

    public class when_build : concern
    {
      Establish c = () =>
      {
        order_by_clause = "order by product_name, discontinued desc";
        order_builder = fake.an<IBuildAnOrderBy>();
        order_builder.setup(x => x.build()).Return("product_name, discontinued desc");
      };

      Because b = () =>
        result = sut.build(order_builder);

      It returns_a_valid_order_by_clause = () =>
        result.ShouldEqual(order_by_clause);

      static string result;
      static string order_by_clause;
      static IBuildAnOrderBy order_builder;
    }
  }
}
