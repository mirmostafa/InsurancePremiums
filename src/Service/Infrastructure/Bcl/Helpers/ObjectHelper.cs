using System.Linq.Expressions;
using System.Reflection;

namespace Service.Infrastructure.Bcl.Helpers;

public static class ObjectHelper
{
    public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
    {
        var type = typeof(TSource);

        if (propertyLambda.Body is not MemberExpression member)
        {
            throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));
        }

        var propInfo = member.Member as PropertyInfo;
        return propInfo == null
            ? throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()))
            : type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType)
            ? throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), type))
            : propInfo;
    }
}