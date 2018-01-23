using System;
using System.Collections.Generic;
using System.Linq;

namespace Fora
{
    public static class Extensions
    {
        public static T ParseEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse(value, true, out T enumobj))
            {
                return enumobj;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// Update properties with properties of the object Supplied (typically anonymous)
        /// </summary>
        /// <typeparam name="T">Type of Source Object</typeparam>
        /// <param name="destination">Object whose property you want to update</param>
        /// <param name="source">destination object (typically anonymous) you want to take values from</param>
        /// <returns>Update reference to same Object</returns>
        public static T Assign<T>(this T destination, object source, params string[] ignoredProperties)
        {
            if (ignoredProperties == null) ignoredProperties = new string[0];
            if (destination != null && source != null)
            {
                var query = from sourceProperty in source.GetType().GetProperties()
                            join destProperty in destination.GetType().GetProperties()
                            on sourceProperty.Name.ToLower() equals destProperty.Name.ToLower()             //Case Insensitive Match
                            where !ignoredProperties.Contains(sourceProperty.Name)
                            where destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)   //Properties can be Assigned
                            where destProperty.GetSetMethod() != null                                       //Destination Property is not Readonly
                            select new { sourceProperty, destProperty };


                foreach (var pair in query)
                {
                    //Go ahead and Assign the value on the destination
                    pair.destProperty
                        .SetValue(destination,
                            value: pair.sourceProperty.GetValue(obj: source, index: new object[] { }),
                            index: new object[] { });
                }
            }
            return destination;
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0) pageIndex = 1;
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex == 0) pageIndex = 1;
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }

        public static PagedList<U> Map<T, U>(this PagedList<T> pagedList, Func<T, U> projection)
        {
            var list = new List<U>();
            foreach (var item in pagedList)
            {
                list.Add(projection(item));
            }
            return new PagedList<U>(list, pagedList.Total, pagedList.PageIndex, pagedList.PageSize);
        }
    }
}
