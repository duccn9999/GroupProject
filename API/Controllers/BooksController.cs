using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.DTOs.Books;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("odata/Books")]
    public class BooksController : ODataController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }

        // GET api/Books/{id}
        [HttpGet("{id}")]
        public ActionResult<GetBookDTO> GetById(Guid id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                return NotFound(new { message = "Book not found." });
            return Ok(book);
        }

        // POST api/Books
        [HttpPost]
        public ActionResult Create([FromBody] CreateBookDTO model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid book data." });

            var book = _mapper.Map<Book>(model);
            _bookRepository.Create(book);
            return Created(book);
        }

        // PUT api/Books/{id}
        [HttpPut]
        public ActionResult Update([FromBody] UpdateBookDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid book data." });

            var existingBook = _bookRepository.Find(model.BookId);
            if (existingBook == null)
                return NotFound(new { message = "Book not found." });

            _mapper.Map(model, existingBook);
            _bookRepository.Update(existingBook);

            return NoContent();
        }

        // DELETE api/Books/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
                return NotFound(new { message = "Book not found." });

            _bookRepository.Delete(id);
            return NoContent();
        }
    }

}
