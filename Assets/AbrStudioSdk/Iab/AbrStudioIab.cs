using System;
using System.Collections.Generic;
using System.Linq;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab.Data;
using UnityEngine;

namespace AbrStudioSdk.Iab
{
    public class AbrStudioIab
    {
        private static GameObject _iabManager;
        private static AbrStudioMessageHandler _messageHandler;
        private static AndroidJavaObject _abrStudioIab;

        private const string IabManagerName = "AbrStudioIabManager";
        
        private const string InitAction = "init";
        private const string QueryInventoryAction = "queryInventory";
        private const string PurchaseAction = "purchase";
        private const string ConsumeAction = "consume";
        private const string MultiConsumeAction = "multiConsume";

        private static readonly AndroidJavaClass AbrStudioIabClass = new AndroidJavaClass ("co.abrtech.game.unity.iab.AbrStudioIabUnity");


        private static void AbrStudioIabInstance()
        {
            if (_abrStudioIab == null)
            {
                _abrStudioIab = AbrStudioIabClass.CallStatic<AndroidJavaObject>("instance");   
            }
        }
        
        private static void Initialize(AbrStudioMessageHandler messageHandler)
        {
            AbrStudioIabInstance();

            _messageHandler = messageHandler;
            _messageHandler.AddHandler(InitAction, new InitIabMessageHandler());
            _messageHandler.AddHandler(QueryInventoryAction, new QueryInventoryMessageHandler());
            _messageHandler.AddHandler(PurchaseAction, new PurchaseMessageHandler());
            _messageHandler.AddHandler(ConsumeAction, new ConsumeMessageHandler());
            _messageHandler.AddHandler(MultiConsumeAction, new MultiConsumeMessageHandler());
        }
        
        private static void InitIabManager()
        {
            if (_iabManager != null)
            {
                return;
            }
		
            _iabManager = new GameObject(IabManagerName);
            UnityEngine.Object.DontDestroyOnLoad(_iabManager);
            var iabMessageHandler = _iabManager.AddComponent<AbrStudioMessageHandler>();
		
            Initialize(iabMessageHandler);
            
            Debug.Log("IabManager initialized successfully.");
        }
    
        public static void Start(string publicKey, Action successAction, Action<int, string> failedAction)
        {
            Debug.Log("Starting AbrStudioIab.");
            if (_iabManager == null)
            {
                InitIabManager();
            }
            
            int reqId = _messageHandler.NewItem(new InitIabCallbackData(successAction, failedAction));
            _abrStudioIab.Call(InitAction, reqId, publicKey);
            
            Debug.Log("AbrStudioIab started successfully.");
        }

        public static void QueryInventory(string[] skus, Action<Inventory> successAction, Action<int, string> failedAction)
        {
            if (_iabManager == null)
            {
                InitIabManager();
            }
            
            if (_abrStudioIab == null)
            {
                failedAction(-1000, "You have to call AbrStudioIab#Start function first.");
                return;
            }
            
            int reqId = _messageHandler.NewItem(new QueryInventoryCallbackData(successAction, failedAction));
            _abrStudioIab.Call(QueryInventoryAction, reqId, skus);
        }

        public static void Purchase(string sku, string developerPayload, Action<Purchase> successAction, Action<int, string> failedAction)
        {
            if (_iabManager == null)
            {
                InitIabManager();
            }
            
            if (_abrStudioIab == null)
            {
                failedAction(-1000, "You have to call AbrStudioIab#Start function first.");
                return;
            }
            
            int reqId = _messageHandler.NewItem(new PurchaseCallbackData(successAction, failedAction));
            _abrStudioIab.Call(PurchaseAction, reqId, sku, developerPayload);
        }

        public static void Consume(Purchase purchase, Action<Purchase> successAction, Action<int, string> failedAction)
        {
            if (_iabManager == null)
            {
                InitIabManager();
            }
            
            if (_abrStudioIab == null)
            {
                failedAction(-1000, "You have to call AbrStudioIab#Start function first.");
                return;
            }
            
            string purchaseJson = purchase.AsJson();
            int reqId = _messageHandler.NewItem(new ConsumeCallbackData(successAction, failedAction));
            _abrStudioIab.Call(ConsumeAction, reqId, purchaseJson);
        }
        
        public static void MultiConsume(IEnumerable<Purchase> purchaseList, Action<List<Purchase>, List<CodeMessagePair>> resultAction)
        {
            if (_iabManager == null)
            {
                InitIabManager();
            }
            
            if (_abrStudioIab == null)
            {
                return;
            }

            string[] toConsume = purchaseList.Select(purchase => purchase.AsJson()).ToArray();
            int reqId = _messageHandler.NewItem(new MultiConsumeCallbackData(resultAction));
            _abrStudioIab.Call(MultiConsumeAction, reqId, toConsume);
        }
    }
}