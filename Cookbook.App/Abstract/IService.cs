using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }
        List<T> GetAllItems();
        int AddItem(T item);
        //int UpdateItem(T item);
        bool RemoveItem(T item);
        int GetLastId();
        T GetItemById(int? id);
        T GetItemById(int id);
    }
}
