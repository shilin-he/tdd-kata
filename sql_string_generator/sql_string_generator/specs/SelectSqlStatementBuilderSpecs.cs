using System;
using System.Linq.Expressions;
using System.Net.Configuration;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{

  [Subject(typeof(SelectSqlStatementBuilder<Product>))]
  public class SelectSqlStatementBuilderSpecs
  {
    public abstract class concern : Observes<SelectSqlStatementBuilder<Product>> { }

    public class when_building_a_select_sql_statement : concern
    {
      Establish c = () =>
      {
        select_statement = "select product_id, product_name, discontinued" + Environment.NewLine +
                           "from product_tbl" + Environment.NewLine +
                           "where discontinued = 0" + Environment.NewLine + 
                           "order by product_name desc";
        filter = p => !p.discontinued;
        select_clause_builder = depends.on<IBuildSelectClauses<Product>>();
        select_clause_builder.setup(x => x.build()).Return("select product_id, product_name, discontinued");
        from_clause_builder = depends.on<IBuildFromClauses<Product>>();
        from_clause_builder.setup(x => x.build()).Return("from product_tbl");
        where_clause_builder = depends.on<IBuildWhereClauses<Product>>();
        where_clause_builder.setup(x => x.build(filter)).Return("where discontinued = 0");
        order_builder = depends.on<IBuildAnOrderBy>();
        order_by_clause_builder = depends.on<IBuildOrderByClauses<Product>>();
        order_by_clause_builder.setup(x => x.build(order_builder)).Return("order by product_name desc");
        depends.on(filter);
      };

      Because b = () =>
        result = sut.build();

      It retruns_a_select_sql_statement_with_where_clause = () =>
        result.ShouldEqual(select_statement);

      static string result;
      static string select_statement;
      static IBuildFromClauses<Product> from_clause_builder;
      static IBuildSelectClauses<Product> select_clause_builder;
      static IBuildWhereClauses<Product> where_clause_builder;
      static Expression<Func<Product, bool>> filter;
      static IBuildOrderByClauses<Product> order_by_clause_builder;
      static IBuildAnOrderBy order_builder;
    }

    public class when_filtering : concern
    {
      Establish c = () =>
      {
        filter = p => !p.discontinued;
        null_filter = p => true;
        depends.on<IBuildWhereClauses<Product>>();
        depends.on<IBuildFromClauses<Product>>();
        depends.on<IBuildSelectClauses<Product>>();
        depends.on(null_filter);
      };

      Because b = () =>
        result = sut.where(filter);

      It returns_a_new_select_statement_builder_with_filter = () =>
      {
        var new_builder = result.ShouldBeAn<SelectSqlStatementBuilder<Product>>();
        new_builder.ShouldNotEqual(sut);
        new_builder.filter.ShouldEqual(filter);
      };

      static IBuildSelectSqlStatements<Product> result;
      static Expression<Func<Product, bool>> null_filter;
      static Expression<Func<Product, bool>> filter;
    }

    public class when_ordering : concern
    {
      Establish c = () =>
      {
        null_filter = p => true;
        prop_expr = x => x.name;

        depends.on<IBuildWhereClauses<Product>>();
        depends.on<IBuildFromClauses<Product>>();
        depends.on<IBuildSelectClauses<Product>>();
        depends.on<IBuildOrderByClauses<Product>>();
        depends.on(null_filter);
        first_order = depends.on<IBuildAnOrderBy>();

        second_order = fake.an<IBuildAnOrderBy>();
        combined_order = fake.an<IBuildAnOrderBy>();
        order_by_builder_factory = depends.on<ICreateAnOrderByBuilder<Product>>();
        order_by_builder_factory.setup(x => x.create(prop_expr, SortOrders.descending)).Return(second_order);

        combinator = (x, y) =>
        {
          x.ShouldEqual(first_order);
          y.ShouldEqual(second_order);
          return combined_order;
        };
        spec.change(() => SelectSqlStatementBuilder<Product>.combine_order_builders).to(combinator);
      };

      Because b = () =>
        result = sut.order_by(prop_expr, SortOrders.descending);

      It returns_a_new_select_statement_builder_with_combined_ordering = () =>
      {
        var new_builder = result.ShouldBeAn<SelectSqlStatementBuilder<Product>>();
        new_builder.ShouldNotEqual(sut);
        new_builder.order_builder.ShouldEqual(combined_order);
      };

      static IBuildSelectSqlStatements<Product> result;
      static Expression<Func<Product, bool>> null_filter;
      static IBuildAnOrderBy combined_order;
      static Expression<Func<Product, string>> prop_expr;
      static IBuildAnOrderBy second_order;
      static ICombineOrderBuilders combinator;
      static IBuildAnOrderBy first_order;
      static ICreateAnOrderByBuilder<Product> order_by_builder_factory;
    }
  }
}
