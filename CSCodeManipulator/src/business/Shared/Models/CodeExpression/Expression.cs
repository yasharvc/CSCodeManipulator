using Shared.Models.CodeTag;
using Shared.Models.CodeTag.Tags;
using System.Collections.Generic;

namespace Shared.Models.CodeExpression
{
    public abstract class Expression
    {
        public IEnumerable<Tag> PrintTags { get; } = new List<Tag>();
        public Dictionary<string, Expression> InnerExpressions { get; set; } = new Dictionary<string, Expression>();
        public abstract bool IsCodeStartingEligable(string code);
        public abstract bool IsCodeContinuesEligible(string code);
        public abstract int Compile(string code);
        public virtual string Render()
        {
            var res = "";
            foreach (var item in PrintTags)
            {
                if (item is ExpressionRenderTag)
                {
                    var r = item as ExpressionRenderTag;
                    res += InnerExpressions[r.ExpressionKey].Render();
                }
                else
                {
                    res += item.Render();
                }
            }
            return res;
        }
        protected void AddPrintTag(Tag tag) => (PrintTags as List<Tag>).Add(tag);
    }
}