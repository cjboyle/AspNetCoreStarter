using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreStarter.Domain;

namespace AspNetCoreStarter.Infrastructure
{
    public static class EntityExtensions
    {
        private static IEnumerable<PropertyInfo> GetIntersectingProperties(Type t1, Type t2)
        {
            return t1.GetProperties(BindingFlags.Public).Intersect(t2.GetProperties(BindingFlags.Public));
        }

        //public static TResult MapTo<TEntity, TResult>(this TEntity entity)
        //    where TEntity : class, IEntity<TEntity>
        //{
        //    var result = Activator.CreateInstance<TResult>();

        //    foreach (var prop in GetIntersectingProperties(typeof(TEntity), typeof(TResult)))
        //    {
        //    }
        //}
    }
}
