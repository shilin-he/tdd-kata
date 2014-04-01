using System;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class SelectSqlStatementBuilder<Model> : IBuildSelectSqlStatements<Model>
  {
    IBuildSelectClauses<Model> select_builder;
    IBuildFromClauses<Model> from_builder;
    IBuildWhereClauses<Model> where_builder;
    Expression<Func<Model, bool>> filter;

    public SelectSqlStatementBuilder(IBuildSelectClauses<Model> select_builder, IBuildFromClauses<Model> from_builder, IBuildWhereClauses<Model> where_builder)
    {
      this.select_builder = select_builder;
      this.from_builder = from_builder;
      this.where_builder = where_builder;
    }

    public string build()
    {
      string sql = select_builder.build() + Environment.NewLine + from_builder.build();
      if (filter != null) sql += Environment.NewLine + where_builder.build(filter);
      return sql;
    }

    public IBuildSelectSqlStatements<Model> where(Expression<Func<Model, bool>> filter)
    {
      this.filter = filter;
      return this;
    }
  }
}