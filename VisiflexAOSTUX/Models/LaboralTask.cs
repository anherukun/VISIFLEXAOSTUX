using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class LaboralTask
    {
        [Key] public string IDLaboralTask { get; set; }
        [Required] public string DocumentID { get; set; }
        public DateTime ReceptionDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime CommitmentDate { get; set; }
        public long UploadTicks { get; set; }
        [ForeignKey("IDDocumentFile")] public DocumentFile DocumentFile { get; set; }
        [ForeignKey("IDAttentionArea")] public AttentionArea AttentionArea { get; set; }
        [ForeignKey("IDRequesterArea")] public RequesterArea RequesterArea { get; set; }
        [ForeignKey("IDWorkplace")] public Workplace Workplace { get; set; }
        [ForeignKey("IDAgent")] public Agent Agent { get; set; }
        public List<LaboralTaskHistoryLog> LaboralTaskHistoryLogs { get; set; }
        public string IDDocumentFile { get; set; }
        public string IDAttentionArea { get; set; }
        public string IDRequesterArea { get; set; }
        public string IDWorkplace { get; set; }
        public string IDAgent { get; set; }
        public string Subjet { get; set; }
        public string LongDescription { get; set; }
        public string AttentionInstructions { get; set; }
        public string Observations { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
    }
}