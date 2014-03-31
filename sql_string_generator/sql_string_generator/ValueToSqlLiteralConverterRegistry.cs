using System.Collections.Generic;
using System.Linq;

namespace sql_string_generator
{
  public class ValueToSqlLiteralConverterRegistry : IFindConverterWhichCanConvertTheValue
  {
    IEnumerable<IConvertOneTypeOfValueToSqlLiteral> converters;

    public ValueToSqlLiteralConverterRegistry(IEnumerable<IConvertOneTypeOfValueToSqlLiteral> converters)
    {
      this.converters = converters;
    }

    public IConvertOneTypeOfValueToSqlLiteral find_converter_which_can_convert(object value)
    {
      return converters.First(x => x.can_convert(value));
    }
  }
}