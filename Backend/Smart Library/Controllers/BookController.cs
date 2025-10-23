using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Models;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Repositories;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapp;
        public BookController(IunitOfWork unitOfWork, IMapper mapp)
        {
            _unitOfWork = unitOfWork;
            _mapp = mapp;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDto bookDto)
        {
            var book = _mapp.Map<Book>(bookDto);
            var Category = await _unitOfWork.Categories.GetAsync(c => c.Id == book.categoryId);
            if (Category == null)
                return BadRequest("Invalid Category Id");
            book.CategoryName = Category.Name;
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.complete();
            return Ok(book);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto bookDto)
        {
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == id);
            if (book == null)
                return NotFound();


            _mapp.Map(bookDto, book);
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == bookDto.categoryId);
                if (category == null)
                    return BadRequest("Invalid Category Id");
                book.CategoryName = category.Name;
            

            _unitOfWork.Books.Update(book);
            await _unitOfWork.complete();
            return Ok(book);
        }


        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == id);
            if (book == null)
                return NotFound();
            _unitOfWork.Books.Remove(book);
            await _unitOfWork.complete();
            return Ok();
        }
    }
}
