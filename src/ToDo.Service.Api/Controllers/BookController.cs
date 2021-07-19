using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.EventSourcedNormalizers;
using ToDo.Application.Interfaces;
using ToDo.Application.ViewModels;
using ToDo.Domain.Interfaces;
using ToDo.Service.Api.Filters;

namespace ToDo.Service.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IMinioService _minioService;

        public BookController(IBookService bookService, IMinioService minioService)
        {
            _bookService = bookService;
            _minioService = minioService;
        }

        [HttpGet("book-management")]
        public async Task<IEnumerable<BookViewModel>> GetAsync()
        {
            return await _bookService.GetAll();
        }

        [HttpGet("book-management/{id:guid}")]
        [ServiceFilter(typeof(CacheResourceFilter))]
        public async Task<BookViewModel> Get(Guid id)
        {
            return await _bookService.GetById(id);
        }

        [HttpPost("book-image")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            await _minioService.UploadFileAsync(file);            
            return Ok();
        }

        [HttpPost("book-management")]
        public async Task<IActionResult> Post([FromBody] BookViewModel bookViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookService.Create(bookViewModel));
        }

        [HttpPut("book-management")]
        public async Task<IActionResult> Put([FromBody] BookViewModel bookViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookService.Update(bookViewModel));
        }

        [HttpDelete("book-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _bookService.Delete(id));
        }

        [HttpGet("book-management/history/{id:guid}")]
        public async Task<IList<BookHistoryData>> History(Guid id)
        {
            return await _bookService.GetAllHistory(id);
        }
    }
}
