using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class Session
    {
        [Key] public string IDSession { get; set; }
        [Required, Index(IsUnique = true, IsClustered = true), StringLength(45)] public string IDAccount { get; set; }
        public string SessionToken { get; set; }
        [Required] public DateTime LastLogin { get; set; }
    }
}