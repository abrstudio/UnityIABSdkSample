using AbrStudioSdk.Core;

namespace AbrStudioSdk.Iab
{
    public class InitIabMessageHandler : IMessageHandler
    {
        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var initCallbackData = data as InitIabCallbackData;

            if (initCallbackData == null)
            {
                return;
            }

            string eventType = message.eventType;
            switch (eventType)
            {
                case null:
                    return;
                case Constants.SuccessEvent:
                    initCallbackData.SuccessAction();
                    return;
                case Constants.FailedEvent:
                    initCallbackData.FailedAction(message.data[Constants.CodeKey].AsInt, message.data[Constants.MessageKey].Value);
                    return;
            }
        }
    }
}