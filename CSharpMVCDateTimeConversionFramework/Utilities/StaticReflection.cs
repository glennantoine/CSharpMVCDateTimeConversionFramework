using System;
using System.Linq.Expressions;
using System.Text;

namespace CSharpMVCDateTimeConversionFramework.Utilities 
{
    public class StaticReflection 
    {

        public static string GetMemberExpressionPropertyPath<T>(Expression<Func<T, object>> expression) 
        {
            if (expression == null) throw new ArgumentException("The expression cannot be null.");

            MemberExpression memberExpression;
            switch (expression.Body.NodeType) 
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var unaryExpression = expression.Body as UnaryExpression;
                    memberExpression = ((unaryExpression != null) ? unaryExpression.Operand : null) as MemberExpression;
                    break;
                case ExpressionType.Parameter:
                    return typeof(T).Name;
                default:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }

            if (memberExpression == null) throw new ArgumentException("Invalid expression");

            var result = new StringBuilder();

            while (memberExpression != null) 
            {
                result.Insert(0, "." + memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return typeof(T).Name + result;
        }

        public static string GetMemberExpressionPropertyName<T>(Expression<Func<T, object>> expression) 
        {
            return GetMemberExpressionPropertyPath(expression).Split('.')[1];
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression) 
        {
            if (expression == null) 
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression) 
        {
            if (expression == null) 
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }


        private static string GetMemberName(Expression expression) 
        {
            if (expression == null) 
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            if (expression is MemberExpression) 
            {
                // Reference type property or field
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression) 
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression) 
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(UnaryExpression unaryExpression) 
        {
            if (unaryExpression.Operand is MethodCallExpression) 
            {
                var methodExpression =
                    (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand)
                .Member.Name;
        }

    }
}