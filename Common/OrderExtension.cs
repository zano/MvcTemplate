using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Common {
    using static ExpressionHelper;

    public static class OrderExtension {
        
        /// <summary>
        /// Applies a sequence of <code>OrderBy</code> on an IQueryable. 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="propertyNames">
        ///     Name of the property of <code>T</code> that should be used for ordering. 
        ///     If prefixed with a <code>-</code> (minus), the order is descending. 
        /// </param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByMany<T>(this IQueryable<T> queryable, params string[] propertyNames) {
            
            bool isDescending(string p, out string propName) {                
                propName = Regex.Replace(p, "/^-/", "");                
                return p != propName;                
            }

            if (!propertyNames.Any()) return (IOrderedQueryable<T>) queryable;


            var desc = isDescending(propertyNames[0], out var propertyName);
            var prop = Property<T>(propertyName);
            var orderedQueryable = desc
                ? queryable.OrderByDescending(prop)
                : queryable.OrderBy(prop);
            
            foreach (var propName in propertyNames.Skip(1)) {
                 desc = isDescending(propName, out propertyName);
                 prop = Property<T>(propertyName);
                 orderedQueryable = desc
                    ? orderedQueryable.ThenByDescending(prop)
                    : orderedQueryable.ThenBy(prop);
            }
            
            return orderedQueryable;
        }

        
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> q, string property) {
            return q.OrderBy(Property<T>(property));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> q, string property)
            => q.OrderByDescending(Property<T>(property));

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> q, string property)
            => q.ThenBy(Property<T>(property));

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> q, string property)
            => q.ThenBy(Property<T>(property));
    }

    public static class ExpressionHelper {
        public static Expression<Func<T, object>> Property<T>(string property) {
            return LambdaCache<T>.GetOrAdd(property, () => PropertyUsingLinq<T>(property));
        }

        private static Expression<Func<T, object>> PropertyUsingType<T>(string property) {
            var propertyInfo = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(info => info.Name == property);
            if (propertyInfo == null) return null;
            return x => propertyInfo.GetValue(x, null);
        }

        private static Expression<Func<T, object>> PropertyUsingLinq<T>(string property) {
            var constantExpression = Expression.Constant(null, typeof(T));
            var memberExpression = Expression.Property(constantExpression, property);
            var unaryExpression = Expression.Convert(memberExpression, typeof(object));

            return Expression.Lambda<Func<T, object>>(unaryExpression);
        }

        private static class LambdaCache<T> {
            private static readonly ConcurrentDictionary<string, Expression<Func<T, object>>>
                Cache = new ConcurrentDictionary<string, Expression<Func<T, object>>>();

            public static Expression<Func<T, object>>
                GetOrAdd(string property, Func<Expression<Func<T, object>>> getLambda)
                => Cache.GetOrAdd(property, getLambda());
        }
    }
}
