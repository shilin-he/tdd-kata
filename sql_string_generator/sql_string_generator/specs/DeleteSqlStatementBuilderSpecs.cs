 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(DeleteSqlStatementBuilder<Product>))]
  public class DeleteSqlStatementBuilderSpecs
  {
    public abstract class concern : Observes<DeleteSqlStatementBuilder<Product>>
    {
        
    }

    public class when_building_a_delete_sql_statement : concern
    {
      Establish c = () =>
      {
        the_product = new Product {id = 1};
        from_clause_builder = depends.on<IBuildFromClauses<Product>>();
        from_clause_builder.setup(x => x.build()).Return("from product");
        where_clause_builder = depends.on<IBuildWhereClauses<Product>>();
        where_clause_builder.setup(x => x.build(the_product)).Return("where id=1");
        delete_sql_statement = "delete" + Environment.NewLine + "from product" + Environment.NewLine + "where id=1";
      };

      Because b = () =>
        result = sut.build(the_product);

      It should_return_a_valid_delete_sql_statement = () =>
        result.ShouldEqual(delete_sql_statement);

      static string result;
      static string delete_sql_statement;
      static Product the_product;
      static IBuildFromClauses<Product> from_clause_builder;
      static IBuildWhereClauses<Product> where_clause_builder;
    }
  }
}
