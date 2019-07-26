using DealerBear.Adaptor.Interface;
using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.Messages;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;

namespace GameBear.UseCases.RequestGameCheckExistingSession
{
    public class IsGameSessionInProgress : IIsGameSessionInProgress
    {
        private readonly IGameDataGateway _gameDataGateway;
        private readonly IPublishMessageAdaptor _publishEndpoint;

        public IsGameSessionInProgress(
            IGameDataGateway gameDataGateway,
            IPublishMessageAdaptor publishEndpoint)
        {
            _gameDataGateway = gameDataGateway;
            _publishEndpoint = publishEndpoint;
        }
        public void Execute(string sessionID, string messageID )
        {
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }
            
            if (InvalidIDString(messageID))
            {
                throw new InvalidMessageIDException();
            }
       
            if (_gameDataGateway.IsExistingSession(sessionID))
            {
                _publishEndpoint.Publish(new RequestGameSessionFound
                {
                    SessionID = sessionID,
                    MessageID = messageID
                });
            }
            else
            {
                _publishEndpoint.Publish(new RequestGameSessionNotFound
                {
                    SessionID = sessionID,
                    MessageID = messageID
                });
            }
        }

        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}