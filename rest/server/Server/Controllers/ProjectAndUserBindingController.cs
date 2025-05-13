using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.ProjectAndUserBindingDTOs.Requests;
using Server.Mapper.ProjectAndUserBindingDTOs.Responses;
using Server.Models.DTOs.ProjectAndUserBinding.Requests;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectAndUserBindingController : ControllerBase
    {
        private readonly IProjectAndUserBindingRepository _projectAndUserBindingRepository;
        private readonly IProjectAndUserBindingToGetProjectAndUserBindingDtoMapper _projectAndUserBindingToGetProjectAndUserBindingDtoMapper;
        private readonly ICreateProjectAndUserBindingDtoToProjectAndUserBindingMapper _createProjectAndUserBindingDtoToProjectAndUserBindingMapper;
        private readonly IUpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper _updateProjectAndUserBindingDtoToProjectAndUserBindingMapper;

        public ProjectAndUserBindingController(IProjectAndUserBindingRepository projectAndUserBindingRepository, IProjectAndUserBindingToGetProjectAndUserBindingDtoMapper projectAndUserBindingToGetProjectAndUserBindingDtoMapper, ICreateProjectAndUserBindingDtoToProjectAndUserBindingMapper createProjectAndUserBindingDtoToProjectAndUserBindingMapper, IUpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper updateProjectAndUserBindingDtoToProjectAndUserBindingMapper)
        {
            _projectAndUserBindingRepository = projectAndUserBindingRepository;
            _projectAndUserBindingToGetProjectAndUserBindingDtoMapper = projectAndUserBindingToGetProjectAndUserBindingDtoMapper;
            _createProjectAndUserBindingDtoToProjectAndUserBindingMapper = createProjectAndUserBindingDtoToProjectAndUserBindingMapper;
            _updateProjectAndUserBindingDtoToProjectAndUserBindingMapper = updateProjectAndUserBindingDtoToProjectAndUserBindingMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProjectAndUserBindings()
        {
            try
            {
                var projectAndUserBindingList = await _projectAndUserBindingRepository.GetProjectAndUserBindingsAsync();
                var getProjectAndUserBindingList = projectAndUserBindingList.Select(_projectAndUserBindingToGetProjectAndUserBindingDtoMapper.Map).ToList();
                return Ok(getProjectAndUserBindingList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProjectAndUserBindingById([FromRoute] Guid id)
        {
            try
            {
                var projectAndUserBinding = await _projectAndUserBindingRepository.GetProjectAndUserBindingByIdAsync(id);
                var getProjectAndUserBinding = _projectAndUserBindingToGetProjectAndUserBindingDtoMapper.Map(projectAndUserBinding);
                return Ok(getProjectAndUserBinding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProjectAndUserBinding([FromBody] CreateProjectAndUserBindingDto createProjectAndUserBindingDto)
        {
            try
            {
                if (!User.HasClaim("Role", "Admin") && !User.HasClaim("CanEditProject", createProjectAndUserBindingDto.ProjectID.ToString()))
                {
                    return Forbid();
                }

                var projectAndUserBinding = _createProjectAndUserBindingDtoToProjectAndUserBindingMapper.Map(createProjectAndUserBindingDto);
                await _projectAndUserBindingRepository.CreateProjectAndUserBindingAsync(projectAndUserBinding);
                return Ok("ProjectAndUserBinding created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProjectAndUserBinding([FromBody] UpdateProjectAndUserBindingDto updateProjectAndUserBindingDto)
        {
            try
            {
                var projectAndUserBinding = _updateProjectAndUserBindingDtoToProjectAndUserBindingMapper.Map(updateProjectAndUserBindingDto);
                await _projectAndUserBindingRepository.UpdateProjectAndUserBindingAsync(projectAndUserBinding);
                return Ok("ProjectAndUserBinding updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProjectAndUserBindingById([FromRoute] Guid id)
        {
            try
            {
                var projectAndUserBinding = await _projectAndUserBindingRepository.GetProjectAndUserBindingByIdAsync(id);
                await _projectAndUserBindingRepository.DeleteProjectAndUserBindingByIdAsync(id);
                return Ok("ProjectAndUserBinding deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
