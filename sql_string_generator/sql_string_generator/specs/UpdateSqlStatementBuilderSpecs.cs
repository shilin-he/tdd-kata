using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(UpdateSqlStatementBuilder<Product>))]
  public class UpdateSqlStatementBuilderSpecs
  {
    public abstract class concern : Observes<UpdateSqlStatementBuilder<Product>>
    {

    }

    public class when_generating : concern
    {
      Establish c = () =>
      {
        the_product = new Product {unit_price = (decimal) 11.0};
        update_factory = depends.on<IBuildUpdateClauses<Product>>();
        update_factory.setup(x => x.build()).Return(update_clause);
        set_factory = depends.on<IBuildSetClauses<Product>>();
        set_factory.setup(x => x.build(the_product)).Return(set_clause);
        where_factory = depends.on<IBuildWhereClauses<Product>>();
        where_factory.setup(x => x.build(the_product)).Return(where_clause);
      };

      Because b = () =>
        result = sut.build(the_product);

      It should_generate_update_sql_statement = () =>
        result.ShouldEqual(update_clause + Environment.NewLine +
                           set_clause + Environment.NewLine +
                           where_clause);

      static string update_clause = "update product_tbl";
      static string set_clause = "set unit_price=11.0";
      static string where_clause = "where product_name='foo'";
      static string result;
      static IBuildUpdateClauses<Product> update_factory;
      static IBuildSetClauses<Product> set_factory;
      static IBuildWhereClauses<Product> where_factory;
      static Product the_product;
    }
  }
}
