namespace GarboSellsUserDataService.Models
{
  public class User
  {
    public long UserId { get; set; }
    public string UserLoginId { get; set; }
    public Role Role { get; set; }
  }
}
