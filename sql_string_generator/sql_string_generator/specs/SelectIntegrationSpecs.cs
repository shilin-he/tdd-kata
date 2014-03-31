using System;
using System.Security.Cryptography.X509Certificates;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{
  [Subject(typeof(SelectBuilder<Person>))]
  public class SelectIntegrationSpecs
  {
    public abstract class concern : Observes
    {

    }

    public class when_building_select_statement : concern
    {
      Establish c = () =>
      {
        peple_mapping = new TableMapping<Person>();
        peple_mapping.map_column(x => x.first_name, "fname");
        peple_mapping.map_table_name("people_tbl");
        Sql<Person>.get_table_mapping = () => peple_mapping;

        product_mapping = new TableMapping<Product>();
//        product_mapping.map_table_name("product_tbl");
        product_mapping.map_column(x => x.name, "product_name");
        Sql<Product>.get_table_mapping = () => product_mapping;
      };

      Because b = () =>
      {
        select_people = Sql<Person>.select(p => p.first_name, p => p.birth_date).generate();
        select_product = Sql<Product>.select(x => x.name, x => x.unit_price).generate();
      };

      It shold_build = () =>
      {
        select_people.ShouldEqual("select fname, birth_date" + Environment.NewLine + "from people_tbl");
        select_product.ShouldEqual("select product_name, unit_price" + Environment.NewLine + "from Product");
      };

      static string select_people;
      static string select_product;
      static IMapModelToTable<Person> peple_mapping;
      static IMapModelToTable<Product> product_mapping;
    }
  }
}
