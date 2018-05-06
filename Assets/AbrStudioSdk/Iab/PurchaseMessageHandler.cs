using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class PurchaseMessageHandler : IMessageHandler
    {
        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var callbackData = data as PurchaseCallbackData;

            if (callbackData == null)
            {
                return;
            }

            string eventType = message.eventType;
            switch (eventType)
            {
                case null:
                    return;
                case Constants.SuccessEvent:
                    callbackData.SuccessAction(new Purchase(message.data[Constants.PurchaseKey].AsObject));
                    return;
                case Constants.FailedEvent:
                    callbackData.FailedAction(message.data[Constants.CodeKey].AsInt, message.data[Constants.MessageKey].Value);
                    return;
            }
        }
    }
}