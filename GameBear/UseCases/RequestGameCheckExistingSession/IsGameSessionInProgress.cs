using DealerBear.Adaptor.Interface;
using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.Messages;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using Messages;

namespace GameBear.UseCases.RequestGameCheckExistingSession
{
    public class IsGameSessionInProgress : IIsGameSessionInProgress
    {
        public void Execute(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse, IGameDataGateway gameDataGateway,
            IPublishMessageAdaptor publishEndpoint)
        {
            if (InvalidSessionID(requestGameIsSessionIDInUse))
            {
                throw new InvalidSessionIDException();
            }
            
            if (InvalidMessageID(requestGameIsSessionIDInUse))
            {
                throw new InvalidMessageIDException();
            }
       
            if (gameDataGateway.IsExistingSession(requestGameIsSessionIDInUse.SessionID))
            {
                publishEndpoint.Publish(new RequestGameSessionFound
                {
                    SessionID = requestGameIsSessionIDInUse.SessionID,
                    MessageID = requestGameIsSessionIDInUse.MessageID
                });
            }
            else
            {
                publishEndpoint.Publish(new RequestGameSessionNotFound
                {
                    SessionID = requestGameIsSessionIDInUse.SessionID,
                    MessageID = requestGameIsSessionIDInUse.MessageID
                });
            }
        }

        private static bool InvalidSessionID(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse)
        {
            return requestGameIsSessionIDInUse.SessionID == null ||
                   string.IsNullOrEmpty(requestGameIsSessionIDInUse.SessionID) ||
                   string.IsNullOrWhiteSpace(requestGameIsSessionIDInUse.SessionID);
        }

        private static bool InvalidMessageID(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse)
        {
            return requestGameIsSessionIDInUse.MessageID == null ||
                   string.IsNullOrEmpty(requestGameIsSessionIDInUse.MessageID) ||
                   string.IsNullOrWhiteSpace(requestGameIsSessionIDInUse.MessageID);
        }
    }
}