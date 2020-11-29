using System;
using Xunit;

namespace AsyncAwait
{
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    public class AsyncAwaitTests
    {
        [Fact]
        public async void AsyncAwaitException()
        {
            var exception = new Exception("huere siech");
            Exception catched = null;
            
            var t1 = Task.Run(fn("one"));
            var t2 = Task.Run(fn(12));
            var tex = Task.Run(fn(exception));
            
            try
            {
                await Task.WhenAll(t1, t2, tex);
            }
            catch (Exception e)
            {
                catched = e;
            }
            
            Assert.Equal("one", await t1.ConfigureAwait(false));
            Assert.Equal(12, await t2.ConfigureAwait(false));
            Assert.Same(exception, catched);
        }
        
        [Fact]
        public async void AsyncAwaitMultipleExceptions()
        {
            var exception1 = new Exception("huere siech");
            var exception2 = new Exception("godverdaminomal");
            Exception catched = null;
            
            var tex1 = Task.Run(fn(exception1));
            var tex2 = Task.Run(fn(exception2));
            
            try
            {
                await Task.WhenAll(tex1, tex2);
            }
            catch (Exception e)
            {
                catched = e;
            }
            
            Assert.Same(exception1, catched);
        }

        private static Func<Task<T>> fn<T>(T result)
        {
            return async () =>
            {
                await Task.Delay(10);
                return result;
            };
        }
        
        private static Func<Task<string>> fn(Exception ex)
        {
            return async () =>
            {
                await Task.Delay(5);
                throw ex;
            };
        }
    }
}