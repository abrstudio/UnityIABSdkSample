using System;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class ConsumeCallbackData : ICallbackData
    {
        public readonly Action<Purchase> SuccessAction;
        public readonly Action<int, string> FailedAction;

        public ConsumeCallbackData(Action<Purchase> successAction, Action<int, string> failedAction)
        {
            SuccessAction = successAction;
            FailedAction = failedAction;
        }
    }
}