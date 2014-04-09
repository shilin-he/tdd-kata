using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Rhino.Mocks;

namespace sql_string_generator.query_play_ground
{
  public class QueryTranslator : ExpressionVisitor
  {
    StringBuilder sb;

    internal QueryTranslator() { }

    internal string Translate(Expression expression)
    {
      sb = new StringBuilder();
      Visit(expression);
      return sb.ToString();
    }

    private static Expression StripQuotes(Expression e)
    {
      while (e.NodeType == ExpressionType.Quote)
      {
        e = ((UnaryExpression)e).Operand;
      }
      return e;
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
      if (node.Method.DeclaringType != typeof(Queryable) || node.Method.Name != "Where")
        throw new NotSupportedException(string.Format("The method '{0}' is not supported.", node.Method.Name));

      sb.Append("SELECT * FROM (");
      Visit(node.Arguments[0]);
      sb.Append(") AS T WHERE ");
      var lamda = (LambdaExpression)StripQuotes(node.Arguments[1]);
      Visit(lamda.Body);
      return node;
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
      switch (node.NodeType)
      {
        case ExpressionType.Not:
          sb.Append(" NOT ");
          Visit(node.Operand);
          break;
        default:
          throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", node.NodeType));
      }

      return node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
      sb.Append("(");
      Visit(node.Left);

      switch (node.NodeType)
      {
        case ExpressionType.And:
          sb.Append(" AND ");
          break;
        case ExpressionType.Or:
          sb.Append(" OR ");
          break;
        case ExpressionType.Equal:
          sb.Append(" = ");
          break;
        case ExpressionType.NotEqual:
          sb.Append(" <> ");
          break;
        case ExpressionType.GreaterThan:
          sb.Append(" > ");
          break;
        case ExpressionType.GreaterThanOrEqual:
          sb.Append(" >= ");
          break;
        case ExpressionType.LessThan:
          sb.Append(" < ");
          break;
        case ExpressionType.LessThanOrEqual:
          sb.Append(" <= ");
          break;
        default:
          throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", node.NodeType));
      }

      Visit(node.Right);
      sb.Append(")");
      return node;
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
      var q = node.Value as IQueryable;
      if (q != null)
      {
        sb.Append("SELECT * FROM ");
        sb.Append(q.ElementType.Name);
      }
      else if (node.Value == null)
      {
        sb.Append("NULL");
      }
      else
      {
        switch (Type.GetTypeCode(node.Value.GetType()))
        {
          case TypeCode.Boolean:
            sb.Append(((bool) node.Value) ? 1 : 0);
            break;
          case TypeCode.String:
            sb.Append("'");
            sb.Append(node.Value);
            sb.Append("'");
            break;
          case TypeCode.Object:
            throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", node.Value));
          default:
            sb.Append(node.Value);
            break;
        }
      }

      return node;
    }

    protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
    {
      if (node.Expression.NodeType != ExpressionType.Parameter)
        throw new NotSupportedException(string.Format("The member '{0}' is not supported", node.Member.Name));

      sb.Append(node.Member.Name);

      return node;
    }
  }
}