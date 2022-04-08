using Cookbook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public string Name { get; set; }
        public string MenuType { get; set; }

        public MenuAction(int id, string name, string menuName)
        {
            Id = id;
            Name = name;
            MenuType = menuName;
        }
    }
}
