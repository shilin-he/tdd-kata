using System.Xml;

namespace sql_string_generator
{
  public class ValuesClauseBuilder<Entity> : IBuildValuesClauses<Entity>
  {
    public string build(Entity entity)
    {
      throw new System.NotImplementedException();
    }
  }
}