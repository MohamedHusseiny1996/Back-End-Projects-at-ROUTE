using AmazonWebSiteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Interfaces
{
    public interface IItemsRepository:IGenericRepository<Items>
    {
        public IEnumerable<Items> SearchByItemName(string SearchValue);
        public IEnumerable<Items> SearchByItemCategory(string SearchValue);
        public IEnumerable<Items> SearchByItemCategoryAndName(string Category, string Name);
        public Items GetByIdNoTracking(int id);
        public IEnumerable<string> GetAllCategories();
    }
}
