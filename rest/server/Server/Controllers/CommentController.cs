using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.CommentDTOs.Requests;
using Server.Mapper.CommentDTOs.Responses;
using Server.Models.DTOs.Comment.Requests;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentToGetCommentDtoMapper _commentToGetCommentDtoMapper;
        private readonly ICreateCommentDtoToCommentMapper _createCommentDtoToCommentMapper;
        private readonly IUpdateCommentDtoToCommentMapper _updateCommentDtoToCommentMapper;

        public CommentController(ICommentRepository commentRepository, ICommentToGetCommentDtoMapper commentToGetCommentDtoMapper, ICreateCommentDtoToCommentMapper createCommentDtoToCommentMapper, IUpdateCommentDtoToCommentMapper updateCommentDtoToCommentMapper)
        {
            _commentRepository = commentRepository;
            _commentToGetCommentDtoMapper = commentToGetCommentDtoMapper;
            _createCommentDtoToCommentMapper = createCommentDtoToCommentMapper;
            _updateCommentDtoToCommentMapper = updateCommentDtoToCommentMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var commentList = await _commentRepository.GetCommentsAsync();
                var getCommentList = commentList.Select(_commentToGetCommentDtoMapper.Map).ToList();
                return Ok(getCommentList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid id)
        {
            try
            {
                var comment = await _commentRepository.GetCommentByIdAsync(id);
                var getComment = _commentToGetCommentDtoMapper.Map(comment);
                return Ok(getComment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            try
            {
                var comment = _createCommentDtoToCommentMapper.Map(createCommentDto);
                await _commentRepository.CreateCommentAsync(comment);
                return Ok("Comment created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto updateCommentDto)
        {
            try
            {
                var comment = _updateCommentDtoToCommentMapper.Map(updateCommentDto);
                await _commentRepository.UpdateCommentAsync(comment);
                return Ok("Comment updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCommentById([FromRoute] Guid id)
        {
            try
            {
                await _commentRepository.DeleteCommentByIdAsync(id);
                return Ok("Comment deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
