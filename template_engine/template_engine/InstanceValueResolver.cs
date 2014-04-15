using System;
using System.Linq;
using System.Reflection;

namespace template_engine
{
  public class InstanceValueResolver : IProvideInstanceValues
  {
    public string resolve(object model, string property_name)
    {
      var names = property_name.Split('.');

      foreach (var name in names)
      {
        var property = model.GetType().GetProperties().FirstOrDefault(p => p.Name == name);
        if (property == null) throw new ArgumentOutOfRangeException(string.Format("Invalid property name: {0}.", property_name));
        model = property.GetValue(model);
      }

      return model.ToString();
    }
  }
}