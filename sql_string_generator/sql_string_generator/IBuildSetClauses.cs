namespace sql_string_generator
{
  public interface IBuildSetClauses<in TableModel>
  {
    string build(TableModel model);
  }
}