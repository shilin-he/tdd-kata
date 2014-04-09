using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Rhino.Mocks;

namespace sql_string_generator
{
  public class WhereClauseBuilder<TableModel> : IBuildWhereClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;
    IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever;
    IExpressionToSqlTranslator<TableModel> expr_to_sql_translator;

    public WhereClauseBuilder(IMapModelToTable<TableModel> mapping, IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever, IExpressionToSqlTranslator<TableModel> expr_to_sql_translator)
    {
      this.mapping = mapping;
      this.property_value_retriever = property_value_retriever;
      this.expr_to_sql_translator = expr_to_sql_translator;
    }

    public string build(TableModel model)
    {
      var where_clause = new StringBuilder();

      foreach (var id_property_name in mapping.get_id_property_names())
      {
        where_clause.AppendFormat("{0}={1},",
          mapping.get_id_column_name(id_property_name),
          property_value_retriever(model, id_property_name));
      }

      var where = where_clause.ToString().TrimEnd(new[] { ',' });
      return string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
    }

    public string build(Expression<Func<TableModel, bool>> filter)
    {
      var where = expr_to_sql_translator.translate(filter);
      return string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
    }
  }
}