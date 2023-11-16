using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteDAL.Contexts;
using AmazonWebSiteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Repositories
{
    public class OrdersRepository:GenericRepository<Order>,IOrdersRepository
    {
        public OrdersRepository(AmazonContext context):base(context)
        {
            
        }
    }
}
