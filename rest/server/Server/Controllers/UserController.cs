using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Mapper.UserDTOs.Requests;
using Server.Mapper.UserDTOs.Responses;
using Server.Models.DTOs.User.Requests;
using Server.Models.DTOs.User.Responses;
using Server.Repositories;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserToGetUserDtoMapper _userToGetUserDtoMapper;
        private readonly ICreateUserDtoToUserMapper _createUserDtoToUserMapper;
        private readonly IUpdateUserDtoToUserMapper _updateUserDtoToUserMapper;
        private readonly IUserToGetUserWithPasswordDtoMapper _userToGetUserWithPasswordDtoMapper;
        private readonly IPatchUserDtoToUserMapper _patchUserDtoToUserMapper;

        public UserController(IUserRepository userRepository, IUserToGetUserDtoMapper userToGetUserDtoMapper, ICreateUserDtoToUserMapper createUserDtoToUserMapper, IUpdateUserDtoToUserMapper updateUserDtoToUserMapper, IUserToGetUserWithPasswordDtoMapper userToGetUserWithPasswordDtoMapper, IPatchUserDtoToUserMapper patchUserDtoToUserMapper)
        {
            _userRepository = userRepository;
            _userToGetUserDtoMapper = userToGetUserDtoMapper;
            _createUserDtoToUserMapper = createUserDtoToUserMapper;
            _updateUserDtoToUserMapper = updateUserDtoToUserMapper;
            _userToGetUserWithPasswordDtoMapper = userToGetUserWithPasswordDtoMapper;
            _patchUserDtoToUserMapper = patchUserDtoToUserMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userList = await _userRepository.GetUsersAsync();
                var getUserList = userList.Select(_userToGetUserDtoMapper.Map).ToList();
                return Ok(getUserList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                var getUser = _userToGetUserDtoMapper.Map(user);
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var user = _createUserDtoToUserMapper.Map(createUserDto);
                await _userRepository.CreateUserAsync(user);
                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var user = _updateUserDtoToUserMapper.Map(updateUserDto);
                await _userRepository.UpdateUserAsync(user);
                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchUser([FromBody] PatchUserDto patchUserDto)
        {
            try
            {
                if (!User.HasClaim("Role", "Admin") && !User.HasClaim("CanEditUser", patchUserDto.UserID.ToString()))
                {
                    return Forbid();
                }
                if (!User.HasClaim("Role", "Admin") && patchUserDto.Role != null)
                {
                    return Forbid();
                }
                var existingUser = await _userRepository.GetUserByIdAsync(patchUserDto.UserID);
                var user = _patchUserDtoToUserMapper.Map(existingUser, patchUserDto);
                await _userRepository.UpdateUserAsync(user);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
        {
            try
            {
                await _userRepository.DeleteUserByIdAsync(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Name")]
        [Authorize]
        public async Task<IActionResult> GetAllUserNames()
        {
            try
            {
                var userList = await _userRepository.GetUsersAsync();
                var listItems = userList.Select(user => new GetUserNameDto
                {
                    UserID = user.UserID,
                    Name = user.Name
                }).ToList();

                return Ok(listItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("WithPassword/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserWithPasswordById([FromRoute] Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (!User.HasClaim("Role", "Admin") && !User.HasClaim("CanEditUser", user.UserID.ToString()))
                {
                    return Forbid();
                }
                var getUser = _userToGetUserWithPasswordDtoMapper.Map(user);
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
