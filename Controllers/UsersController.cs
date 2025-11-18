using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<AppUser> Users = new List<AppUser>();

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] AppUser newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Name) || string.IsNullOrWhiteSpace(newUser.Email))
            {
                return BadRequest("Name and Email are required.");
            }

            if (!newUser.Email.Contains("@"))
            {
                return BadRequest("Invalid email format.");
            }

            try
            {
                newUser.Id = Users.Count > 0 ? Users.Max(u => u.Id) + 1 : 1;
                Users.Add(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] AppUser updatedUser)
        {
            if (string.IsNullOrWhiteSpace(updatedUser.Name) || string.IsNullOrWhiteSpace(updatedUser.Email))
            {
                return BadRequest("Name and Email are required.");
            }

            if (!updatedUser.Email.Contains("@"))
            {
                return BadRequest("Invalid email format.");
            }

            try
            {
                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            Users.Remove(user);
            return NoContent();
        }

        public class AppUser
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
        }
    }
}