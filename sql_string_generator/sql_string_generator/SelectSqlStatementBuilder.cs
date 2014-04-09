using System;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class SelectSqlStatementBuilder<Model> : IBuildSelectSqlStatements<Model>
  {
    IBuildSelectClauses<Model> select_clause_builder;
    IBuildFromClauses<Model> from_clause_builder;
    IBuildWhereClauses<Model> where_clause_builder;
    IBuildOrderByClauses<Model> order_by_clause_builder;
    ICreateAnOrderByBuilder<Model> order_builder_factory;

    public static ICombineOrderBuilders combine_order_builders = (x, y) => new ChainedOrderByBuilder<Model>(x, y);

    public Expression<Func<Model, bool>> filter { get; private set; }
    public IBuildAnOrderBy order_builder { get; private set; }
    

    public SelectSqlStatementBuilder(
      IBuildSelectClauses<Model> select_clause_builder, 
      IBuildFromClauses<Model> from_clause_builder, 
      IBuildWhereClauses<Model> where_clause_builder, 
      IBuildOrderByClauses<Model> order_by_clause_builder, 
      ICreateAnOrderByBuilder<Model> order_builder_factory, 
      Expression<Func<Model, bool>> filter, 
      IBuildAnOrderBy order_builder)
    {
      this.select_clause_builder = select_clause_builder;
      this.from_clause_builder = from_clause_builder;
      this.where_clause_builder = where_clause_builder;
      this.order_by_clause_builder = order_by_clause_builder;
      this.filter = filter;
      this.order_builder_factory = order_builder_factory;
      this.order_builder = order_builder;
    }

    public string build()
    {
      var where = where_clause_builder.build(filter);
      var order_by = order_by_clause_builder.build(order_builder);

      return select_clause_builder.build() + Environment.NewLine +
             from_clause_builder.build() +
             (string.IsNullOrEmpty(where) ? string.Empty : Environment.NewLine + where) +
             (string.IsNullOrEmpty(order_by) ? string.Empty : Environment.NewLine + order_by);
    }

    public IBuildSelectSqlStatements<Model> where(Expression<Func<Model, bool>> filter)
    {
      return new SelectSqlStatementBuilder<Model>(select_clause_builder, 
        from_clause_builder, where_clause_builder, order_by_clause_builder, order_builder_factory, filter, order_builder);
    }

    public IBuildSelectSqlStatements<Model> order_by<PropertyType>(Expression<Func<Model, PropertyType>> property_accessor, ICustomSortOrder sort_order)
    {
      var new_order_builder = order_builder_factory.create(property_accessor, sort_order);
      var combined_order_buiulders = combine_order_builders(order_builder, new_order_builder);

      return new SelectSqlStatementBuilder<Model>(select_clause_builder, from_clause_builder, where_clause_builder, order_by_clause_builder, order_builder_factory, filter, combined_order_buiulders);
    }
  }
}