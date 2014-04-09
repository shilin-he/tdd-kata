using System;
using System.ComponentModel.Design.Serialization;
using System.Linq.Expressions;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{

  [Subject(typeof(ExpressionToSqlTranslator<Product>))]
  public class ExpressionToSqlTranslatorSpecs
  {
    public abstract class concern : Observes<ExpressionToSqlTranslator<Product>>
    {

    }

    public class when_translate_a_comparison_expression : concern
    {
      Establish c = () =>
      {
        expr = p => p.unit_price > 10 && p.discontinued == false;
        sql = "[product_tbl].unit_price > 10 AND [product_tbl].discontinued = 0";

        mapping = depends.on<IMapModelToTable<Product>>();
        mapping.setup(x => x.get_table_name()).Return("product_tbl");
        mapping.setup(x => x.get_column_name("unit_price")).Return("unit_price");
        mapping.setup(x => x.get_column_name("discontinued")).Return("discontinued");
      };

      Because b = () =>
        result = sut.translate(expr);

      It returns_a_equivalent_sql_expression = () =>
        result.ShouldEqual(sql);

      static string result;
      static string sql;
      static Expression<Func<Product, bool>> expr;
      static IMapModelToTable<Product> mapping;
    }
  }
}
