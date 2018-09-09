using Newtonsoft.Json.Linq;
using SQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ProductBookinUsingEntity.Controllers
{
    public class HotelController : ApiController, IProduct
    {
        ProductBookingWebAPIEntities1 entity = new ProductBookingWebAPIEntities1();
        HotelProduct hotelProduct = new HotelProduct();
        public IEnumerable<HotelProduct> GetValue()
        { 
                return entity.HotelProducts.ToList(); 
        }



        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            entity.HotelProducts.Add(jsonFormatInput.ToObject<HotelProduct>());
            entity.SaveChanges();
        }


        [HttpPut]
        [Route("api/Hotel/Book/{id}")]
        public void Book([FromUri] int id)
        { 
            hotelProduct = entity.HotelProducts.Find(id); 
            hotelProduct.isBooked = true;
            entity.SaveChanges(); 
        }

        [HttpPut]
        [Route("api/Hotel/Save/{id}")]
        public void Save([FromUri] int id)
        {
            
            hotelProduct = entity.HotelProducts.Find(id);
            hotelProduct.isSaved = true;
            entity.SaveChanges();
        }


        [HttpGet]
        [Route("api/Hotel/GetSavedItems")]
        public IEnumerable<HotelProduct> GetSavedItems()
        {
            IEnumerable<HotelProduct> enumerable = GetValue();
            List<HotelProduct> hotelItems = enumerable.ToList();
            List<HotelProduct> hotelSavedItems = new List<HotelProduct>();

            for (int i = 0; i < hotelItems.Count; i++)
            {
                hotelProduct = hotelItems[0];
                if (hotelProduct.isSaved == true)
                {
                    hotelSavedItems.Add(hotelProduct);
                }
            }
            return hotelSavedItems;
        }


        [HttpGet]
        [Route("api/Hotel/GetBookedItems")]
        public IEnumerable<HotelProduct> GetBookedItems()
        {
            IEnumerable<HotelProduct> enumerable = GetValue();
            List<HotelProduct> hotelItems = enumerable.ToList();
            List<HotelProduct> hotelBookedItems = new List<HotelProduct>();

            for (int i = 0; i < hotelItems.Count; i++)
            {
                hotelProduct = hotelItems[0];
                if (hotelProduct.isBooked == true)
                {
                    hotelBookedItems.Add(hotelProduct);
                }
            }
            return hotelBookedItems;
        }
    }
}
