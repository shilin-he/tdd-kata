namespace sql_string_generator
{
  public class StringToSqlLiteralConverter : IConvertSpecificTypeOfValueToSqlLiteral
  {
    public string convert(object value)
    {
      return "'" + ((value as string) ?? "") + "'";
    }
  }
}