using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

namespace AbrStudioSdk.Iab.Data
{
    public class Inventory
    {
        public Dictionary<string, SkuDetail> SkuMap { get; set; }
        public Dictionary<string, Purchase> PurchaseMap { get; set; }

        public Inventory()
        {
        }

        public Inventory(JSONNode inventoryInfo)
        {
//            SkuMap = inventoryInfo["skuMap"].AsObject.Childs.ToDictionary(node => node["productId"].Value, node => new SkuDetail(node));
//            PurchaseMap = inventoryInfo["purchaseMap"].AsObject.Childs.ToDictionary(node => node["purchaseToken"].Value, node => new Purchase(node));
        
            SkuMap = new Dictionary<string, SkuDetail>();
            PurchaseMap = new Dictionary<string, Purchase>();
        
            foreach (var item in inventoryInfo["skuMap"].AsObject.Childs)
            {
                SkuMap.Add(item["productId"], new SkuDetail(item));
            }
        
            foreach (var item in inventoryInfo["purchaseMap"].AsObject.Childs)
            {
                PurchaseMap.Add(item["purchaseToken"], new Purchase(item));
            }
        }
    }
}