using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class Sql<Model>
  {
    public static IGetTableMapping<Model> get_table_mapping;
    public static IGetPropertyValueUsingPropertyName<Model> get_property_value;
    public static IFindConverterWhichCanConvertTheValue converter_registry;

    static Sql()
    {
      get_property_value = (model, prop_name) => model.GetType().GetProperty(prop_name).GetValue(model);

      IEnumerable<IConvertOneTypeOfValueToSqlLiteral> converters = new List<IConvertOneTypeOfValueToSqlLiteral>
      {
        new OneTypeOfValueToSqlLiteralConverter(val => val is string, new StringToSqlLiteralConverter()),
        new OneTypeOfValueToSqlLiteralConverter(val => val is bool, new BooleanToSqlLiteralConverter())
      };
      converter_registry = new ValueToSqlLiteralConverterRegistry(converters);
    }

    public static IBuildSqlStatements select(params Expression<Func<Model, object>>[] properties)
    {
      return new SelectBuilder<Model>(new FromClauseBuilder<Model>(get_table_mapping()), new SelectFactory<Model>(get_table_mapping()), properties);
    }

    public static string update(Model item)
    {
      var set_clause_builder = new SetClauseBuilder<Model>(get_table_mapping(), new ValueToSqlLiteralConverter(converter_registry), get_property_value);
      var update_clause_builder = new UpdateClauseBuilder<Model>(get_table_mapping());
      var where_clause_builder = new WhereClauseBuilder<Model>(get_table_mapping(), get_property_value);
      var build_sql_statements = new UpdateSqlStatementBuilder<Model>(set_clause_builder, update_clause_builder, where_clause_builder);
      return build_sql_statements.build(item);
    }
  }
}