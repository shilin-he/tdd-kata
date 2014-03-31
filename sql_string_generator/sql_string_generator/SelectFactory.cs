using System;
using System.Linq;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class SelectFactory<TableModel> : ICreateSelectClauses<TableModel>
  {
    IMapModelToTable<TableModel> mapping;

    public SelectFactory(IMapModelToTable<TableModel> mapping)
    {
      this.mapping = mapping;
    }

    public string create(params Expression<Func<TableModel, object>>[] columns)
    {
      return "select " +
             string.Join(", ", columns.Select(mapping.get_column_name)).TrimEnd(new[] {',', ' '});
    }
  }
}