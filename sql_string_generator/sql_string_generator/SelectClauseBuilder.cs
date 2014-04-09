using System.Linq;
using System.Text;
using developwithpassion.specifications.extensions;
using Machine.Specifications.Runner.Impl;
using Machine.Specifications.Utility;

namespace sql_string_generator
{
  public class SelectClauseBuilder<Model> : IBuildSelectClauses<Model>
  {
    IMapModelToTable<Model> mapping;

    public SelectClauseBuilder(IMapModelToTable<Model> mapping)
    {
      this.mapping = mapping;
    }

    public string build()
    {
      var sb = new StringBuilder();
      sb.Append("select ");
      mapping.get_id_property_names().Select(x => mapping.get_id_column_name(x)).ToList().ForEach(x => sb.AppendFormat("{0},", x));
      mapping.get_mapped_property_names().Select(x => mapping.get_column_name(x)).ToList().ForEach(x => sb.AppendFormat("{0},", x));
      return sb.ToString().TrimEnd(',');
    }
  }
}