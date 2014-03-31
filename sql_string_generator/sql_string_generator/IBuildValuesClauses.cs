namespace sql_string_generator
{
  public interface IBuildValuesClauses<in Entity>
  {
    string build(Entity entity);
  }
}