using Microsoft.AspNetCore.Mvc;
using WebApi.MinimalApi.Domain;
using WebApi.MinimalApi.Models;
using AutoMapper;

namespace WebApi.MinimalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private IUserRepository userRepository;
    private IMapper mapper;
    // Чтобы ASP.NET положил что-то в userRepository требуется конфигурация
    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    [Produces("application/json", "application/xml")]
    [HttpGet("{userId}", Name = nameof(GetUserById))]
    public ActionResult<UserDto> GetUserById([FromRoute] Guid userId)
    {
        var user = userRepository.FindById(userId);
        if (user is null)
        {
            return NotFound();
        }
        var response = mapper.Map<UserDto>(user);
        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserDto user)
    {
        if (user is null)
            return BadRequest();
        if (!userEntity.Login.All(c => char.IsLetterOrDigit(c)))
            ModelState.AddModelError(userEntity.Login, "Не поддерживаемые символы в логине");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);




        var userEntity = mapper.Map<UserEntity>(user);
        userEntity = userRepository.Insert(userEntity);
        return CreatedAtRoute(
            nameof(GetUserById),
            new { userId = userEntity.Id },
            userEntity);
    }
}
