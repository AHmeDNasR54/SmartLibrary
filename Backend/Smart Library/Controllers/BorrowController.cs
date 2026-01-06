using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Models;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Repositories;
using System.Security.Claims;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowController(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all borrows (with related Book & User)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var borrows = await _unitOfWork.Borrows
                .GetAllAsync(includeProperties: "Book,User");

            var borrowDtos = _mapper.Map<IEnumerable<BorrowDto>>(borrows);
            return Ok(borrowDtos);
        }

        // Get specific borrow by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var borrow = await _unitOfWork.Borrows
                .GetAsync(b => b.Id == id, includeProperties: "Book,User");

            if (borrow == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Borrow record not found"
                });

            return Ok(_mapper.Map<BorrowDto>(borrow));
        }

        // Add new borrow
        [HttpPost]
        public async Task<IActionResult> BorrowBook([FromBody] CreateBorrowDto borrowDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Unauthorized"
                });
            //// Check user
            //var user = await _unitOfWork.ApplicationUsers.GetAsync(u => u.Id == borrowDto.UserId);
            //if (user == null)
            //    return BadRequest(new ApiResponseDto<object>
            //    {
            //        Success = false,
            //        Message = "Invalid User ID"
            //    });

            // Check book
            var book = await _unitOfWork.Books.GetAsync(b => b.Id == borrowDto.BookId);
            if (book == null)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Invalid Book ID"
                });

            // Check available copies
            if (book.AvailableCopies <= 0)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "No available copies for this book"
                });

            // Check existing borrow
            var existingBorrow = await _unitOfWork.Borrows.GetAsync(
                b => b.UserId == userId&&
                     b.BookId == borrowDto.BookId &&
                     !b.IsReturned
            );

            if (existingBorrow != null)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "You already borrowed this book and haven't returned it yet."
                });

            // Create borrow
            var borrow = _mapper.Map<Borrow>(borrowDto);
            borrow.UserId = userId;
            //_unitOfWork.Borrows.AddBorrow(borrow);
            //إنشاء عملية الاستعارة الجديدة
            borrow.BorrowDate = DateTime.Now;
            borrow.ReturnDate = borrow.BorrowDate.AddDays(3);
            borrow.IsReturned = false;
            //_unitOfWork.Borrows.AddBorrow(borrow);
            // Update available copies
            book.AvailableCopies--;

            await _unitOfWork.Borrows.AddAsync(borrow);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.complete();

            return Ok(_mapper.Map<BorrowDto>(borrow));
        }

        // Return book
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var borrow = await _unitOfWork.Borrows
                .GetAsync(b => b.Id == id, includeProperties: "Book");

            if (borrow == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Borrow record not found"
                });

            if (borrow.IsReturned)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "This book has already been returned"
                });

            borrow.IsReturned = true;
            borrow.ReturnDate = DateTime.Now;

            if (borrow.Book != null)
            {
                borrow.Book.AvailableCopies++;
                _unitOfWork.Books.Update(borrow.Book);
            }

            _unitOfWork.Borrows.Update(borrow);
            await _unitOfWork.complete();

            return Ok(new ApiResponseDto<object>
            {
                Success = true,
                Message = "Book returned successfully"
            });
        }

        // Delete borrow
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _unitOfWork.Borrows.GetAsync(b => b.Id == id);
            if (borrow == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Borrow record not found"
                });

            _unitOfWork.Borrows.Remove(borrow);
            await _unitOfWork.complete();

            return Ok(new ApiResponseDto<object>
            {
                Success = true,
                Message = "Borrow deleted successfully"
            });
        }
    }
}


//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SmartLibrary.Models;
//using SmartLibrary.Models.DTOs;
//using SmartLibrary.Models.Repositories;

//namespace Smart_Library.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class BorrowController : ControllerBase
//    {
//        private readonly IunitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public BorrowController(IunitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        // Get all borrows (with related Book & User)
//        [HttpGet]
//        [Authorize(Roles = "Admin")]

