using Newtonsoft.Json.Linq;
using SQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductBookinUsingEntity.Controllers
{
    public class CarController : ApiController, IProduct
    {
        ProductBookingWebAPIEntities1 entity = new ProductBookingWebAPIEntities1();
        CarProduct carProduct = new CarProduct();

        [HttpGet]
        public IEnumerable<CarProduct> GetValue()
        {
            return entity.CarProducts.ToList();
        } 

        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            entity.CarProducts.Add(jsonFormatInput.ToObject<CarProduct>());
            entity.SaveChanges();
        }

        [HttpPut]
        [Route("api/Car/Book/{id}")]
        public void Book([FromUri] int id)
        {
           
            carProduct = entity.CarProducts.Find(id);
            carProduct.isBooked = true;
            entity.SaveChanges();
        } 

        [HttpPut]
        [Route("api/Car/Save/{id}")]
        public void Save([FromUri] int id)
        {
           
            carProduct = entity.CarProducts.Find(id);
            carProduct.isSaved = true;
            entity.SaveChanges();
        }

        [HttpGet]
        [Route("api/Car/GetSavedItems")]
        public IEnumerable<CarProduct> GetSavedItems()
        {
            IEnumerable<CarProduct> enumerable = GetValue();
            List<CarProduct> carItems = enumerable.ToList();
            List<CarProduct> carSavedItems = new List<CarProduct>();

            for (int i = 0; i < carItems.Count; i++)
            {
                carProduct = carItems[0];
                if (carProduct.isSaved == true)
                {
                    carSavedItems.Add(carProduct);
                }
            }
            return carSavedItems;
        }


        [HttpGet]
        [Route("api/Car/GetBookedItems")]
        public IEnumerable<CarProduct> GetBookedItems()
        {
            IEnumerable<CarProduct> enumerable = GetValue();
            List<CarProduct> carItems = enumerable.ToList();
            List<CarProduct> carBookedItems = new List<CarProduct>();

            for (int i = 0; i < carItems.Count; i++)
            {
                carProduct = carItems[0];
                if (carProduct.isBooked == true)
                {
                    carBookedItems.Add(carProduct);
                }
            }
            return carBookedItems;
        }
    }
}
