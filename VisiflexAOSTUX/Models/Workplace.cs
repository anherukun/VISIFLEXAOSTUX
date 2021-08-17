using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class Workplace
    {
        [Key] public string WorkplaceID { get; set; }
        [Required] public int WorkplaceCode { get; set; }
        [Required] public string Name { get; set; }
    }
}