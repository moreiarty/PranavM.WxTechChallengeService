using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PranavM.WxTechChallengeService.WebApi.Utilities.Helpers;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;

namespace PranavM.WxTechChallengeService.WebApi.Controllers.Implementation
{
    public class WxTechChallengeControllerImplementation : IWxTechChallengeController
    {
        private const string USER_NAME = "Pranav More";

        private const string USER_TOKEN = "9e1b0aee-31bb-4422-b068-ec01afae5f90";

        private readonly IWooliesRecruitmentProductsClient _wooliesRecruitmentProductsClient;
        private readonly IWooliesRecruitmentShopperHistoryClient _wooliesRecruitmentShopperHistoryClient;
        private readonly IWooliesRecruitmentTrolleyClient _wooliesRecruitmentTrolleyClient;

        public WxTechChallengeControllerImplementation(
            IWooliesRecruitmentProductsClient wooliesRecruitmentProductsClient,
            IWooliesRecruitmentShopperHistoryClient wooliesRecruitmentShopperHistoryClient,
            IWooliesRecruitmentTrolleyClient wooliesRecruitmentTrolleyClient)
        {
            _wooliesRecruitmentProductsClient = wooliesRecruitmentProductsClient;
            _wooliesRecruitmentShopperHistoryClient = wooliesRecruitmentShopperHistoryClient;
            _wooliesRecruitmentTrolleyClient = wooliesRecruitmentTrolleyClient;
        }

        public async Task<ActionResult<GetUserResponse>> GetUserAsync(string x_Tracking_Id = null)
        {
            var response = new GetUserResponse
            {
                Name = USER_NAME,
                Token = USER_TOKEN,
            };

            return response.ToJsonApiOKResponse();
        }

        public async Task<ActionResult<ICollection<Product>>> SortProductsAsync(string x_Tracking_Id = null, SortOption? sortOption = null)
        {
            var products = await _wooliesRecruitmentProductsClient.GetProductsAsync();
            if (sortOption != null)
            {
                switch (sortOption)
                {
                    case SortOption.Low:
                        products = products.OrderBy(p => p.Price).ToList();
                        break;
                    case SortOption.High:
                        products = products.OrderByDescending(p => p.Price).ToList();
                        break;
                    case SortOption.Ascending:
                        products = products.OrderBy(p => p.Name).ToList();
                        break;
                    case SortOption.Descending:
                        products = products.OrderByDescending(p => p.Name).ToList();
                        break;
                    case SortOption.Recommended:
                        var shopperHistory = await _wooliesRecruitmentShopperHistoryClient.GetShopperHistory();
                        var distinctProductsWithSummedQuantity = new List<Product>();
                        foreach (var customerShopperHistory in shopperHistory)
                        {   
                            foreach(var product in customerShopperHistory.Products)
                            {
                                var matchingProduct = distinctProductsWithSummedQuantity
                                    .Where(p => p.Name == product.Name)
                                    .FirstOrDefault();
                                if (matchingProduct == null)
                                {
                                    distinctProductsWithSummedQuantity.Add(new Product
                                        {
                                            Name = product.Name,
                                            Price = product.Price,
                                            Quantity = (int)product.Quantity
                                        });
                                }
                                else
                                {
                                    matchingProduct.Quantity += (int)product.Quantity;
                                }
                            }
                        }
                        return distinctProductsWithSummedQuantity
                            .OrderByDescending(product => product.Quantity)
                            .ToList()
                            .ToJsonApiOKResponse();
                    default: break;
                }
            }
            return products
                .Select(product => new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = (int)product.Quantity
                })
                .ToList()
                .ToJsonApiOKResponse();
        }
    }
}