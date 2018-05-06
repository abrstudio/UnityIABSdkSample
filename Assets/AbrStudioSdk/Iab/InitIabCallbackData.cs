using System;
using AbrStudioSdk.Core;

namespace AbrStudioSdk.Iab
{
    public class InitIabCallbackData : ICallbackData
    {
        public readonly Action SuccessAction;
        public readonly Action<int, string> FailedAction;

        public InitIabCallbackData(Action successAction, Action<int, string> failedAction)
        {
            SuccessAction = successAction;
            FailedAction = failedAction;
        }
    }
}