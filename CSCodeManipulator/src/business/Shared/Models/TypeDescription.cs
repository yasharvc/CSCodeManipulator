namespace Shared.Models
{
    public class TypeDescription
    {
        public string Name { get; set; }
        public string Namespace1 { get; }
        public string Namespace { get; set; }
        public string FullName => $"{Namespace}{(string.IsNullOrEmpty(Namespace) ? "" : ".")}{Name}";
        public bool IsTypeMatch(string typeName) => typeName == Name || FullName == typeName || FullName.EndsWith(typeName);

        public TypeDescription() : this("", "")
        {

        }

        public TypeDescription(string @namespace, string name)
        {
            Name = name;
            Namespace1 = @namespace;
        }

        public TypeDescription(string name) : this("", name) { }

        public static implicit operator TypeDescription(string typeName)
        {
            var lastIndex = typeName.LastIndexOf(".");
            return new TypeDescription
            {
                 Name = lastIndex != -1 ?
                 typeName[(lastIndex+1)..]:
                 typeName,
                 Namespace = lastIndex != -1?
                 typeName[..lastIndex]:
                 ""
            };
        }
    }
}
