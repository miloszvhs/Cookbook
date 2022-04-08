using Cookbook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Domain.Entity
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public Ingredient(int id, string name, int amount, string unit)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Unit = unit;
        }
    }
}
