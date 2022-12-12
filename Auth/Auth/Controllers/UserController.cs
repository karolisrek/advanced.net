using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        [HttpPost]
        public void SaveUser(User user)
        {
            _userRepository.Save(user);
        }
    }
}