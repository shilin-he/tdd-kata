using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.specifications.extensions;

namespace sql_string_generator
{
  public class TableMapping<Model> : IMapModelToTable<Model>
  {
    string table_name = default(string);
    IDictionary<string, string> prop_column_maps = new Dictionary<string, string>();
    IDictionary<string, string> prop_id_maps = new Dictionary<string, string>();

    public void map_column<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr, string column_name)
    {
      var prop_name = get_property_name(prop_expr.Body);
      prop_column_maps.Add(prop_name, column_name);
    }

    public string get_column_name<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr)
    {
      var prop_name = get_property_name(prop_expr.Body);
      return prop_column_maps.ContainsKey(prop_name) ? prop_column_maps[prop_name] : prop_name;
    }

    public string get_column_name(string prop_name)
    {
      return prop_column_maps[prop_name];
    }

    private string get_property_name(Expression expr)
    {
      if (expr is UnaryExpression)
      {
        expr = (expr as UnaryExpression).Operand;
      }

      return ((MemberExpression) expr).Member.Name;
    }

    public string get_table_name()
    {
      return string.IsNullOrWhiteSpace(table_name) ? typeof(Model).Name : table_name;
    }

    public void map_table_name(string table_name)
    {
      this.table_name = table_name;
    }

    public IEnumerable<string> get_id_property_names()
    {
      return prop_id_maps.Keys;
    }

    public IEnumerable<string> get_mapped_property_names()
    {
      return prop_column_maps.Keys;
    }

    public string get_id_column_name(string id_property_name)
    {
      return prop_id_maps[id_property_name];
    }

    public void map_id<PropertyType>(Expression<Func<Person, PropertyType>> id_expr, string id_column_name)
    {
      var prop_name = get_property_name(id_expr.Body);
      prop_id_maps.Add(prop_name, id_column_name);
    }
  }
}