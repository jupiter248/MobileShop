using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Comment;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using MainApi.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        public CommentController(ICommentRepository commentRepository, IProductRepository productRepository)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            string username = User.GetUsername();
            List<Comment>? comments = await _commentRepository.GetAllCommentAsync(username);
            if (comments == null) return NotFound();
            List<CommentDto>? commentDtos = comments.Select(c => c.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            Comment? comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null) return BadRequest();
            return Ok(comment.ToCommentDto());
        }
        [Authorize]
        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddComment([FromRoute] int id, AddCommentRequestDto addCommentRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string username = User.GetUsername();

            Product? product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                Comment? comment = addCommentRequestDto.ToCommentFromAdd(id);
                comment.Product = product;
                Comment? commentModel = await _commentRepository.AddCommentAsync(comment, username);
                if (commentModel != null)
                {
                    return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.ToCommentDto());
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound("Product is not found");
            }

        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditComment([FromRoute] int id, EditCommentRequestDto editCommentRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string username = User.GetUsername();
            if (username != null)
            {
                Comment? commentModel = await _commentRepository.EditCommentAsync(id, editCommentRequestDto.ToCommentFromEdit(), username);
                if (commentModel != null)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Comment is not found");
                }
            }
            else
            {
                return NotFound("User is not found");
            }
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int id)
        {
            Comment? comment = await _commentRepository.RemoveCommentAsync(id);
            if (comment == null) return NotFound();
            return NoContent();
        }
    }
}