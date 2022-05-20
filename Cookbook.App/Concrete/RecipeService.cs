using Cookbook.App.Common;
using Cookbook.Domain.Common;
using Cookbook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cookbook.App.Concrete
{
    public class RecipeService : BaseService<Recipe>
    {
        private const string PATH = @"C:\Temp\recipes.xml";

        public RecipeService()
        {

        }
        
        public void SaveToXML()
        {
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "recipes";
            root.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>), root);

            using StreamWriter sw = new StreamWriter(PATH);
            serializer.Serialize(sw, Items);
        }

        public void LoadFromXML()
        {
            if (File.Exists(PATH))
            {
                string xml = File.ReadAllText(PATH);

                StringReader sr = new StringReader(xml);

                XmlRootAttribute root = new XmlRootAttribute();
                root.ElementName = "recipes";
                root.IsNullable = true;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>), root);

                var xmlItems = (List<Recipe>)serializer.Deserialize(sr);

                Items = new List<Recipe>(xmlItems);
            }
        }
    }
}
