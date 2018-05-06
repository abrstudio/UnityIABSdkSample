using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class QueryInventoryMessageHandler : IMessageHandler
    {
        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var callbackData = data as QueryInventoryCallbackData;

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
                    callbackData.SuccessAction(new Inventory(message.data[Constants.InventoryKey].AsObject));
                    return;
                case Constants.FailedEvent:
                    callbackData.FailedAction(message.data[Constants.CodeKey].AsInt, message.data[Constants.MessageKey].Value);
                    return;
            }
        }
    }
}