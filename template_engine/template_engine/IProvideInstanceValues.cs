namespace template_engine
{
  public interface IProvideInstanceValues
  {
    string resolve(object model, string property_name);
  }
}