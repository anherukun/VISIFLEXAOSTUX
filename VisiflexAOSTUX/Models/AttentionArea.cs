using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class AttentionArea
    {
        [Key] public string AttentionAreaID { get; set; }
        [Required] public int AreaCode { get; set; }
        [Required] public string Name { get; set; }
    }
}