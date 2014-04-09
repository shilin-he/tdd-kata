using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace sql_string_generator.query_play_ground
{
  public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
  {
    QueryProvider provider;
    Expression expression;

    public Query(QueryProvider provider)
    {
      if (provider == null) throw new ArgumentNullException("provider");
      this.provider = provider;
      this.expression = Expression.Constant(this);
    }

    public Query(QueryProvider provider, Expression expression)
    {
      if (provider == null) throw new ArgumentNullException("provider");
      if (expression == null) throw new ArgumentNullException("expression");
      if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) throw new ArgumentOutOfRangeException("expression");
      this.provider = provider;
      this.expression = expression;
    }

    public IEnumerator<T> GetEnumerator()
    {
      return ((IEnumerable<T>)provider.Execute(expression)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public Expression Expression
    {
      get { return expression; }
    }

    public Type ElementType
    {
      get { return typeof(T); }
    }

    public IQueryProvider Provider
    {
      get { return provider; }
    }

    public override string ToString()
    {
      return provider.GetQueryText(expression);
    }
  }
}