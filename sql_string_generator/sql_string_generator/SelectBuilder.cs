using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace sql_string_generator
{
  public class SelectBuilder<TableModel> : IBuildSqlStatements
  {
    IBuildFromClauses<TableModel> factory;
    ICreateSelectClauses<TableModel> select_factory;
    readonly Expression<Func<TableModel, object>>[] columns;

    public SelectBuilder(IBuildFromClauses<TableModel> factory, 
      ICreateSelectClauses<TableModel> select_factory, params Expression<Func<TableModel, object>>[] columns)
    {
      this.factory = factory;
      this.select_factory = select_factory;
      this.columns = columns;
    }

    public string generate()
    {
      return select_factory.create(columns) + Environment.NewLine + factory.build();
    }
  }
}