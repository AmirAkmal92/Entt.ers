using System;

namespace Entt.Ers
{
    public static class DbHelper
    {
        public static string ToNullableDateString(this DateTime? val)
        {
            return val.HasValue ? val.Value.ToString("dd/MM/yyyy") : string.Empty;
        }

        public static string ToNullableString(this object val)
        {
            return val?.ToString();
        }

        public static DateTime? ToNullableDateTime(this object date)
        {
            if (DBNull.Value == date) return null;
            DateTime dateTime;
            var isValid = DateTime.TryParse(date.ToString(), out dateTime);
            return isValid ? (DateTime?)dateTime : null;
        }

        public static DateTime ToDateTime(this object val)
        {
            if (null == val) return DateTime.MinValue;
            DateTime dateTime;
            var isValid = DateTime.TryParse(val.ToString(), out dateTime);
            return isValid ? dateTime : DateTime.MinValue;
        }

        public static DateTime ToDateTime(this DateTime? date)
        {
            return date ?? DateTime.MinValue;
        }

        public static decimal ToDecimal(this object val)
        {
            if (DBNull.Value == val) return 0;
            if (null == val) return 0;
            return Decimal.Parse(val.ToString());
        }

        public static float ToFloat(this object val)
        {
            if (DBNull.Value == val) return 0;
            if (null == val) return 0;
            return float.Parse(val.ToString());
        }

        public static int ToInt(this object val)
        {
            if (DBNull.Value == val) return 0;
            return int.Parse(val.ToString());
        }

        public static T? ReadNullable<T>(this object val) where T : struct
        {
            if (val == null)
            {
                return new T?(default(T));
            }
            if (val == DBNull.Value)
            {
                return null;
            }
            return new T?((T)val);
        }


    }
}