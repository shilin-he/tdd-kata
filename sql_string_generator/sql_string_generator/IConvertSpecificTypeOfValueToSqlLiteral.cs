namespace sql_string_generator
{
  public interface IConvertSpecificTypeOfValueToSqlLiteral
  {
    string convert(object value);
  }
}