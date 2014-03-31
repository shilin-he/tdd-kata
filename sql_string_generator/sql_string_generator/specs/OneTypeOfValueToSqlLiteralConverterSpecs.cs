 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(OneTypeOfValueToSqlLiteralConverter))]
  public class OneTypeOfValueToSqlLiteralConverterSpecs
  {
    public abstract class concern : Observes<OneTypeOfValueToSqlLiteralConverter>
    {
        
    }

    public class when_determining_if_it_can_convert_a_value : concern
    {
      Establish c = () =>
      {
        the_value = "product1";
        depends.on<IDetermineIfCanConvertAValueToSqlLiteral>(val =>
        {
          val.ShouldEqual(the_value);
          return true;
        });
      };

      Because b = () =>
        result = sut.can_convert(the_value);

      It should_make_the_decision_based_on_the_specification_of_the_value = () =>
        result.ShouldBeTrue();

      static bool result;
      static object the_value;
    }

    public class when_converting : concern
    {
      Establish c = () =>
      {
        the_value = "product1";
        sql_literal = "'product1'";
        real_converter = depends.on<IConvertSpecificTypeOfValueToSqlLiteral>();
        real_converter.setup(x => x.convert(the_value)).Return(sql_literal);
      };

      Because b = () =>
        result = sut.convert(the_value);

      It should_delegate_converting_to_a_real_converter = () =>
        result.ShouldEqual(sql_literal);

      static string result;
      static string sql_literal;
      static object the_value;
      static IConvertSpecificTypeOfValueToSqlLiteral real_converter;
    }
  }
}
