using Shared.Data;
using Shared.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.GeneralProgramming
{
    public class TypeClassTests
    {
        [Fact]
        public void Check_GetType_Function()
        {
            var typeString = typeof(TypeClassTests).FullName;

            var type = Type.GetType(typeString);

            Assert.NotNull(type);
            type.FullName.ShouldBe(typeString);
        }

        [Fact]
        public void Check_int_For_GetType()
        {
            TypeDescription x = "System.Int32";

            x.IsTypeMatch("Int32").ShouldBeTrue();
            x.IsTypeMatch("System.Int32").ShouldBeTrue();
        }
    }
}
