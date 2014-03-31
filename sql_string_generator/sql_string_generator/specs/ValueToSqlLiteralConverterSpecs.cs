 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(ValueToSqlLiteralConverter))]
  public class ValueToSqlLiteralConverterSpecs
  {
    public abstract class concern : Observes<ValueToSqlLiteralConverter>
    {
        
    }

    public class when_converting_a_value_to_sql_literal : concern
    {
      Establish c = () =>
      {
        value = "product1";
        sql_literal = "'product1'";
        the_converter = fake.an<IConvertOneTypeOfValueToSqlLiteral>();
        the_converter.setup(x => x.convert(value)).Return(sql_literal);
        converter_registry = depends.on<IFindConverterWhichCanConvertTheValue>();
        converter_registry.setup(x => x.find_converter_which_can_convert(value)).Return(the_converter);
      };

      Because b = () =>
        result = sut.convert(value);

      It should_use_the_converter_to_convert_the_value = () =>
        result.ShouldEqual(sql_literal);

      static string result;
      static string value;
      static IFindConverterWhichCanConvertTheValue converter_registry;
      static IConvertOneTypeOfValueToSqlLiteral the_converter;
      static string sql_literal;
    }
  }
}
