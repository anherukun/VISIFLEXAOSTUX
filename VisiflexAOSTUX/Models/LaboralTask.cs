﻿using System;
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
        [Required] public DateTime ReceptionDate { get; set; }
        [Required] public DateTime DocumentDate { get; set; }
        [Required] public DateTime CommitmentDate { get; set; }
        [Required] public long UploadTicks { get; set; }
        [ForeignKey("IDDocumentFile")] public DocumentFile DocumentFile { get; set; }
        [ForeignKey("IDAttentionArea")] public AttentionArea AttentionArea { get; set; }
        [ForeignKey("IDRequesterArea")] public RequesterArea RequesterArea { get; set; }
        [ForeignKey("IDWorkplace")] public Workplace Workplace { get; set; }
        [ForeignKey("IDAgent")] public Agent Agent { get; set; }
        public List<LaboralTaskHistoryLog> LaboralTaskHistoryLogs { get; set; }
        public string IDDocumentFile { get; set; }
        [Required] public string IDAttentionArea { get; set; }
        [Required] public string IDRequesterArea { get; set; }
        [Required] public string IDWorkplace { get; set; }
        [Required] public string IDAgent { get; set; }
        [Required] public string Subjet { get; set; }
        [Required] public string LongDescription { get; set; }
        [Required] public string AttentionInstructions { get; set; }
        [Required] public string Observations { get; set; }
        [Required] public string Status { get; set; }
        public int Progress { get; set; }
        [Required] public int Count { get; set; }
    }
}