namespace sql_string_generator
{
  public class ValueToSqlLiteralConverter : IConvertValueToSqlLiteral
  {
    IFindConverterWhichCanConvertTheValue converter_registry;

    public ValueToSqlLiteralConverter(IFindConverterWhichCanConvertTheValue converter_registry)
    {
      this.converter_registry = converter_registry;
    }

    public string convert(object value)
    {
      return converter_registry.find_converter_which_can_convert(value).convert(value);
    }
  }
}