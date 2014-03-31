namespace sql_string_generator
{
  public class BooleanToSqlLiteralConverter:IConvertSpecificTypeOfValueToSqlLiteral
  {
    public string convert(object value)
    {
      return (bool) value ? "1" : "0";
    }
  }
}