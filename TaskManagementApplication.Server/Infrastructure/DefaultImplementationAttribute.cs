namespace TaskManagementApplication.Server.Infrastructure
{
    public class DefaultImplementationAttribute : Attribute
    {
        public Type Interface { get; set; }

        public string RegistrationName { get; set; }

        public DefaultImplementationAttribute() : this(null, null)
        {
        }

        public DefaultImplementationAttribute(Type interfaceType) : this(interfaceType, null)
        {
        }

        public DefaultImplementationAttribute(Type interfaceType, string registrationName)
        {
            Interface = interfaceType;
            RegistrationName = registrationName;
        }
    }
}
