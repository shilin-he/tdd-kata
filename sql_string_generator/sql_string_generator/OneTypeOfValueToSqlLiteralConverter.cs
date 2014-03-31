namespace sql_string_generator
{
  public class OneTypeOfValueToSqlLiteralConverter : IConvertOneTypeOfValueToSqlLiteral
  {
    IDetermineIfCanConvertAValueToSqlLiteral can_covert;
    IConvertSpecificTypeOfValueToSqlLiteral real_converter;

    public OneTypeOfValueToSqlLiteralConverter(IDetermineIfCanConvertAValueToSqlLiteral can_covert, IConvertSpecificTypeOfValueToSqlLiteral real_converter)
    {
      this.can_covert = can_covert;
      this.real_converter = real_converter;
    }

    public string convert(object value)
    {
      return real_converter.convert(value);
    }

    public bool can_convert(object value)
    {
      return can_covert(value);
    }
  }
}