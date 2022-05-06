using System.Linq.Expressions;
using System.Reflection;

namespace ProgettoCinema.ClientWeb.Extensions
{
    public static class FilterExtension
    {
        public static Expression<Func<T, bool>> GetFilterQuery<T>(this string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null!;

            var filterQuery = filter.Split(';', StringSplitOptions.TrimEntries);
            PropertyInfo propertyInfo = typeof(T).GetProperty(filterQuery[0]);
            var parameter = Expression.Parameter(typeof(T));

            if (filterQuery[1].ToLower() == "null")
            {
                var result = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.Constant(null, propertyInfo.PropertyType),
                        Expression.Property(parameter, propertyInfo)
                    ),
                    parameter);
                return result;
            }
            else
            {
                var constant = filterQuery[1].Convert(propertyInfo.PropertyType);

                var result = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.Constant(constant, constant.GetType()),
                        Expression.Convert(Expression.Property(parameter, propertyInfo), constant.GetType())
                    ),
                    parameter);
                return result;
            }
        }
        private static object Convert(this object value, Type t)
        {
            Type underlyingType = Nullable.GetUnderlyingType(t);

            if (underlyingType != null && value == null)
            {
                return null!;
            }
            Type basetype = underlyingType == null ? t : underlyingType;
            return System.Convert.ChangeType(value, basetype);
        }
    }
}
