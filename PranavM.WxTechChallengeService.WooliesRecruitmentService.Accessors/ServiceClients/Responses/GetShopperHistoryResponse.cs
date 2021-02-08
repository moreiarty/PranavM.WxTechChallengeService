using System.Collections.Generic;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses
{
    public class GetShopperHistoryResponse
    {
        public string CustomerId { get; set; }

        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}