using System.Diagnostics;

namespace TaskManagementApplication.Server.Infrastructure
{
    public class DependencyRegistrar
    {
        public static void RegisterDefaultImplementations(IServiceCollection service)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var publicTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                publicTypes.AddRange(assembly.GetTypes());
            }

            var interfaces = publicTypes.Where(x => x.IsInterface).ToList();
            var defaultImplementations = publicTypes.Where(type => Attribute.IsDefined(type, typeof(DefaultImplementationAttribute)));

            foreach (var type in defaultImplementations)
            {
                try
                {
                    var attribute = (DefaultImplementationAttribute)type.GetCustomAttributes(typeof(DefaultImplementationAttribute), false).Single();
                    Type typeClosure = type;
                    Debug.WriteLine(typeClosure.Name);
                    try
                    {
                        attribute.Interface = attribute.Interface ?? interfaces.Single(i => i.Name == "I" + typeClosure.Name);
                    }
                    catch (Exception ex)
                    {

                    }

                    service.AddScoped(attribute.Interface, type);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
