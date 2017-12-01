using System;
using System.Linq;
using System.Reflection;

namespace Common {
    [Obsolete("Naïve test, don't use. Really.")]
    internal static class Mapper {
        public static TTarget Map<TSource, TTarget>(this TSource source) where TTarget : new() {
            if (source == null) return default(TTarget);
            
            var sPropertyInfos = source.GetType().GetProperties(BindingFlags.Public);            
            var tPropertyInfos = typeof(TTarget)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite)
                .ToArray();
           
            var target = new TTarget();
            var sClassName = typeof(TSource).Name;
            foreach (var sPropInfo in sPropertyInfos) {
                var sPropName = sPropInfo.Name;
                var sNames = new[] { sClassName + sPropName, sPropName };
                var tPropInfo = tPropertyInfos
                    .Where(i => i.GetType() == sPropInfo.GetType())                                         
                    .FirstOrDefault(i => sNames.Contains(i.Name));
                tPropInfo?.SetValue(target, sPropInfo.GetValue(source));
            }
            return target;
        }
    }
}