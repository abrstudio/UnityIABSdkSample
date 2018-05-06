using SimpleJSON;

namespace AbrStudioSdk.Iab.Data
{
    public class SkuDetail
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
        public long PriceNum { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }

        public SkuDetail()
        {
        }
    
        public SkuDetail(JSONNode skuDetailInfo)
        {
            Title = skuDetailInfo["title"];
            Description = skuDetailInfo["description"];
            ProductId = skuDetailInfo["productId"];
            PriceNum = skuDetailInfo["priceNum"].AsInt;
            Price = skuDetailInfo["price"];
            Type = skuDetailInfo["type"];
        }
    }
}