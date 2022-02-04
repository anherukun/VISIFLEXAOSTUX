using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    [Serializable()]
    public class RequesterArea
    {
        [Key] public string RequesterAreaID { get; set; }
        [Required] public string Name { get; set; }
    }
}