﻿using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Models.DTO;
using PetShopApi.Services;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <remarks>
        /// Creates a new user with email and name. It is not meant for production and does not implement passwords or authentication tokens!<br/>
        /// Both fields are required. Returns the newly created user entry.
        /// </remarks>
        /// <param name="dto">Payload with name and email. Both are required.</param>        
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDTO dto)
        {
            return Ok(await _userService.CreateUserAsync(dto));
        }

        /// <summary>
        /// Deletes an existing user.
        /// </summary>
        /// <remarks>
        /// Deletes an user based on it's id.
        /// Returns the deleted user entry.
        /// </remarks>
        /// <param name="id">The id of an existing user. Must be a valid 24 digits hex value.</param>        
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var result = await _userService.GetUserAsync(id);
            if (result == null)
                return NotFound();

            return Ok(await _userService.DeleteUserAsync(id));
        }

        /// <summary>
        /// Retrieves a list with all users.
        /// </summary>
        /// <remarks>
        /// Returns the list with all users.
        /// </remarks>   
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an user based on it's id.
        /// </summary>
        /// <remarks>
        /// Returns the user if it exists.
        /// </remarks>
        /// <param name="id">The id of an existing user. Must be a valid 24 digits hex value.</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var result = await _userService.GetUserAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Finds and updates an user with new attributes.
        /// </summary>
        /// <remarks>
        /// Returns the updated user or the original user if no changes were made.<br/>
        /// Changes will only be commited if the payload contains a new (different) value for AT LEAST one property.<br/>        
        /// </remarks>
        /// <param name="id">The id of an existing user. Must be a valid 24 digits hex value.</param>
        /// <param name="dto">Payload with the new values for the existing user.<br/></param>       
        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] UpdateUserDTO dto)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
                return NotFound();

            return Ok(await _userService.UpdateUserAsync(id, user, dto));
        }
    }
}
