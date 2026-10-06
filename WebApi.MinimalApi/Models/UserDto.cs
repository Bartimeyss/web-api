using WebApi.MinimalApi.Domain;
namespace WebApi.MinimalApi.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string FullName { get; set; }
    public int GamesPlayed { get; set; }
    public Guid? CurrentGameId { get; set; }
    
    public UserDto() {}
    public UserDto(UserEntity userEntity)
    {
        this.Id = userEntity.Id;
        this.Login = userEntity.Login;
        this.FullName = $"{userEntity.LastName} {userEntity.FirstName}";
        this.GamesPlayed = userEntity.GamesPlayed;
        this.CurrentGameId = userEntity.CurrentGameId;
    }
}
