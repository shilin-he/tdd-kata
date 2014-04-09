using System;
using System.Collections.Generic;

namespace sql_string_generator.query_play_ground
{
  internal static class TypeSystem
  {
    internal static Type GetElementType(Type type)
    {
      Type ienum = FindIEnumerable(type);
      if (ienum == null) return type;
      return ienum.GetGenericArguments()[0];
    }

    static Type FindIEnumerable(Type type)
    {
      if (type == null || type == typeof(string)) return null;

      if (type.IsArray) return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());

      if (type.IsGenericType)
      {
        foreach (var argument in type.GetGenericArguments())
        {
          Type ienum = typeof(IEnumerable<>).MakeGenericType(argument);
          if (ienum.IsAssignableFrom(type)) return ienum;
        }
      }

      Type[] interfaces = type.GetInterfaces();
      if (interfaces.Length > 0)
      {
        foreach (var i in interfaces)
        {
          Type ienum = FindIEnumerable(i);
          if (ienum != null) return ienum;
        }
      }

      if (type.BaseType != null && type.BaseType != typeof(object))
        return FindIEnumerable(type.BaseType);

      return null;
    }
  }
}