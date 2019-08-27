namespace GarboSellsUserDataService.Models
{
  public class Role
  {
    public long RoleId { get; set; }
    public string RoleDescripton { get; set; }
    public bool CanPost { get; set; }
    public bool CanDelete { get; set; }
  }
}
