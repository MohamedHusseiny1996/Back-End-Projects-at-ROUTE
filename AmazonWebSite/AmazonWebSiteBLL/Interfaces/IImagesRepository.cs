using AmazonWebSiteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Interfaces
{
    public interface IImagesRepository:IGenericRepository<Image>
    {
        public void Deletefile(string path);
    }
}
