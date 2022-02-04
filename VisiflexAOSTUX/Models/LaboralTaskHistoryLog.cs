using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    [Serializable()]
    public class LaboralTaskHistoryLog
    {
        [Key] public string IDLaboralTaskHistoryLog { get; set; }
        [Required]public string Description { get; set; }
        [Required]public DateTime Date { get; set; }
        [Required]public long UploadTicks { get; set; }
        [ForeignKey("IDHistoryLogDocumentFile")] public HistoryLogDocumentFile HistoryLogDocumentFile { get; set; }
        public string IDHistoryLogDocumentFile { get; set; }
        public string IDLaboralTask { get; set; }
    }
}