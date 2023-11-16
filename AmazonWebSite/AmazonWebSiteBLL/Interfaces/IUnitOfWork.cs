using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IImagesRepository _imagesRepository { get; set; }
        public IItemsRepository _itemssRepository { get; set; }
        public IOrdersRepository _iOrdersRepository { get; set; }
    }
}
