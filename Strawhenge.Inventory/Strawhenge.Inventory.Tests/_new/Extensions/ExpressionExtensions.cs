using System.Linq.Expressions;
using System.Reflection;

namespace Strawhenge.Inventory.Tests._new
{
    static class ExpressionExtensions
    {
        public static MethodInfo GetMethodInfo<T>(this Expression<T> expression)
        {
            if (expression.Body is UnaryExpression
                {
                    Operand: MethodCallExpression
                    {
                        Object: ConstantExpression
                        {
                            Value: MethodInfo methodInfo
                        }
                    }
                })
            {
                return methodInfo;
            }

            throw new TestSetupException($"Invalid expression - could not determine {nameof(MethodInfo)}.");
        }
    }
}