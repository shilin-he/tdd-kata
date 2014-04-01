using System;
using System.Linq;
using System.Text;
using System.Xml;
using Machine.Specifications.Utility;

namespace sql_string_generator
{
  public class ValuesClauseBuilder<Entity> : IBuildValuesClauses<Entity>
  {
    IMapModelToTable<Entity> mapping;
    IGetPropertyValueUsingPropertyName<Entity> property_accessor;
    IConvertValueToSqlLiteral val_2_sql_literal_converter;

    public ValuesClauseBuilder(IMapModelToTable<Entity> mapping, IGetPropertyValueUsingPropertyName<Entity> property_accessor, IConvertValueToSqlLiteral val_2_sql_literal_converter)
    {
      this.mapping = mapping;
      this.property_accessor = property_accessor;
      this.val_2_sql_literal_converter = val_2_sql_literal_converter;
    }

    public string build(Entity entity)
    {
      var prop_names = mapping.get_mapped_property_names().ToArray();
//      if (!prop_names.Any()) throw new ArgumentException("No mapping columns."); 

      var sb = new StringBuilder();
      sb.Append("(");
      prop_names.Each(x => sb.AppendFormat("{0},", mapping.get_column_name(x)));
      sb.Remove(sb.Length - 1, 1);
      sb.Append(") values (");
      prop_names.Each(x => sb.AppendFormat("{0},", val_2_sql_literal_converter.convert(property_accessor(entity, x))));
      sb.Remove(sb.Length - 1, 1);
      sb.Append(")");
      return sb.ToString();
    }
  }
}