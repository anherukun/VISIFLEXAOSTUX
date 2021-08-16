using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class LaboralTask
    {
        public string IDLaboralTask { get; set; }
        public string DocumentID { get; set; }
        public DateTime DocumentDate { get; set; }
        // [ForeingKey("IDDocumentFile")]public DocumentFile DocumentFile { get; set; }
        // [ForeingKey("IDAttentionArea")]public AttentionArea AttentionArea { get; set; }
        // [ForeingKey("IDRequesterArea")]public RequesterArea RequesterArea { get; set; }
        // [ForeingKey("IDWorkplace")]public Workplace Workplace { get; set; }
        public string IDDocumentFile { get; set; }
        public string IDAttentionArea { get; set; }
        public string IDRequesterArea { get; set; }
        public string IDWorkplace { get; set; }
        public string Subjet { get; set; }
        public string LongDescription { get; set; }
        public string AttentionInstructions { get; set; }
        public string Observations { get; set; }

    }
}