using System;

namespace sql_string_generator
{
  public class UpdateSqlStatementBuilder<Model> : IBuildUpdateSqlStatements<Model>
  {
    IBuildSetClauses<Model> set_factory;
    IBuildUpdateClauses<Model> update_factory;
    IBuildWhereClauses<Model> where_factory;

    public UpdateSqlStatementBuilder(IBuildSetClauses<Model> set_factory,
      IBuildUpdateClauses<Model> update_factory, IBuildWhereClauses<Model> where_factory)
    {
      this.set_factory = set_factory;
      this.update_factory = update_factory;
      this.where_factory = where_factory;
    }

    public string build(Model model)
    {
      string sql = update_factory.build() + Environment.NewLine + set_factory.build(model) + Environment.NewLine + where_factory.build(model);
      return sql.Trim();
    }
  }
}