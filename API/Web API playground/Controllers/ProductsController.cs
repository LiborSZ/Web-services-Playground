using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API_playground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private static readonly List<string> products = new List<string>
        {
            "Mobiles", "Notebooks", "Desktop PC's"
        };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0 || id >= products.Count)
            {
                return NotFound();
            }
            return products[id];
        }

        [HttpPost]
        public void Post([FromBody] string product)
        {
            products.Add(product);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            if(id >= 0 && id < products.Count)
            products.RemoveAt(id);
        }

    }
}
