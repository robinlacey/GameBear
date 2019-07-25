using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class SaveNewGameDataTests
    {
        public class GivenValidInput
        {
            [Test]
            public void ChecksIfSessionIDDoesNotExist()
            {
                Assert.Fail();
            }
            [Test]
            public void IfNoSessionIDRunsSaveUseCase()
            {
                Assert.Fail();
            }
            [Test]
            public void ThenCallsFetchInProgressUseCase()
            {
                Assert.Fail();
            }
        
            [Test]
            public void AddSaveNewGameDataTestsToStartup()
            {
                Assert.Fail();
            }
        }

        public class GivenInvalidInput
        {
            
        }
   
    }
}