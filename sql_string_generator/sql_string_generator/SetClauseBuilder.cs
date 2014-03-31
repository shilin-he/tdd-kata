using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using developwithpassion.specifications.extensions;

namespace sql_string_generator
{
  public class SetClauseBuilder<TableModel> : IBuildSetClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;
    IConvertValueToSqlLiteral value_to_sql_literal_converter;
    IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever;

    public SetClauseBuilder(
      IMapModelToTable<TableModel> mapping, 
      IConvertValueToSqlLiteral value_to_sql_literal_converter, 
      IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever)
    {
      this.mapping = mapping;
      this.value_to_sql_literal_converter = value_to_sql_literal_converter;
      this.property_value_retriever = property_value_retriever;
    }

    public string build(TableModel model)
    {
      var set_clause = new StringBuilder();
      set_clause.Append("set ");

      foreach (var prop_name in mapping.get_mapped_property_names())
      {
        set_clause.AppendFormat("{0}={1},",
          mapping.get_column_name(prop_name),
          value_to_sql_literal_converter.convert(property_value_retriever(model, prop_name)));
      }

      return set_clause.ToString().TrimEnd(new[] {','});
    }
  }
}