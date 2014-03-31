using System;

namespace sql_string_generator
{
  public class DeleteSqlStatementBuilder<Entity> : IBuildDeleteSqlStatements<Entity>
  {
    IBuildWhereClauses<Entity> where_clause_builder;
    IBuildFromClauses<Entity> from_clause_builder;

    public DeleteSqlStatementBuilder(IBuildWhereClauses<Entity> where_clause_builder, IBuildFromClauses<Entity> from_clause_builder)
    {
      this.where_clause_builder = where_clause_builder;
      this.from_clause_builder = from_clause_builder;
    }

    public string build(Entity model)
    {
      return "delete" + Environment.NewLine + from_clause_builder.build() + Environment.NewLine +
             where_clause_builder.build(model);
    }
  }
}