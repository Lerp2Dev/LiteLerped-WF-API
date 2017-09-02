using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiteLerped_WF_API.Classes.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetAllDerivedClasses(this Type typ)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typ));
        }

        public static T TryGetFormByType<T>()
        {
            return typeof(T).TryGetFormByType<T>();
        }

        public static T TryGetFormByType<T>(this Type typ)
        {
            Type formType = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(a => a == typ)
                 .FirstOrDefault();

            if (formType == null) // If there is no form with the given frmname
                return default(T);

            return (T) Activator.CreateInstance(formType);
        }
    }
}