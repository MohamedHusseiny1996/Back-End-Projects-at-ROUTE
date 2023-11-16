using AmazonWebSiteBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        public IImagesRepository _imagesRepository { get ; set ; }
        public IItemsRepository _itemssRepository { get ; set ; }
        public IOrdersRepository _iOrdersRepository { get; set; }
        public UnitOfWorkRepository(IImagesRepository imagesRepository ,IItemsRepository itemsRepository,IOrdersRepository ordersRepository)
        {
            _imagesRepository = imagesRepository ;
            _itemssRepository = itemsRepository ;   
            _iOrdersRepository = ordersRepository ;
        }
    }
}
