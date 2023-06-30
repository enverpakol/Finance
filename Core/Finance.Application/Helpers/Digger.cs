using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Helpers
{
    public static class Digger
    {
        public static Type GetType(Type type, string propertyName)
        {
            if (type == null && propertyName.Contains("."))
            {
                return null;
            }

            string[] array = propertyName.Split('.');
            for (int i = 0; i < array.Count() - 1; i++)
            {
                PropertyInfo property = type.GetProperty(array[i]);
                if (property != null)
                {
                    type = property.PropertyType;
                }
            }

            propertyName = array[array.Count() - 1];
            if (type != null)
            {
                if (!(type.GetProperty(propertyName) != null))
                {
                    return null;
                }

                return type.GetProperty(propertyName)!.PropertyType;
            }

            return null;
        }

        public static dynamic GetObjectValue(dynamic source, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return null;
            }

            string[] array = propertyName.Split('.');
            for (int i = 0; i < array.Count() - 1; i++)
            {
                if (!((source == null) ? true : false))
                {
                    dynamic property = source.GetType().GetProperty(array[i]);
                    if (property != null)
                    {
                        source = property.GetValue(source, null);
                    }
                }
            }

            propertyName = array[array.Count() - 1];
            if (source != null)
            {
                if (source is ExpandoObject)
                {
                    IDictionary<string, object> dictionary = source as IDictionary<string, object>;
                    if (!dictionary.ContainsKey(propertyName))
                    {
                        return string.Empty;
                    }

                    return dictionary[propertyName];
                }

                return source.GetType().GetProperty(propertyName)?.GetValue(source, null);
            }

            return null;
        }

        public static void SetObjectValue(object source, string propertyName, object value)
        {
            PropertyInfo property = source.GetType().GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(source, value, null);
            }
        }

        public static string GetDisplayName(Type type, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return string.Empty;
            }

            string[] array = fieldName.Split('.');
            if (array.Count() > 1)
            {
                for (int i = 0; i < array.Count() - 1; i++)
                {
                    PropertyInfo property = type.GetProperty(array[i]);
                    if (property != null)
                    {
                        type = property.PropertyType;
                    }
                }

                fieldName = array[array.Count() - 1];
            }

            return fieldName;
        }

        public static string GetClassName(Type type, bool clearTire = false)
        {
            string text = type.Name;
            if (text.IndexOf("_", StringComparison.Ordinal) > -1)
            {
                text = text.Split('_')[0];
            }

            return text;
        }

        public static bool IsTypeOf(Type source, Type target)
        {
            while (true)
            {
                if (source == target)
                {
                    return true;
                }

                if (!(source.BaseType != null))
                {
                    break;
                }

                source = source.BaseType;
            }

            return false;
        }

        public static List<string> GetAttributePropertyList<T>(Type type) where T : class
        {
            List<string> list = new List<string>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetCustomAttributes(typeof(T), inherit: false).Any())
                {
                    list.Add(propertyInfo.Name);
                }
            }

            return list;
        }

        public static Dictionary<string, T> GetAttributePropertyList2<T>(Type type) where T : class
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                List<object> source = propertyInfo.GetCustomAttributes(typeof(T), inherit: false).ToList();
                if (source.Any())
                {
                    dictionary.Add(propertyInfo.Name, source.First() as T);
                }
            }

            return dictionary;
        }

        public static T GetAttribute<T>(Type type, bool searchProperties = true) where T : class
        {
            if (searchProperties)
            {
                PropertyInfo[] properties = type.GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    object[] customAttributes = properties[i].GetCustomAttributes(typeof(T), inherit: false);
                    if (customAttributes.Any())
                    {
                        return customAttributes.First() as T;
                    }
                }
            }
            else
            {
                object[] customAttributes2 = type.GetCustomAttributes(typeof(T), inherit: false);
                if (customAttributes2.Any())
                {
                    return customAttributes2.First() as T;
                }
            }

            return null;
        }

        public static T[] GetAttributeList<T>(Type type, bool searchProperties = true) where T : class
        {
            if (searchProperties)
            {
                PropertyInfo[] properties = type.GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    object[] customAttributes = properties[i].GetCustomAttributes(typeof(T), inherit: false);
                    if (customAttributes.Count() > 0)
                    {
                        return customAttributes as T[];
                    }
                }
            }
            else
            {
                object[] customAttributes2 = type.GetCustomAttributes(typeof(T), inherit: false);
                if (customAttributes2.Count() > 0)
                {
                    return customAttributes2 as T[];
                }
            }

            return null;
        }
    }
}
