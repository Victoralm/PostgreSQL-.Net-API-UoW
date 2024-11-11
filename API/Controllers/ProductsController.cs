using API2.Entities;
using API2.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var items = await _unitOfWork.Products.GetProductsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _unitOfWork.Products.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                await _unitOfWork.Products.Add(product);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("Get", new { product.Id }, product);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _unitOfWork.Products.Upsert(product);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Products.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
