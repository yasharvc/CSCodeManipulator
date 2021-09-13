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

        public string Name { get; set; }
        public List<TagProperty> Properties { get; set; }
        public string Body { get; set; }

        public abstract string Render();
        public abstract bool IsEligable(string exp);

        protected virtual string GetStringOfProperties() =>
            string.Join(Separator, Properties.Select(m => m.ToString()));
        public override string ToString() => $"{GetTagStart()}{GetStringOfProperties()}{Pipe}{Body}{GetTagFinish()}";

        public static implicit operator Tag(string exp)
        {
            var start = exp.IndexOf(TagStart);
            var end = exp.IndexOf(TagCharacter);

            var res = new RawTag
            {
                Properties = new List<TagProperty>()
            };

            if(end > start && start != -1 && end != -1)
            {
                try
                {
                    res.Name = exp[start..(end - 1)];
                    start = end;
                    end = exp.IndexOf(Pipe);
                    var props = exp[start..(end - 1)];
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

                    res.Body = exp[start..(end - 1)];
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