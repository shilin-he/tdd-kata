 using System;
 using System.Linq.Expressions;
 using System.Net.Configuration;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(OrderByBuilder<Product, string>))]
  public class OrderByBuilderSpecs
  {
    public abstract class concern : Observes<OrderByBuilder<Product, string>>
    {
        
    }

    public class when_build : concern
    {
      Establish c = () =>
      {
        column_name = "product_name";
        prop_expr = x => x.name;
        depends.on(prop_expr);
        sort_order = depends.on<ICustomSortOrder>();
        sort_order.setup(x => x.apply_to(column_name)).Return("product_name desc");
        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_column_name(prop_expr)).Return(column_name );
      };

      Because b = () =>
        result = sut.build();

      It returns_order_column_name_and_sort_order_string = () =>
        result.ShouldEqual("product_name desc");

      static string result;
      static Expression<Func<Product, string>>  prop_expr;
      static IMapModelToTable<Product> mapping;
      static ICustomSortOrder sort_order;
      static string column_name;
    }
  }
}
