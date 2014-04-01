namespace sql_string_generator
{
  public interface IBuildSelectClauses<Model>
  {
    string build();
  }
}