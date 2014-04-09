using System;
using System.Linq.Expressions;
using System.Text;

namespace sql_string_generator
{
  public class ExpressionToSqlTranslator<TModel> : ExpressionVisitor, IExpressionToSqlTranslator<TModel>
  {
    StringBuilder sb;
    IMapModelToTable<TModel> mapping;

    public ExpressionToSqlTranslator(IMapModelToTable<TModel> mapping)
    {
      this.mapping = mapping;
    }

    public string translate(Expression<Func<TModel, bool>> expr)
    {
      sb = new StringBuilder();
      Visit(expr);
      return sb.ToString();
    }

    protected override Expression VisitMember(MemberExpression node)
    {
      var prop_name = node.Member.Name;
      sb.AppendFormat("[{0}].{1}", mapping.get_table_name(), mapping.get_column_name(prop_name));
      return node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
      Visit(node.Left);

      switch (node.NodeType)
      {
        case ExpressionType.And:
        case ExpressionType.AndAlso:
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

    protected override Expression VisitConstant(ConstantExpression node)
    {
      if (node.Value == null)
      {
        sb.Append("NULL");
      }
      else
      {
        switch (Type.GetTypeCode(node.Value.GetType()))
        {
          case TypeCode.Boolean:
            sb.Append(((bool)node.Value) ? 1 : 0);
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
  }
}