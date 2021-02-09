using System.Collections.Generic;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Requests
{
    public class CalculateTrolleyRequest
    {
        public List<CalculateTrolleyRequestProduct> Products { get; set; }

        public List<CalculateTrolleyRequestSpecial> Specials { get; set; }

        public List<CalculateTrolleyRequestQuantity> Quantities { get; set; }
    }

    public class CalculateTrolleyRequestQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class CalculateTrolleyRequestSpecial
    {
        public List<CalculateTrolleyRequestQuantity> Quantities { get; set; }

        public decimal Total { get; set; }
    }

    public class CalculateTrolleyRequestProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set ;}
    }
}