namespace Business.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ParseServiceFixture
    {
        private IParseService parserService { get; }

        public ParseServiceFixture()
        {
        }

        [Fact]
        public async Task Test_Success_Async()
        {
            string testFile = "./TestFiles/test.txt";

            (int lineNumber, int[] badLines) = await ParseService.ParseFileAsync(testFile);

            Assert.Equal(9, lineNumber);
            Assert.Equal(5, badLines.Length);
        }
    }
}
