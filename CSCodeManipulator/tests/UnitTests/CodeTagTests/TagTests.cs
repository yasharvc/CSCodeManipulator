using Shared.Models.CodeTag;
using Shared.Models.CodeTag.Tags;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CodeTagTests
{
    public class TagTests
    {
        [Fact]
        public void Should_Parse_Tag()
        {
            var tag = new RawTag
            {
                Name = "Test",
                Body = "body",
                Properties = new List<TagProperty>()
            };

            var res = (Tag)tag.ToString();

            res.ShouldNotBeNull();
            res.Name.ShouldBe(tag.Name);
            res.Properties.ShouldBeEmpty();
            res.Body.ShouldBe(tag.Body);
        }
        [Fact]
        public void Should_Parse_Tag_With_Property()
        {
            var tag = new RawTag
            {
                Name = "keyword",
                Body = "using",
                Properties = new List<TagProperty>
                {
                    new TagProperty
                    {
                        Name = "type",
                        Value = "static"
                    }
                }
            };

            var res = (Tag)tag.ToString();

            res.ShouldNotBeNull();
            res.Name.ShouldBe(tag.Name);
            res.Body.ShouldBe(tag.Body);
            res.Properties.ShouldNotBeEmpty();
            res.Properties.Count.ShouldBe(1);
            res.Properties.First().Name.ShouldBe(tag.Properties.First().Name);
            res.Properties.First().Value.ShouldBe(tag.Properties.First().Value);
        }
        [Fact]
        public void Should_Render_keyword()
        {
            var usingTag = new KeywordTag { Body = "using" };
            var wsTag = new WhitespaceTag { Body = $"\t " };

            var res = wsTag.Render();

            res.ShouldNotBeEmpty();

            res.ShouldBe("\t ");

            res = usingTag.Render();

            res.ShouldBe("using");
        }
        [Fact]
        public void Should_Render_Tag_Inside_Tag()
        {
            var usingTag = new KeywordTag { Body = "using" };
            var wsTag = new WhitespaceTag { Body = $"\t {usingTag}" };

            var res = wsTag.Render();

            res.ShouldNotBeEmpty();

            res.ShouldBe("\t using");
        }
        [Fact]
        public void Should_Render_Tags()
        {
            var usingTag = new KeywordTag { Body = "using" };
            var wsTag = new WhitespaceTag { Body = $"\t\t" };
            var ws2Tag = new WhitespaceTag { Body = $"\t {usingTag}{wsTag}" };

            var res = ws2Tag.Render();

            res.ShouldNotBeEmpty();

            res.ShouldBe("\t using\t\t");
        }
    }
}