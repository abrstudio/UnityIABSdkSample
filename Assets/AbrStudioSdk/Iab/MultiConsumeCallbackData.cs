using System;
using System.Collections.Generic;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;

namespace AbrStudioSdk.Iab
{
    public class MultiConsumeCallbackData : ICallbackData
    {
        public readonly Action<List<Purchase>, List<CodeMessagePair>> ResultAction;
    
        public MultiConsumeCallbackData(Action<List<Purchase>, List<CodeMessagePair>> resultAction)
        {
            ResultAction = resultAction;
        }
    }
}