using API2.Entities;
using API2.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(ILogger<CategoriesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var items = await _unitOfWork.Category.GetCategoriesAsync();

            if (items == null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _unitOfWork.Category.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();

                await _unitOfWork.Category.Add(category);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("Get", new { category.Id }, category);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Category category)
        {
            //if (id != category.Id)
            //    return BadRequest();

            await _unitOfWork.Category.Upsert(category);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _unitOfWork.Category.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Category.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
