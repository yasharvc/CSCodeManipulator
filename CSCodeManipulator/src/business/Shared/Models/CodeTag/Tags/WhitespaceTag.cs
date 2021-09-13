using System;

namespace Shared.Models.CodeTag.Tags
{
    public class WhitespaceTag : Tag
    {
        public WhitespaceTag() => Name = "whitespace";

        public override string Render()
        {
            throw new NotImplementedException();
        }
    }
}