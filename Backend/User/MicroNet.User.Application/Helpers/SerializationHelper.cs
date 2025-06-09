using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace MicroNet.User.Application.Helpers
{
    public class SerializationHelper
    {
        public static JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new IgnoreVirtualMembersResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Avoid self-referencing loops
                    NullValueHandling = NullValueHandling.Ignore // Ignore null values
                };
            }
        }
    }

    public class IgnoreVirtualMembersResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            // List all non-virtual properties
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.GetGetMethod()!.IsVirtual)
                .Select(p => base.CreateProperty(p, memberSerialization))
                .ToList();

            props.ForEach(p => { p.Ignored = false; }); // Do not ignore them in serialization
            return props;
        }
    }
}
