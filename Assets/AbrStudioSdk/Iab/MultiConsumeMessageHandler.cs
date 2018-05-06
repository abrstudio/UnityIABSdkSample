using System.Collections.Generic;
using System.Linq;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;
using SimpleJSON;

namespace AbrStudioSdk.Iab
{
    public class MultiConsumeMessageHandler : IMessageHandler
    {
        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var callbackData = data as MultiConsumeCallbackData;

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
                    callbackData.ResultAction(getPurchaseList(message.data[Constants.PurchaseListKey].AsArray), getResultList(message.data[Constants.ResultListKey].AsArray));
                    return;
            }
        }

        private List<Purchase> getPurchaseList(JSONArray jsonArray)
        {
            if (jsonArray == null || jsonArray.Childs == null)
            {
                return null;
            }
            return jsonArray.Childs.Select(json => new Purchase(json)).ToList();
        }
        
        private List<CodeMessagePair> getResultList(JSONArray jsonArray)
        {
            if (jsonArray == null || jsonArray.Childs == null)
            {
                return null;
            }
            return jsonArray.Childs.Select(json => new CodeMessagePair(json)).ToList();
        }
    }
}