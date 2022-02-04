using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class SystemParameters
    {
        [Key] public string IDSystemParameters { get; set; } = "671F0752-45EB-4B86-AA4E-E0648F1B94A7";
        [Required] public int LaboralTaskCounter { get; set; } = 0;
    }
}