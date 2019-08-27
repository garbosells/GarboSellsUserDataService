using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using GarboSellsUserDataService.Models;

namespace GarboSellsUserDataService.Database
{
  public class UserDataContext : DbContext
  {
    private IConfiguration configuration;
    public DbSet<UserDTO> Users { get; set; }
    public DbSet<UserRoleDTO> UserRoles { get; set; }

    public UserDataContext(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(GetConnectionString()).UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserDTO>().ToTable("users");
      modelBuilder.Entity<UserRoleDTO>().ToTable("user_roles");
      modelBuilder.Entity<UserDTO>().HasKey(u => u.UserId);
      modelBuilder.Entity<UserRoleDTO>().HasKey(r => r.UserRoleId);
      modelBuilder.Entity<UserDTO>().HasOne<UserRoleDTO>(u => u.Role).WithMany(r => r.Users).IsRequired().HasForeignKey(u => u.UserRoleId);
      //modelBuilder.Entity<UserRole>().HasMany<User>(r => r.Users).WithOne(u => u.Role).HasForeignKey(u => u.UserRoleId);
    }

    private string GetConnectionString()
    {
      var server = configuration.GetValue<string>("UserDbConnectionInfo:Server");
      var port = configuration.GetValue<string>("UserDbConnectionInfo:Port");
      var user = configuration.GetValue<string>("UserDbConnectionInfo:User");
      var pw = configuration.GetValue<string>("UserDbConnectionInfo:Password");
      var database = configuration.GetValue<string>("UserDbConnectionInfo:Database");
      return $"Server={server};Port={port};User Id={user};Password={pw};Database={database};SSL Mode=Require;Trust Server Certificate=true";
    }

    public class UserDTO
    {
      [Column("user_id")]
      public long UserId { get; set; }
      [Column("user_login_id")]
      public string UserLoginId { get; set; }
      [Column("user_role_id")]
      public long UserRoleId { get; set; }
      public virtual UserRoleDTO Role { get; set; }

      public User ToUser()
      {
        return new User
        {
          UserId = UserId,
          UserLoginId = UserLoginId,
          Role = Role.ToRole()
        };
      }
    }

    public class UserRoleDTO
    {
      public virtual ICollection<UserDTO> Users { get; set; }
      [Column("user_role_id")]
      public long UserRoleId { get; set; }
      [Column("user_role_description")]
      public string UserRoleDescription { get; set; }
      [Column("can_post")]
      public bool CanPost { get; set; }
      [Column("can_delete")]
      public bool CanDelete { get; set; }

      public Role ToRole()
      {
        return new Role
        {
          RoleId = UserRoleId,
          RoleDescripton = UserRoleDescription,
          CanPost = CanPost,
          CanDelete = CanDelete
        };
      }
    }
  }
}
