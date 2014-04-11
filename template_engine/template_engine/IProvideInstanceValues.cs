namespace template_engine
{
  public interface IProvideInstanceValues
  {
    string convert<TModel>(TModel model, string template_value);
  }
}