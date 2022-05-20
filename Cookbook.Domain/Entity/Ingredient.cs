using Cookbook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookbook.Domain.Entity
{
    public class Ingredient : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("AmountOfUnit")]
        public int AmountOfUnit { get; set; }
        [XmlElement("Unit")]
        public string Unit { get; set; }

        public Ingredient()
        {

        }
        public Ingredient(int id, string name, int amount, string unit)
        {
            Id = id;
            Name = name;
            AmountOfUnit = amount;
            Unit = unit;
        }
    }
}
