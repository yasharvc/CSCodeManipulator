namespace Shared.Models.CodeTag
{
    public class RawTag : Tag
    {
        public override bool IsEligable(string exp) => false;
    }
}