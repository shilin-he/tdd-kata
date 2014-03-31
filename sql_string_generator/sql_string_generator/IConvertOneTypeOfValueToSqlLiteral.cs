namespace sql_string_generator
{
  public interface IConvertOneTypeOfValueToSqlLiteral
  {
    string convert(object value);
    bool can_convert(object value);
  }
}