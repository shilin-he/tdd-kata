namespace sql_string_generator
{
  public interface IFindConverterWhichCanConvertTheValue
  {
    IConvertOneTypeOfValueToSqlLiteral find_converter_which_can_convert(object value);
  }
}