using System;
using System.Linq.Expressions;
using System.Net.Configuration;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{

  [Subject(typeof(SelectSqlStatementBuilder<Product>))]
  public class SelectSqlStatementBuilderSpecs
  {
    public abstract class concern : Observes<SelectSqlStatementBuilder<Product>>
    {

    }

    public class when_building_a_select_sql_statement : concern
    {
      class and_no_filtering
      {
        Establish c = () =>
        {
          select_statement = "select product_id, product_name" + Environment.NewLine +
                             "from product_tbl";
          from_clause_builder = depends.on<IBuildFromClauses<Product>>();
          from_clause_builder.setup(x => x.build()).Return("from product_tbl");
          select_clause_builder = depends.on<IBuildSelectClauses<Product>>();
          select_clause_builder.setup(x => x.build()).Return("select product_id, product_name");
        };

        Because b = () =>
          result = sut.build();

        It returns_a_valid_select_sql_statement = () =>
          result.ShouldEqual(select_statement);

        static string result;
        static string select_statement;
        static IBuildFromClauses<Product> from_clause_builder;
        static IBuildSelectClauses<Product> select_clause_builder;

      }

      class and_filtering
      {
        Establish c = () =>
        {
          select_statement = "select product_id, product_name, discontinued" + Environment.NewLine +
                             "from product_tbl" + Environment.NewLine + 
                             "where discontinued = 0";
          filter = p => !p.discontinued;
          where_clause_builder = depends.on<IBuildWhereClauses<Product>>();
          where_clause_builder.setup(x => x.build(filter)).Return("where discontinued = 0");
          from_clause_builder = depends.on<IBuildFromClauses<Product>>();
          from_clause_builder.setup(x => x.build()).Return("from product_tbl");
          select_clause_builder = depends.on<IBuildSelectClauses<Product>>();
          select_clause_builder.setup(x => x.build()).Return("select product_id, product_name, discontinued");
        };

        Because b = () =>
          result = sut.where(filter).build();

        It retruns_a_select_sql_statement_with_where_clause = () =>
          result.ShouldEqual(select_statement);

        static string result;
        static string select_statement;
        static IBuildFromClauses<Product> from_clause_builder;
        static IBuildSelectClauses<Product> select_clause_builder;
        static IBuildWhereClauses<Product> where_clause_builder;
        static Expression<Func<Product, bool>>  filter;
      }
    }
  }
}
