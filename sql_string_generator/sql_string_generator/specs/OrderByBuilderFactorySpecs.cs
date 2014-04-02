 using System;
 using System.Linq.Expressions;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(OrderByBuilderFactory<Product>))]
  public class OrderByBuilderFactorySpecs
  {
    public abstract class concern : Observes<OrderByBuilderFactory<Product>>
    {
        
    }

    public class when_create : concern
    {
      Establish c = () =>
      {
        prop_expr = x => x.name;
        depends.on<IMapModelToTable<Product>>();
      };

      Because b = () =>
        result = sut.create(prop_expr, SortOrders.descending);

      It returns_an_instance_of_order_by_builder = () =>
        result.ShouldBeAn<OrderByBuilder<Product, string>>();

      static IBuildAnOrderBy result;
      static Expression<Func<Product, string>> prop_expr;
    }
  }
}
