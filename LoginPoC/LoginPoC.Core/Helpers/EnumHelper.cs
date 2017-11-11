using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LoginPoC.Core.Helpers
{
    public static class EnumHelper<T> where T : struct, IConvertible
    {
        public static IList<T> GetValues()
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(typeof(T), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames()
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues()
        {
            return GetNames().Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes.Any() && descriptionAttributes[0].ResourceType != null)
                    return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public static IEnumerable<KeyValuePair<int, string>> AsKeyValuePairs()
        {
            return GetValues()
                  .Select(v => new KeyValuePair<int, string>(Convert.ToInt32(v), GetDisplayValue(v)))
                  .ToList();
        }
    }
}