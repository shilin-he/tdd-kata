using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace sql_string_generator.query_play_ground
{
  public abstract class QueryProvider : IQueryProvider
  {
    public IQueryable CreateQuery(Expression expression)
    {
      Type element_type = TypeSystem.GetElementType(expression.Type);
      try
      {
        return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(element_type), new object[] { this, expression });
      }
      catch (TargetInvocationException tie)
      {
        throw tie.InnerException;
      }
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
      return new Query<TElement>(this, expression);
    }

    object IQueryProvider.Execute(Expression expression)
    {
      return Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
      return (TResult) Execute(expression);
    }

    public abstract string GetQueryText(Expression expression);
    public abstract object Execute(Expression expression);
  }
}