using SimpleJSON;

namespace AbrStudioSdk.Iab.Data
{
    public class Purchase
    {
        private const string ItemTypeKey = "itemType";
        private const string OrderIdKey = "orderId";
        private const string PackageNameKey = "packageName";
        private const string ProductIdKey = "productId";
        private const string PurchaseTimeKey = "purchaseTime";
        private const string PurchaseStateKey = "purchaseState";
        private const string DeveloperPayloadKey = "developerPayload";
        private const string PurchaseTokenKey = "purchaseToken";
    
        public string ItemType{ get; set; }  // ITEM_TYPE_INAPP or ITEM_TYPE_SUBS
        public string OrderId{ get; set; }
        public string PackageName{ get; set; }
        public string ProductId{ get; set; }
        public long Time{ get; set; }
        public int State{ get; set; }
        public string DeveloperPayload{ get; set; }
        public string Token{ get; private set; }

        public Purchase()
        {
        }
    
        public Purchase(JSONNode purchaseInfo)
        {
            ItemType = purchaseInfo[ItemTypeKey];
            OrderId = purchaseInfo[OrderIdKey];
            PackageName = purchaseInfo[PackageNameKey];
            ProductId = purchaseInfo[ProductIdKey];
            Time = purchaseInfo[PurchaseTimeKey].AsInt;
            State = purchaseInfo[PurchaseStateKey].AsInt;
            DeveloperPayload = purchaseInfo[DeveloperPayloadKey];
            Token = purchaseInfo[PurchaseTokenKey];
        }

        public string AsJson()
        {
            JSONClass json = new JSONClass();
            json[ItemTypeKey] = ItemType;
            json[OrderIdKey] = OrderId;
            json[PackageNameKey] = PackageName;
            json[ProductIdKey] = ProductId;
            json[PurchaseTimeKey] = new JSONData(Time);
            json[PurchaseStateKey] = new JSONData(State);
            json[DeveloperPayloadKey] = DeveloperPayload;
            json[PurchaseTokenKey] = Token;

            return json.ToString();
        }
    
    }
}