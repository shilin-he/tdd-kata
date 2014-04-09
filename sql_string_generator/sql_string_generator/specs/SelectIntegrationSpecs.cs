using System;
using System.Security.Cryptography.X509Certificates;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(SelectSqlStatementBuilder<Person>))]
  public class SelectIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_building_select_statement : concern
    {
      Establish c = () =>
      {
        sql = "select product_id,product_name,unit_price,discontinued" + Environment.NewLine +
              "from product_tbl" + Environment.NewLine + 
              "where [product_tbl].unit_price > 10 AND [product_tbl].discontinued = 0" + Environment.NewLine + 
              "order by product_name desc";

        product_mapping = new TableMapping<Product>();
        product_mapping.map_table_name("product_tbl");
        product_mapping.map_id(x => x.id, "product_id");
        product_mapping.map_column(x => x.name, "product_name");
        product_mapping.map_column(x => x.unit_price, "unit_price");
        product_mapping.map_column(x => x.discontinued, "discontinued");

        Sql<Product>.get_table_mapping = () => product_mapping;
      };

      Because b = () =>
      {
        result = Sql<Product>.select()
            .where(x => x.unit_price > 10 && x.discontinued == false)
            .order_by(x => x.name, SortOrders.descending)
            .build();
      };

      It shold_build = () =>
      {
        Console.WriteLine(sql);
        Console.WriteLine(result);
        result.ShouldEqual(sql);
      };

      static string result;
      static IMapModelToTable<Product> product_mapping;
      static string sql;
    }
  }
}
