 using System.Collections;
 using System.Collections.Generic;
 using System.Linq;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace sql_string_generator.specs
{   

  [Subject(typeof(ValueToSqlLiteralConverterRegistry))]
  public class ValueToSqlLiteralConverterRegistrySpec
  {
    public abstract class concern : Observes<ValueToSqlLiteralConverterRegistry>
    {
        
    }

    public class when_finding_the_converter_which_can_converter_a_value : concern
    {
      Establish c = () =>
      {
        the_converter = fake.an<IConvertOneTypeOfValueToSqlLiteral>();
        the_converter.setup(x => x.can_convert(the_value)).Return(true);
        converters = Enumerable.Range(1, 10).Select(x => fake.an<IConvertOneTypeOfValueToSqlLiteral>()).ToList();
        converters.Add(the_converter);
        depends.on<IEnumerable<IConvertOneTypeOfValueToSqlLiteral>>(converters);
      };

      Because b = () =>
        result = sut.find_converter_which_can_convert(the_value);

      It should_determing_which_converter_can_convert_the_value = () =>
        result.can_convert(the_value).ShouldBeTrue();

      static IConvertOneTypeOfValueToSqlLiteral result;
      static object the_value;
      static IConvertOneTypeOfValueToSqlLiteral the_converter;
      static IList<IConvertOneTypeOfValueToSqlLiteral> converters;
    }
  }
}
