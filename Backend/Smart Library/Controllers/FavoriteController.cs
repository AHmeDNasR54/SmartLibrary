using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Models.Repositories;
using SmartLibrary.Models;
using System.Security.Claims;
using SmartLibrary.Models.DTOs;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Only authenticated users can manage favorites
    public class FavoriteController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;

        public FavoriteController(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/favorite
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Unauthorized"
                });

            var favorites = await _unitOfWork.Favorites.GetUserFavoritesAsync(userId);

            var favoriteDto = favorites.Select(f => new
            {
                f.BookId,
                f.Book.Title,
                f.Book.Author,
                f.Book.Description,
                f.Book.Price,
                f.Book.CategoryName,
                f.Book.ImageUrl,
                f.Book.AvailableCopies
            });

            return Ok(favoriteDto);
        }

        // POST: api/favorite/{bookId}
        [HttpPost("{bookId}")]
        public async Task<IActionResult> AddFavorite(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Unauthorized"
                });

            var exists = await _unitOfWork.Favorites.ExistsAsync(userId, bookId);
            if (exists)
                return BadRequest(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Book is already in favorites."
                });

            var favorite = new Favorite
            {
                UserId = userId,
                BookId = bookId
            };

            await _unitOfWork.Favorites.AddFavoriteAsync(favorite);
            await _unitOfWork.complete();

            return Ok(new ApiResponseDto<object>
            {
                Success = true,
                Message = "Book added to favorites."
            });
        }

        // DELETE: api/favorite/{bookId}
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> RemoveFavorite(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Unauthorized"
                });

            var favorite = await _unitOfWork.Favorites.GetFavoriteAsync(userId, bookId);
            if (favorite == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Favorite not found."
                });

            _unitOfWork.Favorites.Remove(favorite);
            await _unitOfWork.complete();

            return Ok(new ApiResponseDto<object>
            {
                Success = true,
                Message = "Book removed from favorites."
            });
        }
    }
}



//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SmartLibrary.Models.Repositories;
//using SmartLibrary.Models;
//using System.Security.Claims;

//namespace Smart_Library.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize] // Only authenticated users can manage favorites
//    public class FavoriteController : ControllerBase
//    {
//        private readonly IunitOfWork _unitOfWork;

//        public FavoriteController(IunitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        // GET: api/favorite
//        [HttpGet]
//        public async Task<IActionResult> GetUserFavorites()
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null) return Unauthorized();

//            var favorites = await _unitOfWork.Favorites.GetUserFavoritesAsync(userId);

//            var favoriteDto = favorites.Select(f => new
//            {
//                f.BookId,
//                f.Book.Title,
//                f.Book.Author
//            });

//            return Ok(favoriteDto);
//        }

//        // POST: api/favorite/{bookId}
//        [HttpPost("{bookId}")]
//        public async Task<IActionResult> AddFavorite(int bookId)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null) return Unauthorized();

//            var exists = await _unitOfWork.Favorites.ExistsAsync(userId, bookId);
//            if (exists) return BadRequest("Book is already in favorites.");

//            var favorite = new Favorite
//            {
//                UserId = userId,
//                BookId = bookId
//            };

//            await _unitOfWork.Favorites.AddFavoriteAsync(favorite);
//           await  _unitOfWork.complete();

//            return Ok(new { Message = "Book added to favorites." });
//        }

//        // DELETE: api/favorite/{bookId}
//        [HttpDelete("{bookId}")]
//        public async Task<IActionResult> RemoveFavorite(int bookId)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null) return Unauthorized();

//            var favorite = await _unitOfWork.Favorites.GetFavoriteAsync(userId, bookId);
//            if (favorite == null) return NotFound("Favorite not found.");

//            _unitOfWork.Favorites.Remove(favorite);
//            await _unitOfWork.complete();

//            return Ok(new { Message = "Book removed from favorites." });
//        }
//    }
//}
