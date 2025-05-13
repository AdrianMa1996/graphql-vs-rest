using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.ContributionDTOs.Requests;
using Server.Mapper.ContributionDTOs.Responses;
using Server.Models.DTOs.Contribution.Requests;
using Server.Models.DTOs.Contribution.Responses;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IContributionToGetContributionDtoMapper _contributionToGetContributionDtoMapper;
        private readonly ICreateContributionDtoToContributionMapper _createContributionDtoToContributionMapper;
        private readonly IUpdateContributionDtoToContributionMapper _updateContributionDtoToContributionMapper;
        private readonly IVoteRepository _voteRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPatchContributionDtoToContributionMapper _patchContributionDtoToContributionMapper;
        private readonly IUserRepository _userRepository;

        public ContributionController(IContributionRepository contributionRepository, IContributionToGetContributionDtoMapper contributionToGetContributionDtoMapper, ICreateContributionDtoToContributionMapper createContributionDtoToContributionMapper, IUpdateContributionDtoToContributionMapper updateContributionDtoToContributionMapper, IVoteRepository voteRepository, ICommentRepository commentRepository, IPatchContributionDtoToContributionMapper patchContributionDtoToContributionMapper, IUserRepository userRepository)
        {
            _contributionRepository = contributionRepository;
            _contributionToGetContributionDtoMapper = contributionToGetContributionDtoMapper;
            _createContributionDtoToContributionMapper = createContributionDtoToContributionMapper;
            _updateContributionDtoToContributionMapper = updateContributionDtoToContributionMapper;
            _voteRepository = voteRepository;
            _commentRepository = commentRepository;
            _patchContributionDtoToContributionMapper = patchContributionDtoToContributionMapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllContributions()
        {
            try
            {
                var contributionList = await _contributionRepository.GetContributionsAsync();
                var getContributionList = contributionList.Select(_contributionToGetContributionDtoMapper.Map).ToList();
                return Ok(getContributionList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetContributionById([FromRoute] Guid id)
        {
            try
            {
                var contribution = await _contributionRepository.GetContributionByIdAsync(id);
                var getContribution = _contributionToGetContributionDtoMapper.Map(contribution);
                return Ok(getContribution);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateContribution([FromBody] CreateContributionDto createContributionDto)
        {
            try
            {
                var contribution = _createContributionDtoToContributionMapper.Map(createContributionDto);
                await _contributionRepository.CreateContributionAsync(contribution);
                return Ok("Contribution created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateContribution([FromBody] UpdateContributionDto updateContributionDto)
        {
            try
            {
                var contribution = _updateContributionDtoToContributionMapper.Map(updateContributionDto);
                await _contributionRepository.UpdateContributionAsync(contribution);
                return Ok("Contribution updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchContribution([FromBody] PatchContributionDto patchContributionDto)
        {
            try
            {
                var existingContribution = await _contributionRepository.GetContributionByIdAsync(patchContributionDto.ContributionID);
                var contribution = _patchContributionDtoToContributionMapper.Map(existingContribution, patchContributionDto);
                await _contributionRepository.UpdateContributionAsync(contribution);

                return Ok("Contribution updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteContributionById([FromRoute] Guid id)
        {
            try
            {
                await _contributionRepository.DeleteContributionByIdAsync(id);
                return Ok("Contribution deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("overview")]
        [Authorize]
        public async Task<IActionResult> GetAllContributionOverviews([FromQuery] Guid? projectId, [FromQuery] string? category, [FromQuery] string? status, [FromQuery] string? orderByDate)
        {
            try
            {
                // var contributions = await _contributionRepository.GetContributionsAsync();

                var contributions = await _contributionRepository.GetContributionsFilteredAsync(projectId, category, status);

                var overviewList = new List<GetContributionOverviewDto>();

                foreach (var contribution in contributions)
                {
                    var votes = await _voteRepository.GetVotesByContributionIdAsync(contribution.ContributionID);
                    var comments = await _commentRepository.GetCommentsByContributionIdAsync(contribution.ContributionID);

                    var overview = new GetContributionOverviewDto
                    {
                        ContributionID = contribution.ContributionID,
                        ProjectID = contribution.ProjectID,
                        UserID = contribution.UserID,
                        Category = contribution.Category,
                        Title = contribution.Title,
                        Text = contribution.Text,
                        Date = contribution.Date,
                        Status = contribution.Status,
                        Votes = votes.Select(v => new VoteDto
                        {
                            VoteID = v.VoteID,
                            UserID = v.UserID
                        }).ToList(),
                        Comments = comments.Select(c => new CommentDto
                        {
                            CommentID = c.CommentID
                        }).ToList()
                    };

                    overviewList.Add(overview);
                }

                if (orderByDate == "DESC")
                    overviewList = overviewList.OrderByDescending(o => DateTime.Parse(o.Date)).ToList();
                else if (orderByDate == "ASC")
                    overviewList = overviewList.OrderBy(o => DateTime.Parse(o.Date)).ToList();

                return Ok(overviewList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("detail/{id}")]
        [Authorize]
        public async Task<IActionResult> GetContributionDetailById([FromRoute] Guid id)
        {
            try
            {
                var contribution = await _contributionRepository.GetContributionByIdAsync(id);
                var user = await _userRepository.GetUserByIdAsync(contribution.UserID);
                var votes = await _voteRepository.GetVotesByContributionIdAsync(contribution.ContributionID);
                var comments = await _commentRepository.GetCommentsByContributionIdAsync(contribution.ContributionID);

                var commentUserIds = comments.Select(c => c.UserID).ToList();
                var commentUsers = await _userRepository.GetUsersByIdsAsync(commentUserIds);
                var userDict = commentUsers.ToDictionary(u => u.UserID);

                var contributionDetail = new GetContributionDetailDto
                {
                    ProjectID = contribution.ProjectID,
                    UserID = contribution.UserID,
                    Category = contribution.Category,
                    Title = contribution.Title,
                    Text = contribution.Text,
                    Date = contribution.Date,
                    Status = contribution.Status,
                    Creator = new CreatorDetailDto
                    {
                        Name = user.Name,
                        ProfilPicture = user.ProfilPicture
                    },
                    Votes = votes.Select(v => new VoteDetailDto
                    {
                        VoteID = v.VoteID,
                        UserID = v.UserID
                    }).ToList(),
                    Comments = comments.Select(c =>
                    {
                        var commentUser = userDict[c.UserID];
                        return new CommentDetailDto
                        {
                            Text = c.Text,
                            Date = c.Date,
                            Creator = new CreatorDetailDto
                            {
                                Name = commentUser.Name,
                                ProfilPicture = commentUser.ProfilPicture
                            }
                        };
                    }).ToList()
                };

                return Ok(contributionDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
