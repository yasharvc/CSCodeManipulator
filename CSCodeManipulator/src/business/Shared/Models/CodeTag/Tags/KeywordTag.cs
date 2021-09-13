namespace Shared.Models.CodeTag.Tags
{
    public class KeywordTag : Tag
    {
        public KeywordTag() => Name = "keyword";

        public override string Render() => Body.StartsWith(TagStart)
            ? ((Tag)Body).Render()
            : Body;
    }
}