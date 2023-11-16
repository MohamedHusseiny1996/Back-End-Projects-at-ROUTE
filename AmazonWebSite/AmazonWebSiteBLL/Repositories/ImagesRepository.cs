using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteDAL.Contexts;
using AmazonWebSiteDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteBLL.Repositories
{
    public class ImagesRepository : GenericRepository<Image>, IImagesRepository
    {
        
        public ImagesRepository(AmazonContext context):base(context)
        {
           
        }

        public void Deletefile(string path)
        {
            File.Delete(path);
        }
        

        

       
    }
}
