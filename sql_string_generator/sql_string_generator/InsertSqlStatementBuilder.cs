using System;

namespace sql_string_generator
{
  public class InsertSqlStatementBuilder<Entity> : IBuildInsertSqlStatements<Entity>
  {
    IMapModelToTable<Entity> mapping;
    IBuildValuesClauses<Entity> values_clause_builder;

    public InsertSqlStatementBuilder(IMapModelToTable<Entity> mapping, IBuildValuesClauses<Entity> values_clause_builder)
    {
      this.mapping = mapping;
      this.values_clause_builder = values_clause_builder;
    }

    public string build(Entity entity)
    {
      return "insert into " + mapping.get_table_name() + Environment.NewLine + values_clause_builder.build(entity);
    }
  }
}