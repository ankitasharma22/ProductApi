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
    public class ActivityController :  ApiController, IProduct
    {
        ProductBookingWebAPIEntities1 entity = new ProductBookingWebAPIEntities1();
        ActivityProduct activityProduct = new ActivityProduct();

        [HttpGet]
        public IEnumerable<ActivityProduct> GetValue()
        {
            return entity.ActivityProducts.ToList();
        } 

        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            entity.ActivityProducts.Add(jsonFormatInput.ToObject<ActivityProduct>());
            entity.SaveChanges();
        }

        [HttpPut]
        [Route("api/Activity/Book/{id}")]
        public void Book([FromUri] int id)
        {
            
            activityProduct = entity.ActivityProducts.Find(id);
            activityProduct.isBooked = true;
            entity.SaveChanges();
        }


        [HttpPut]
        [Route("api/Activity/Save/{id}")]
        public void Save([FromUri] int id)
        {
            ActivityProduct activityProduct = new ActivityProduct();
            activityProduct = entity.ActivityProducts.Find(id);
            activityProduct.isSaved = true;
            entity.SaveChanges();
        }


        [HttpGet]
        [Route("api/Activity/GetSavedItems")]
        public IEnumerable<ActivityProduct> GetSavedItems()
        {
            IEnumerable<ActivityProduct> enumerable = GetValue();
            List<ActivityProduct> activityItems = enumerable.ToList();
            List<ActivityProduct> activitySavedItems = new List<ActivityProduct>();

            for (int i = 0; i < activityItems.Count; i++)
            {
                activityProduct = activityItems[0];
                if (activityProduct.isSaved == true)
                {
                    activitySavedItems.Add(activityProduct);
                }
            }
            return activitySavedItems;
        }

        [HttpGet]
        [Route("api/Activity/GetBookedItems")]
        public IEnumerable<ActivityProduct> GetBookedItems()
        {
            IEnumerable<ActivityProduct> enumerable = GetValue();
            List<ActivityProduct> activityItems = enumerable.ToList();
            List<ActivityProduct> activityBookedItems = new List<ActivityProduct>();

            for (int i = 0; i < activityItems.Count; i++)
            {
                activityProduct = activityItems[0];
                if (activityProduct.isBooked == true)
                {
                    activityBookedItems.Add(activityProduct);
                }
            }
            return activityBookedItems;
        }
    }
}
