using System.Collections;
using System.Collections.Generic;

namespace max.web.utils
{
  public static class IEnumerableExtensions
  {
    public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
    {
      foreach (var item in items)
      {
        yield return item;
      }
    }
  }
}