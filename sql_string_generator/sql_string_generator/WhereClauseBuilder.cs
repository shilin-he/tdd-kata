using System.Text;
using System.Threading;
using Rhino.Mocks;

namespace sql_string_generator
{
  public class WhereClauseBuilder<TableModel>:IBuildWhereClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;
    IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever;

    public WhereClauseBuilder(IMapModelToTable<TableModel> mapping, IGetPropertyValueUsingPropertyName<TableModel> property_value_retriever)
    {
      this.mapping = mapping;
      this.property_value_retriever = property_value_retriever;
    }

    public string build(TableModel model)
    {
      var where_clause = new StringBuilder();
      where_clause.Append("where ");

      foreach (var id_property_name in mapping.get_id_property_names())
      {
        where_clause.AppendFormat("{0}={1},", 
          mapping.get_id_column_name(id_property_name), 
          property_value_retriever(model, id_property_name));
      }

      return where_clause.ToString().TrimEnd(new[] {','});
    }
  }
}