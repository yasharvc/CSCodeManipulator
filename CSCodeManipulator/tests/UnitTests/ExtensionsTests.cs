using Shared;
using Shouldly;
using Xunit;

namespace UnitTests
{
    public class ExtensionsTests
    {
        [Fact]
        public void NextWord_Should_ReturnSpace()
        {
            var exp = " Test";
            var expected = " ";

            exp.NextWord().ShouldBe(expected);
        }
        [Fact]
        public void NextWord_Should_ReturnSpaceAndTab()
        {
            var exp = " \tTest";
            var expected = " \t";

            exp.NextWord().ShouldBe(expected);
        }
        [Fact]
        public void NextWord_Should_Return_Test()
        {
            var exp = " \tTest";
            var expected = "Test";

            exp.NextWord(2).ShouldBe(expected);
        }
    }
}
