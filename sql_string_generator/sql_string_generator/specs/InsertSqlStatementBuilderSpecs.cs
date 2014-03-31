 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(InsertSqlStatementBuilder<Product>))]
  public class InsertSqlStatementBuilderSpecs
  {
    public abstract class concern : Observes<InsertSqlStatementBuilder<Product>>
    {
        
    }

    public class when_building_an_insert_sql_statement : concern
    {
      Establish c = () =>
      {
        the_product = new Product();
        values_clause_builder = depends.on<IBuildValuesClauses<Product>>();
        values_clause = "(product_name, discontinued) values ('super', 0)";
        values_clause_builder.setup(x => x.build(the_product)).Return(values_clause);
        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_table_name()).Return("product_tbl");
        insert_sql_statement = "insert into product_tbl" + Environment.NewLine + values_clause;
      };

      Because b = () =>
        result = sut.build(the_product);

      It should_return_a_valid_insert_sql_statement = () =>
        result.ShouldEqual(insert_sql_statement);

      static string result;
      static string insert_sql_statement;
      static Product the_product;
      static IBuildValuesClauses<Product> values_clause_builder;
      static IMapModelToTable<Product> mapping;
      static string values_clause;
    }
  }
}
