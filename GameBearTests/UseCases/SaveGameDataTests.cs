using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class SaveGameDataTests
    {
        public class GivenExistingSessionID
        {
            public class WhenMessageIDIsInHistory
            {
                [Test]
                public void ThenWillGetMessageIDHistoryFromGateway()
                {
                    Assert.Fail();
                }
                [Test]
                public void ThenDataIsNotSaved()
                {
                    Assert.Fail();
                }
            }

            public class WhenMessageIDIsNotInHistory
            {
                [Test]
                public void ThenWillGetMessageIDHistoryFromGateway()
                {
                    Assert.Fail();
                }

                [Test]
                public void ThenWillStoreMessageIDInMessageIDHistory()
                {
                    Assert.Fail();
                }

                [Test]
                public void ThenDataIsSaved()
                {
                    Assert.Fail();
                }
            }
        }
        public class GivenNoExistingSessionID
        {
            [Test]
            public void ThenWillStoreMessageIDInMessageIDHistory()
            {
                Assert.Fail();
            }
            [Test]
            public void ThenDataIsSaved()
            {
                Assert.Fail();
            }
        }
        [Test]
        public void AddSaveGameUseCaseToStartup()
        {
            Assert.Fail();
        }
    }
}