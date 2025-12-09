using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Repositories;
using System.Security.Claims;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _unitOfWork.ApplicationUsers.GetUserWithDetailsAsync(userId);
            if (user == null) return NotFound("User not found");

            var dto = new UserProfileDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                BorrowHistory = user.UserBorrows.Select(b => new BorrowDetailsDto
                {
                    BookId = b.BookId,
                    BookTitle = b.Book.Title,
                    BorrowDate = b.BorrowDate,
                    ReturnDate = b.ReturnDate,
                    IsReturned=b.IsReturned
                }).ToList(),
                FavoriteBooks = user.FavoriteBooks.Select(f => new FavoriteDto
                {
                    BookId = f.BookId,
                    Title = f.Book.Title,
                    Author = f.Book.Author
                }).ToList()
            };

            return Ok(dto);
        }

    }
}
