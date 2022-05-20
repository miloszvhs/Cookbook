using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookbook.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
    }
}
