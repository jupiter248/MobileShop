using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Comment;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Orders;
using MainApi.Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IProductRepository _productRepository;
        public CommentController(ICommentService commentService, IProductRepository productRepository)
        {
            _commentService = commentService;
            _productRepository = productRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            string? username = User.GetUsername();
            if (username == null) return NotFound("User not found");
            List<CommentDto>? commentDtos = await _commentService.GetAllUserCommentsAsync(username);
            return Ok(commentDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int commentId)
        {
            CommentDto commentDto = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(commentDto);
        }
        [Authorize]
        [HttpPost("{productId:int}")]
        public async Task<IActionResult> AddComment([FromRoute] int productId, AddCommentRequestDto addCommentRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("Username is invalid");

            CommentDto? commentDto = await _commentService.AddCommentAsync(productId, addCommentRequestDto, username);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);

        }
        [Authorize]
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> EditComment([FromRoute] int commentId, EditCommentRequestDto editCommentRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (username == null) return NotFound("User is not found");

            await _commentService.UpdateCommentAsync(commentId, editCommentRequestDto, username);
            return NoContent();

        }
        [Authorize]
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int commentId)
        {
            string? username = User.GetUsername();
            if (username == null) return NotFound("User is not found");
            await _commentService.DeleteCommentAsync(commentId, username);
            return NoContent();
        }
    }
}