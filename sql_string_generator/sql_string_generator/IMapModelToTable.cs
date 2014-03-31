using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rhino.Mocks;

namespace sql_string_generator
{
  public interface IMapModelToTable<Model>
  {
    void map_table_name(string table_name);
    void map_column<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr, string column_name);
    void map_id<PropertyType>(Expression<Func<Person, PropertyType>> id_expr, string id_column_name);

    string get_column_name<PropertyType>(Expression<Func<Model, PropertyType>> prop_expr);
    string get_column_name(string prop_name);
    string get_table_name();
    IEnumerable<string> get_id_property_names();
    IEnumerable<string> get_mapped_property_names();
    string get_id_column_name(string id_property_name);
  }
}