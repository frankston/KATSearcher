using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal static class Methods
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string RandomCharString(int length)
        {
            char[] c = new char[length];
            for (int i = 0; i < length; i++) c[i] = (char)random.Next(97, 122);
            return new string(c);
        }

        public static bool AllPropertiesAreDefaultValues<T>(T classInstance)
        {
            foreach (var property in classInstance.GetType().GetProperties())
            {
                var name = property.Name; var value = property.GetValue(classInstance, null);
                if (!AreEqual(value, GetDefaultValue(property.PropertyType))) return false;
            }
            return true;
        }

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        private static bool AreEqual<T>(T a, T b)
        {
            return EqualityComparer<T>.Default.Equals(a, b);
        }
    }
}