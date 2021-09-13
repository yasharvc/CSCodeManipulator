using Shared.Models.CodeTag.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Models.CodeTag
{
    public abstract class Tag
    {
        public const string Pipe = "¦";
        public const string TagCharacter = "¶";
        public const string Separator = "·";
        public const string TagStart = "«";
        public const string TagFinish = "»";
        public const string Equal = "˭";

        public string GetTagStart() => $"{TagStart}{Name}{TagCharacter}";
        public string GetTagFinish() => $"{TagCharacter}{Name}{TagFinish}";

        protected static IEnumerable<Tag> AllTags => new Tag[]
            {
                new KeywordTag()
            };

        public string Name { get; set; }
        public List<TagProperty> Properties { get; set; } = new List<TagProperty>();
        public string Body { get; set; }

        public virtual string Render() => Body.StartsWith(TagStart)
            ? ((Tag)Body).Render()
            : Body;
        public virtual bool IsEligable(string exp) => exp.StartsWith(GetTagStart());

        protected virtual string GetStringOfProperties() =>
            string.Join(Separator, Properties.Select(m => m.ToString()));
        public override string ToString() => $"{GetTagStart()}{GetStringOfProperties()}{Pipe}{Body}{GetTagFinish()}";

        public static implicit operator Tag(string exp)
        {
            var start = exp.IndexOf(TagStart);
            var end = exp.IndexOf(TagCharacter, start);

            Tag tagFromString = AllTags.FirstOrDefault(m => m.IsEligable(exp));
            Tag res = tagFromString != null ?
                (Tag)Activator.CreateInstance(tagFromString.GetType())
                : new RawTag();

            if(end > start && start != -1 && end != -1)
            {
                try
                {
                    res.Name = exp[(start + 1)..end];
                    start = end;
                    end = exp.IndexOf(Pipe, start);
                    var props = exp[(start + 1)..end];
                    if (!string.IsNullOrWhiteSpace(props))
                    {
                        var lst = props.Split(Separator);
                        foreach (var item in lst)
                        {
                            res.Properties.Add(item);
                        }
                    }
                    start = end;
                    end = exp.LastIndexOf(res.GetTagFinish());

                    res.Body = exp[(start + 1)..end];
                    return res;
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }
            throw new InvalidCastException();
        }  
    }
}