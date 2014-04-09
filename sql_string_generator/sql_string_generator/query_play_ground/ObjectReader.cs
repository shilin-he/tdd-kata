using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace sql_string_generator.query_play_ground
{
  public class ObjectReader<T> : IEnumerable<T>, IEnumerable where T : class, new()
  {
    Enumerator enumerator;

    public ObjectReader(DbDataReader reader)
    {
      enumerator = new Enumerator(reader);
    }

    public IEnumerator<T> GetEnumerator()
    {
      Enumerator e = enumerator;
      if (e == null) throw new InvalidOperationException("Cannot enumerate more than once.");
      enumerator = null;
      return e;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
    {
      readonly FieldInfo[] fields;
      readonly DbDataReader reader;
      T current;
      int[] fieldLookup;

      public Enumerator(DbDataReader reader)
      {
        this.reader = reader;
        fields = typeof(T).GetFields();
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        if (reader.Read())
        {
          if (fieldLookup == null)
          {
            InitFieldLookup();
          }

          var instance = new T();

          for (int i = 0, n = fields.Length; i < n; i++)
          {
            int index = fieldLookup[i];
            if (index < 0) continue;
            FieldInfo fi = fields[i];
            fi.SetValue(instance, reader.IsDBNull(index) ? null : reader.GetValue(index));
          }

          current = instance;
          return true;
        }

        return false;
      }

      public void Reset() { }

      public T Current
      {
        get { return current; }
      }

      object IEnumerator.Current
      {
        get { return Current; }
      }

      void InitFieldLookup()
      {
        var map = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

        for (int i = 0, n = reader.FieldCount; i < n; i++)
        {
          map.Add(reader.GetName(i), i);
        }

        fieldLookup = new int[fields.Length];

        for (int i = 0, n = fields.Length; i < n; i++)
        {
          int index;

          if (map.TryGetValue(fields[i].Name, out index))
          {
            fieldLookup[i] = index;
          }
          else
          {
            fieldLookup[i] = -1;
          }
        }
      }
    }
  }
}