//        public async Task<IActionResult> GetAll()
//        {
//            var borrows = await _unitOfWork.Borrows
//                .GetAllAsync(includeProperties: "Book,User");

//            var borrowDtos = _mapper.Map<IEnumerable<BorrowDto>>(borrows);
//            return Ok(borrowDtos);
//        }

//        //  Get specific borrow by Id (with related Book & User)
//        [HttpGet("{id}")]
//        //[Authorize(Roles = "Admin")]

//        public async Task<IActionResult> GetById(int id)
//        {
//            var borrow = await _unitOfWork.Borrows
//                .GetAsync(b => b.Id == id, includeProperties: "Book,User");

//            if (borrow == null)
//                return NotFound();

//            return Ok(_mapper.Map<BorrowDto>(borrow));
//        }

//        //  Add new borrow
//        [HttpPost]
//        public async Task<IActionResult> BorrowBook([FromBody] CreateBorrowDto borrowDto)
//        {
//            // تحقق من وجود المستخدم
//            var user = await _unitOfWork.ApplicationUsers.GetAsync(u => u.Id == borrowDto.UserId);
//            if (user == null)
//                return BadRequest("Invalid User ID");

//            // تحقق من وجود الكتاب
//            var book = await _unitOfWork.Books.GetAsync(b => b.Id == borrowDto.BookId);
//            if (book == null)
//                return BadRequest("Invalid Book ID");

//            //  تحقق من النسخ المتاحة
//            if (book.AvailableCopies <= 0)
//                return BadRequest("No available copies for this book");

//            //  تحقق إن المستخدم مش مستعير نفس الكتاب بالفعل ومش مرجّع
//            var existingBorrow = await _unitOfWork.Borrows.GetAsync(
//                b => b.UserId == borrowDto.UserId && b.BookId == borrowDto.BookId && !b.IsReturned
//            );

//            if (existingBorrow != null)
//                return BadRequest("You already borrowed this book and haven't returned it yet.");

//            //  إنشاء عملية الاستعارة الجديدة
//            var borrow = _mapper.Map<Borrow>(borrowDto);
//            //borrow.BorrowDate = DateTime.Now;
//            //borrow.ReturnDate = borrow.BorrowDate.AddDays(3);
//            //borrow.IsReturned = false;
//            _unitOfWork.Borrows.AddBorrow(borrow);

//            //  تحديث عدد النسخ المتاحة
//            book.AvailableCopies--;

//            // حفظ التغييرات
//            await _unitOfWork.Borrows.AddAsync(borrow);
//            _unitOfWork.Books.Update(book);
//            await _unitOfWork.complete();

//            return Ok(_mapper.Map<BorrowDto>(borrow));
//        }


//        [HttpPut("return/{id}")]
//        public async Task<IActionResult> ReturnBook(int id)
//        {
//            var borrow = await _unitOfWork.Borrows.GetAsync(b => b.Id == id, includeProperties: "Book");
//            if (borrow == null)
//                return NotFound("Borrow record not found");

//            if (borrow.IsReturned)
//                return BadRequest("This book has already been returned");

//            // عملية الإرجاع
//            borrow.IsReturned = true;
//            borrow.ReturnDate = DateTime.Now;

//            //  رجّع النسخة المتاحة
//            if (borrow.Book != null)
//            {
//                borrow.Book.AvailableCopies++;
//                _unitOfWork.Books.Update(borrow.Book);
//            }

//            _unitOfWork.Borrows.Update(borrow);
//            await _unitOfWork.complete();

//            return Ok("Book returned successfully");
//        }


//        // Delete borrow record
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var borrow = await _unitOfWork.Borrows.GetAsync(b => b.Id == id);
//            if (borrow == null)
//                return NotFound();

//            _unitOfWork.Borrows.Remove(borrow);
//            await _unitOfWork.complete();

//            return Ok("Borrow deleted successfully");
//        }
//    }
//}
