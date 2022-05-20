using Cookbook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookbook.Domain.Entity
{
    public class Recipe : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Ingredients")]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        [XmlElement("Description")]
        public string Description { get; set; }

        public Recipe()
        {

        }

        public Recipe(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
