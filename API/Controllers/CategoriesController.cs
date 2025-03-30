using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.DTOs.Categories;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    [Route("odata/Categories")]
    public class CategoriesController : ODataController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: odata/Categories
        [HttpGet]
        [EnableQuery] // Enables OData querying ($filter, $orderby, etc.)
        public ActionResult<IQueryable<Category>> GetAll()
        {
            return Ok(_categoryRepository.GetAll().AsQueryable());
        }

        // GET: odata/Categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound(new { message = "Category not found." });

            return Ok(category);
        }

        // POST: odata/Categories
        [HttpPost]
        public ActionResult Create([FromBody] CreateCategoryDTO model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid category data." });
            var category = _mapper.Map<Category>(model);
            _categoryRepository.Create(category);
            return Created(category);
        }

        // PUT: odata/Categories/{id}
        [HttpPut]
        public ActionResult Update([FromBody] UpdateCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid category data." });

            var existingCategory = _categoryRepository.GetById(model.CategoryId);
            if (existingCategory == null)
                return NotFound(new { message = "Category not found." });

            _mapper.Map(model, existingCategory);
            _categoryRepository.Update(existingCategory);

            return NoContent();
        }

        // DELETE: odata/Categories/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var existingCategory = _categoryRepository.GetById(id);
            if (existingCategory == null)
                return NotFound(new { message = "Category not found." });

            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}