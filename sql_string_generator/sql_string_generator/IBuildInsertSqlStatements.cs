namespace sql_string_generator
{
  public interface IBuildInsertSqlStatements<in Entity>
  {
    string build(Entity entity);
  }
}