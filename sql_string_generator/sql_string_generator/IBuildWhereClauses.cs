namespace sql_string_generator
{
  public interface IBuildWhereClauses<in TableModel>
  {
    string build(TableModel model);
  }
}