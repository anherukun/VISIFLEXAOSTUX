using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class Agent
    {
        [Key] public string AgentID { get; set; }
        [Required] public int AgentCode { get; set; }
        [Required] public string Name { get; set; }
    }
}