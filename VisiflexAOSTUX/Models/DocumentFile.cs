using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    [Serializable()]
    public class DocumentFile
    {
        [Key] public string DocumentFileID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Mime { get; set; }
        [Required] public int Size { get; set; }
        [Required] public byte[] Data { get; set; }
        [Required] public long UploadTicks { get; set; }
        public string LaboralTaskID { get; set; }
    }
}