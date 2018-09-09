using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductBookinUsingEntity.Controllers
{
    interface IProduct
    {
        void Post([FromBody]JObject jsonFormatInput);

        void Book([FromUri] int id);

        void Save([FromUri] int id);
    }
}
