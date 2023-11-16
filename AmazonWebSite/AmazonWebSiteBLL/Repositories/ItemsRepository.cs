using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteDAL.Contexts;
using AmazonWebSiteDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Repositories
{
    public class ItemsRepository : GenericRepository<Items>, IItemsRepository
    {
        
        public ItemsRepository(AmazonContext context):base(context) 
        {
           
        }


        

        public Items GetByIdNoTracking(int id)
        {
            var items = _Context.Items.AsNoTracking().Where(x=>x.Id==id).FirstOrDefault();
            
            return items;
        }

        public IEnumerable<Items> SearchByItemCategory(string SearchValue)
        {
            var items = _Context.Items.Include(x => x.image)
                .Where(x => x.Category.Trim().ToLower().Contains( SearchValue.Trim().ToLower()));
            return items;
        }

        public IEnumerable<Items> SearchByItemName(string SearchValue)
        {
            var items = _Context.Items.Include(x=>x.image)
                .Where(x => x.Name.Trim().ToLower().Contains(SearchValue.Trim().ToLower()));
            return items;
        }

        //public int Update(Items Entity)
        //{
        //    var OldEntity = _Context.Items.Where(x => x.Id == Entity.Id).Single();
        //    _Context.Items.Remove(OldEntity);
        //    _Context.SaveChanges();
        //    Entity.Id = default;
        //    _Context.Items.Add(Entity);

        //    return _Context.SaveChanges();
        //}

        public IEnumerable<Items> SearchByItemCategoryAndName(string Category, string Name)
        {
            var items = _Context.Items.Include(x => x.image)
               .Where(x => x.Name.Trim().ToLower().Contains(Name.Trim().ToLower()))
               .Where(x => x.Category.Trim().ToLower().Contains( Category.Trim().ToLower()));
            return items;
        }

        public IEnumerable<string> GetAllCategories()
        {
            return GetAll().Select(x => x.Category).Distinct().ToList();
        }
    }
}
