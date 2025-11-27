using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Models;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Repositories;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Consumes("multipart/form-data")]

    public class BookController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapp;
        private readonly IWebHostEnvironment _env;

        public BookController(IunitOfWork unitOfWork, IMapper mapp, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapp = mapp;
            _env = env;
        }

        // ------------------- GET ALL -------------------
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync());
        }

        // ------------------- GET BY ID ------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // ------------------- CREATE BOOK -------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookDto bookDto)
        {
            var book = _mapp.Map<Book>(bookDto);

            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == bookDto.categoryId);
            if (category == null)
                return BadRequest("Invalid Category Id");

            book.CategoryName = category.Name;

            // Handle image upload
            if (bookDto.Image != null && bookDto.Image.Length > 0)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(bookDto.Image.FileName)}";
                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await bookDto.Image.CopyToAsync(stream);

                book.ImageUrl = $"/images/{fileName}";
            }

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.complete();

            return Ok(book);
        }

        // ------------------- UPDATE BOOK -------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BookDto bookDto)
        {
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == id);
            if (book == null)
                return NotFound();

            _mapp.Map(bookDto, book);

            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == bookDto.categoryId);
            if (category == null)
                return BadRequest("Invalid Category Id");

            book.CategoryName = category.Name;

            // Replace image if new one exists
            if (bookDto.Image != null)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(bookDto.Image.FileName)}";
                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await bookDto.Image.CopyToAsync(stream);

                book.ImageUrl = $"/images/{fileName}";
            }

            _unitOfWork.Books.Update(book);
            await _unitOfWork.complete();

            return Ok(book);
        }

        // ------------------- DELETE -------------------
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
