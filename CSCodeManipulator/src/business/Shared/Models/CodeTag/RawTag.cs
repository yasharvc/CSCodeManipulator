namespace Shared.Models.CodeTag
{
    public class RawTag : Tag
    {
        public RawTag()
        {

        }
        public RawTag(string name) => Name = name;
        public override bool IsEligable(string exp) => false;
    }
}