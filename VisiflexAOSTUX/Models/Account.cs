using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class Account
    {
        [Key] public string IDAccount { get; set; }
        [Required, Index(IsUnique = true, IsClustered = true), StringLength(150)] public string Email { get; set; }
        [Required, Index(IsUnique = true, IsClustered = true), StringLength(10)] public string Username { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required, ForeignKey("UserRol")] public string IDUserRol { get; set; }
        public UserRol UserRol { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
    }

    public class UserRol
    {
        [Key] public string IDUserRol { get; set; }
        [Required, Index(IsUnique = true, IsClustered = true)] public int UserLevel { get; set; }
        [Required] public string UserRolName { get; set; }
        [Required] public string UserRolPermisions { get; set; }
    }
}