using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.VoteDTOs.Requests;
using Server.Mapper.VoteDTOs.Responses;
using Server.Models.DTOs.Vote.Requests;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IVoteToGetVoteDtoMapper _voteToGetVoteDtoMapper;
        private readonly ICreateVoteDtoToVoteMapper _createVoteDtoToVoteMapper;
        private readonly IUpdateVoteDtoToVoteMapper _updateVoteDtoToVoteMapper;

        public VoteController(IVoteRepository voteRepository, IVoteToGetVoteDtoMapper voteToGetVoteDtoMapper, ICreateVoteDtoToVoteMapper createVoteDtoToVoteMapper, IUpdateVoteDtoToVoteMapper updateVoteDtoToVoteMapper)
        {
            _voteRepository = voteRepository;
            _voteToGetVoteDtoMapper = voteToGetVoteDtoMapper;
            _createVoteDtoToVoteMapper = createVoteDtoToVoteMapper;
            _updateVoteDtoToVoteMapper = updateVoteDtoToVoteMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllVotes()
        {
            try
            {
                var voteList = await _voteRepository.GetVotesAsync();
                var getVoteList = voteList.Select(_voteToGetVoteDtoMapper.Map).ToList();
                return Ok(getVoteList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetVoteById([FromRoute] Guid id)
        {
            try
            {
                var vote = await _voteRepository.GetVoteByIdAsync(id);
                var getVote = _voteToGetVoteDtoMapper.Map(vote);
                return Ok(getVote);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVote([FromBody] CreateVoteDto createVoteDto)
        {
            try
            {
                var vote = _createVoteDtoToVoteMapper.Map(createVoteDto);
                await _voteRepository.CreateVoteAsync(vote);
                return Ok("Vote created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateVote([FromBody] UpdateVoteDto updateVoteDto)
        {
            try
            {
                var vote = _updateVoteDtoToVoteMapper.Map(updateVoteDto);
                await _voteRepository.UpdateVoteAsync(vote);
                return Ok("Vote updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVoteById([FromRoute] Guid id)
        {
            try
            {
                await _voteRepository.DeleteVoteByIdAsync(id);
                return Ok("Vote deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
