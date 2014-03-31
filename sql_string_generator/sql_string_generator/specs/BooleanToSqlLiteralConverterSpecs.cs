
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace sql_string_generator.specs
{
  [Subject(typeof(BooleanToSqlLiteralConverter))]
  public class BooleanToSqlLiteralConverterSpecs
  {
    public abstract class concern : Observes<BooleanToSqlLiteralConverter>
    {
    }

    public class when_converting_a_string : concern
    {
      Establish c = () =>
      {
        the_value = true;
        sql_literal = "1";
      };

      Because b = () =>
        result = sut.convert(the_value);

      It should_return_a_valid_sql_string_literal = () =>
        result.ShouldEqual(sql_literal);

      static string result;
      static string sql_literal;
      static object the_value;
    }
  }
}