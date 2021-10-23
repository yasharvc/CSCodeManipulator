using System.Linq;

namespace Shared.Models.CodeTag.Tags
{
    public class ExpressionRenderTag : Tag
    {
        public string ExpressionKey
        {
            get => Properties.FirstOrDefault(m => m.Name == "key")?.Value ?? "";
            set
            {
                var prop = new TagProperty
                {
                    Name = "key",
                    Value = value
                };

                if (Properties.Any(m => m.Name == "key"))
                {
                    prop = Properties.FirstOrDefault(m => m.Name == "key");
                    prop.Value = value;
                }
                else
                {
                    Properties.Add(prop);
                }
            }
        }
        public ExpressionRenderTag()
        {
            Name = "expr";
            Properties.Add(new TagProperty { Name = "key", Value = "" });
        }
    }
}