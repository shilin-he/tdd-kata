using System;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(InsertSqlStatementBuilder<Product>))]
  public class InsertIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_building_insert_sql_statement : concern
    {
      Establish c = () =>
      {
        insert_statement = "insert into product_tbl" + Environment.NewLine +
                           "(product_name,discontinued) values ('super',0)";

        product_mapping = new TableMapping<Product>();
        product_mapping.map_table_name("product_tbl");
        product_mapping.map_column(x => x.name, "product_name");
        product_mapping.map_column(x => x.discontinued, "discontinued");
        product_mapping.map_id(x => x.id, "product_id");
        Sql<Product>.get_table_mapping = () => product_mapping;
        the_product = new Product { name = "super", discontinued = false };
      };

      Because b = () =>
        result = Sql<Product>.insert(the_product);

      It should_return_a_valid_insert_sql_statement = () =>
        result.ShouldEqual(insert_statement);

      static string result;
      static string insert_statement;
      static Product the_product;
      static IMapModelToTable<Product> product_mapping;
    }
  }
}