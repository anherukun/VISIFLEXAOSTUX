using System.ComponentModel.DataAnnotations;

namespace VisiflexAOSTUX.Models
{
    public class HistoryLogDocumentFile
    {
        [Key] public string IDHistoryLogDocumentFile { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Mime { get; set; }
        [Required] public int Size { get; set; }
        [Required] public byte[] Data { get; set; }
        [Required] public long UploadTicks { get; set; }
        public string LaboralTaskHistoryLogID { get; set; }
    }
}