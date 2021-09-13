namespace Shared.Models.CodeTag
{
    public class TagProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Name}{Tag.Equal}{Value}";

        public static implicit operator TagProperty(string exp)
        {
            var name = exp[..exp.IndexOf(Tag.Equal)];
            var val = exp[(exp.IndexOf(Tag.Equal) + 1)..];
            return new TagProperty { Name = name, Value = val };
        }
    }
}