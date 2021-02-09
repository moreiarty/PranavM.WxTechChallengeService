using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PranavM.WxTechChallengeService.WebApi.Utilities.Helpers;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Requests;

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

        public async Task<ActionResult<float>> CalculateTrolleyTotalAsync(TrolleyTotalRequest body, string x_Tracking_Id = null)
        {
            var clientRequest = new CalculateTrolleyRequest 
            {
                Products = body.Products.Select(p => new CalculateTrolleyRequestProduct
                    {
                        Name = p.Name,
                        Price = (decimal)p.Price
                    }).ToList(),
                Quantities = body.Quantities.Select(q => new CalculateTrolleyRequestQuantity
                    {
                        Name = q.Name,
                        Quantity = q.Quantity
                    }).ToList(),
                Specials = body.Specials.Select(s => new CalculateTrolleyRequestSpecial
                    {
                        Quantities = s.Quantities.Select(q => new CalculateTrolleyRequestQuantity
                            {
                                Name = q.Name,
                                Quantity = q.Quantity
                            }).ToList(),
                        Total = (decimal)s.Total
                    }).ToList()
            };

            var lowestTrolleyTotal = await _wooliesRecruitmentTrolleyClient.CalculateTrolleyTotal(clientRequest);
            return ((float)lowestTrolleyTotal).ToJsonApiOKResponse();
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
            var products = new List<Product>(); 
            
            if (sortOption == null || sortOption != SortOption.Recommended)
            {
                products = (await _wooliesRecruitmentProductsClient.GetProductsAsync())
                    .Select(product => new Product
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = (int)product.Quantity
                    })
                    .ToList();
            }
            else
            {
                var shopperHistories = await _wooliesRecruitmentShopperHistoryClient.GetShopperHistory();
                foreach (var shopperHistory in shopperHistories)
                {   
                    foreach(var boughtProduct in shopperHistory.Products)
                    {
                        var matchingProduct = products
                            .Where(p => p.Name == boughtProduct.Name)
                            .FirstOrDefault();
                        if (matchingProduct == null)
                        {
                            products.Add(new Product
                                {
                                    Name = boughtProduct.Name,
                                    Price = boughtProduct.Price,
                                    Quantity = (int)boughtProduct.Quantity
                                });
                        }
                        else
                        {
                            matchingProduct.Quantity += (int)boughtProduct.Quantity;
                        }
                    }
                }
            }

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
                        products = products
                            .OrderByDescending(product => product.Quantity)
                            .ToList();
                        break;
                    default: break;
                }
            }

            return products.ToJsonApiOKResponse();
        }
    }
}