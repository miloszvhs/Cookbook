using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookbook.Domain.Common
{
    public class AuditableModel
    {
        [XmlIgnore]
        public int CreatedById { get; set; }
        [XmlIgnore]
        public DateTime CreatedDateTime { get; set; }
        [XmlIgnore]
        public int? ModifiedById { get; set; }
        [XmlIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
