using Shared.Models.CodeTag;
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
    }
}