using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Models;
using SmartLibrary.Models.DTOs;
using SmartLibrary.Models.Repositories;
using SmartLibrary.Utilities;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapp;
        public CategoryController(IunitOfWork iunitOfWork,IMapper mapp) { 
        
            _unitOfWork = iunitOfWork;
            _mapp = mapp;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
             return Ok( await _unitOfWork.Categories.GetAllAsync());
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);

            if(category == null)
                return NotFound();

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryDto categorydto)
        {
            var category = _mapp.Map<Category>(categorydto);
              await _unitOfWork.Categories.AddAsync(category);
               await _unitOfWork.complete();
                return Ok(category);
               
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CategoryDto categorydto)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);

            if (category ==null)
                return NotFound();

            _mapp.Map(categorydto,category);
            _unitOfWork.Categories.Update(category);
           await   _unitOfWork.complete();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            _unitOfWork.Categories.Remove(category);
             await _unitOfWork.complete();
            return NoContent();
        }
    }


}

