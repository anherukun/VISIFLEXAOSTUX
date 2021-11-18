using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    [Serializable()]
    public class Backup
    {
        public List<Account> Accounts { get; set; }
        public List<AttentionArea> AttentionAreas { get; set; }
        public List<DocumentFile> DocumentFiles { get; set; }
        public List<LaboralTask> LaboralTasks { get; set; }
        public List<LaboralTaskHistoryLog> LaboralTaskHistoryLogs { get; set; }
        public List<HistoryLogDocumentFile> HistoryLogDocumentFiles { get; set; }
        public List<RequesterArea> RequesterAreas { get; set; }
        public List<Session> Sessions { get; set; }
        public List<UserRol> UserRols { get; set; }
        public List<Workplace> Workplaces { get; set; }

        public byte[] ToBytes()
        {
            if (this != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, this);
                    return ms.ToArray();
                }
            }

            throw new NullReferenceException("El objeto \"Backup\" es nulo, no se puede convertir");
        }

        public static Backup FromBytes(byte[] bytes)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return formatter.Deserialize(memoryStream) as Backup;
            }
            catch (Exception ex)
            {

                throw new Exception("Los datos no pueden ser leidos correctamente o no son compatibles", ex);
            }
        }
    }
}