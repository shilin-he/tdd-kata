using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace sql_string_generator.specs
{
  [Subject(typeof(StringToSqlLiteralConverter))]
  public class StringToSqlLiteralConverterSpecs
  {
    public abstract class concern : Observes<StringToSqlLiteralConverter>
    {
    }

    public class when_converting_a_string : concern
    {
      Establish c = () =>
      {
        the_value = "super";
        sql_literal = "'super'";
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