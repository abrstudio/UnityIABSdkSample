using System;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class QueryInventoryCallbackData : ICallbackData
    {
        public readonly Action<Inventory> SuccessAction;
        public readonly Action<int, string> FailedAction;

        public QueryInventoryCallbackData(Action<Inventory> successAction, Action<int, string> failedAction)
        {
            SuccessAction = successAction;
            FailedAction = failedAction;
        }
    }
}