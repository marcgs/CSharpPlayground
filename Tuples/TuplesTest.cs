namespace Tuples
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TuplesTest
    {
        [Fact]
        public void Fibonacci()
        {
            var fibGen = new FibGen();
            
            Assert.Equal(1, fibGen.NextFib());
            Assert.Equal(2, fibGen.NextFib());
            Assert.Equal(3, fibGen.NextFib());
            Assert.Equal(5, fibGen.NextFib());
            Assert.Equal(8, fibGen.NextFib());
            Assert.Equal(13, fibGen.NextFib());
            Assert.Equal(21, fibGen.NextFib());
        }
        
        [Fact]
        public void FibonacciSequence()
        {
            var fibGen = new FibGen();

            Assert.Equal(new long[] {1, 2, 3, 5, 8, 13, 21}, fibGen.Fib(22).ToArray());
        }

    }

    public class FibGen
    {
        private long prev = 1;
        private long current = 1;

        public long NextFib()
        {
            (prev, current) = (current, prev + current);
            return prev;
        }

        public IEnumerable<long> Fib(long max)
        {
            var next = NextFib();
            while (next <= max)
            {
                yield return next;
                next = NextFib();
            }
        }
    }
}