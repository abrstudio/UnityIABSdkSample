using System;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class PurchaseCallbackData : ICallbackData
    {
        public readonly Action<Purchase> SuccessAction;
        public readonly Action<int, string> FailedAction;
    
        public PurchaseCallbackData(Action<Purchase> successAction, Action<int, string> failedAction)
        {
            SuccessAction = successAction;
            FailedAction = failedAction;
        }
    }
}