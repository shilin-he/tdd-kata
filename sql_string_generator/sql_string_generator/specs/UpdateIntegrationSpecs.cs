using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(UpdateClauseBuilder<Product>))]
  public class UpdateIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_building_update_sql_statement : concern
    {
      Establish c = () =>
      {
        udpate_statement = "update product_tbl" + Environment.NewLine +
                           "set product_name='super',discontinued=0" + Environment.NewLine +
                           "where product_id=1";

        product_mapping = new TableMapping<Product>();
        product_mapping.map_table_name("product_tbl");
        product_mapping.map_column(x => x.name, "product_name");
        product_mapping.map_column(x => x.discontinued, "discontinued");
        product_mapping.map_id(x => x.id, "product_id");
        Sql<Product>.get_table_mapping = () => product_mapping;
        the_product = new Product { name = "super", discontinued = false, id = 1 };
      };

      Because b = () =>
        result = Sql<Product>.update(the_product);

      It should_return_a_valid_udpate_sql_statement = () =>
        result.ShouldEqual(udpate_statement);

      static string result;
      static string udpate_statement;
      static Product the_product;
      static IMapModelToTable<Product> product_mapping;
    }
  }
}
