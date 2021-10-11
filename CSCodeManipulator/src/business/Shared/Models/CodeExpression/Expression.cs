using Shared.Models.CodeTag;
using System.Collections.Generic;

namespace Shared.Models.CodeExpression
{
    public abstract class Expression
    {
        public virtual bool IsBlock => false;
        public IEnumerable<Tag> PrintTags { get; } = new List<Tag>();
        public abstract bool IsCodeStartingEligable(string code);
        public abstract bool IsCodeContinuesEligible(string code);
        public abstract int Compile(string code);
        public virtual string Render()
        {
            var res = "";
            foreach (var item in PrintTags)
            {
                res += item.Render();
            }
            return res;
        }
        protected void AddPrintTag(Tag tag) => (PrintTags as List<Tag>).Add(tag);
    }
}