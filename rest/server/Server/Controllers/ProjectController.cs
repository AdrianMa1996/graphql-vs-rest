using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.ProjectDTOs.Requests;
using Server.Mapper.ProjectDTOs.Responses;
using Server.Models.DTOs.Project.Requests;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectToGetProjectDtoMapper _projectToGetProjectDtoMapper;
        private readonly ICreateProjectDtoToProjectMapper _createProjectDtoToProjectMapper;
        private readonly IUpdateProjectDtoToProjectMapper _updateProjectDtoToProjectMapper;

        public ProjectController(IProjectRepository projectRepository, IProjectToGetProjectDtoMapper projectToGetProjectDtoMapper, ICreateProjectDtoToProjectMapper createProjectDtoToProjectMapper, IUpdateProjectDtoToProjectMapper updateProjectDtoToProjectMapper)
        {
            _projectRepository = projectRepository;
            _projectToGetProjectDtoMapper = projectToGetProjectDtoMapper;
            _createProjectDtoToProjectMapper = createProjectDtoToProjectMapper;
            _updateProjectDtoToProjectMapper = updateProjectDtoToProjectMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projectList = await _projectRepository.GetProjectsAsync();
                var getProjectList = projectList.Select(_projectToGetProjectDtoMapper.Map).ToList();
                return Ok(getProjectList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            try
            {
                var project = await _projectRepository.GetProjectByIdAsync(id);
                var getProject = _projectToGetProjectDtoMapper.Map(project);
                return Ok(getProject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            try
            {
                var project = _createProjectDtoToProjectMapper.Map(createProjectDto);
                await _projectRepository.CreateProjectAsync(project);
                return Ok("Project created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto updateProjectDto)
        {
            try
            {
                if (!User.HasClaim("Role", "Admin") && !User.HasClaim("CanEditProject", updateProjectDto.ProjectID.ToString()))
                {
                    return Forbid();
                }

                var project = _updateProjectDtoToProjectMapper.Map(updateProjectDto);
                await _projectRepository.UpdateProjectAsync(project);
                return Ok("Project updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProjectById([FromRoute] Guid id)
        {
            try
            {
                if (!User.HasClaim("Role", "Admin") && !User.HasClaim("CanEditProject", id.ToString()))
                {
                    return Forbid();
                }

                await _projectRepository.DeleteProjectByIdAsync(id);
                return Ok("Project deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
