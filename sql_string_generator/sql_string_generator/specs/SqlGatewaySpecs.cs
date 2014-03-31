using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(SqlGateway))]
  public class SqlGatewaySpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_generating_a_delete_sql_statement : concern
    {
      Establish c = () =>
      {
        IMapModelToTable<Product> product_mapping = new TableMapping<Product>();
        product_mapping.map_id(x => x.id, "product_id");
        product_mapping.map_table_name("product_tbl");
        the_product = new Product {id = 1};
        delete_sql_statement = "delete" + Environment.NewLine + "from product_tbl" + Environment.NewLine + "where product_id=1";

        SqlGateway.register_mapping(product_mapping);
      };

      Because b = () =>
        result = SqlGateway.delete(the_product);

      It should_return_a_valid_delete_sql_statement = () =>
        result.ShouldEqual(delete_sql_statement);

      static string result;
      static string delete_sql_statement;
      static Product the_product;
    }
  }
}
