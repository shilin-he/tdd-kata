 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(SelectClauseBuilder<Product>))]
  public class SelectClauseBuilderSpecs
  {
    public abstract class concern : Observes<SelectClauseBuilder<Product>>
    {
        
    }

    public class when_build : concern
    {
      Establish c = () =>
      {
        select_clause = "select product_id,product_name,unit_price";
        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_mapped_property_names()).Return(new[]{"name", "unit_price"});
        mapping.setup(x => x.get_column_name("name")).Return("product_name");
        mapping.setup(x => x.get_column_name("unit_price")).Return("unit_price");
        mapping.setup(x => x.get_id_property_names()).Return(new[]{"id"});
        mapping.setup(x => x.get_id_column_name("id")).Return("product_id");
      };

      Because b = () =>
        result = sut.build();

      It returns_a_sql_select_clause_using_mapped_columns = () =>
        result.ShouldEqual(select_clause);

      static string result;
      static string select_clause;
      static IMapModelToTable<Product> mapping;
    }
  }
}
