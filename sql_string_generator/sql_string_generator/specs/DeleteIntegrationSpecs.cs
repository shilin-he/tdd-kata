
using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(DeleteSqlStatementBuilder<Product>))]
  public class DeleteIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_building_delete_sql_statement : concern
    {
      Establish c = () =>
      {
        delete_statement = "delete" + Environment.NewLine +
                           "from product_tbl" + Environment.NewLine +
                           "where product_id=1";

        product_mapping = new TableMapping<Product>();
        product_mapping.map_table_name("product_tbl");
        product_mapping.map_id(x => x.id, "product_id");
        Sql<Product>.get_table_mapping = () => product_mapping;
        the_product = new Product { name = "super", discontinued = false, id = 1 };
      };

      Because b = () =>
        result = Sql<Product>.delete(the_product);

      It should_return_a_valid_delete_sql_statement = () =>
        result.ShouldEqual(delete_statement);

      static string result;
      static string delete_statement;
      static Product the_product;
      static IMapModelToTable<Product> product_mapping;
    }
  }
}