using System.ComponentModel;

namespace sql_string_generator
{
  public delegate object IGetPropertyValueUsingPropertyName<in TItem>(TItem item, string property_name);
}