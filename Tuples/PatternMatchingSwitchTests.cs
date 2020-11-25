namespace Tuples
{
    using System;
    using Xunit;

    public class PatternMatchingSwitchTests
    {
        [Fact]
        public void PatternMatchingSwitch()
        {
            Func<(int, bool, string),string> fn = val => val switch
            {
                (1, true, "bla") => "first",
                (1, false, "bla") => "second",
                (2, _, "bli") => "third",
                (_, _, "bli") => "fourth",
                (_, _, _) => "fifth",
            };
            Assert.Equal("first", fn((1, true, "bla")));
            Assert.Equal("second", fn((1, false, "bla")));
            Assert.Equal("third", fn((2, true, "bli")));
            Assert.Equal("third", fn((2, false, "bli")));
            Assert.Equal("fourth", fn((1, true, "bli")));
            Assert.Equal("fifth", fn((1, true, "blo")));
        }

    }
}