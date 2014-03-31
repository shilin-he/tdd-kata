namespace sql_string_generator
{
  public interface IConvertValueToSqlLiteral
  {
    string convert(object value);
  }
}