using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace sql_string_generator
{
  public class SqlGateway
  {
    private static IDictionary<Type, object> mapping_registry = new Dictionary<Type, object>();

    public static void register_mapping<Entity>(IMapModelToTable<Entity> entity_mapping)
    {
      mapping_registry.Add(typeof(Entity), entity_mapping);
    }

    public static string delete<Entity>(Entity entity)
    {
      IGetPropertyValueUsingPropertyName<Entity> get_property_value = (model, prop_name) => model.GetType().GetProperty(prop_name).GetValue(model);

      var mapping = (IMapModelToTable<Entity>) mapping_registry[typeof(Entity)];
      return new DeleteSqlStatementBuilder<Entity>(
        new WhereClauseBuilder<Entity>(mapping, get_property_value),
        new FromClauseBuilder<Entity>(mapping)).build(entity);
    }
  }
}