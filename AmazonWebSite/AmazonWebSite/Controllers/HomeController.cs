using AmazonWebSite.Models;
using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteDAL.Entities;
using AmazonWebSitePL.Helper;
using AmazonWebSitePL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Reflection.Metadata;



//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AmazonWebSite.Controllers
{
    
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public IUnitOfWork _UnitOfWork { get; set; }
        public IMapper _Mapper { get; }

        // public string _dir { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork,IMapper mapper/*,IHostingEnvironment hosting*/)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            //_dir = hosting.ContentRootPath;
        }
        
        public IActionResult Index(string Category="",string Name="")
        {
            IEnumerable<ItemViewModel> ItemViewModel;
            IEnumerable<Items> items;
            ViewBag.categories = _UnitOfWork._itemssRepository.GetAllCategories();

            if (string.IsNullOrEmpty(Category)&& string.IsNullOrEmpty(Name))
            {

                 items = _UnitOfWork._itemssRepository.GetAll();
                ItemViewModel=_Mapper.Map<IEnumerable<ItemViewModel>>(items);
               
                
                return View(ItemViewModel);
            }
            if(string.IsNullOrEmpty(Category) && Name is not null)
            {
                items = _UnitOfWork._itemssRepository.SearchByItemName(Name);
                ItemViewModel = _Mapper.Map<IEnumerable<ItemViewModel>>(items);
                
                return View(ItemViewModel);

            }
            if (string.IsNullOrEmpty(Name) && Category is not null)
            {
                 items = _UnitOfWork._itemssRepository.SearchByItemCategory(Category);
                ItemViewModel = _Mapper.Map<IEnumerable<ItemViewModel>>(items);
               
                return View(ItemViewModel);

            }

             items = _UnitOfWork._itemssRepository.SearchByItemCategoryAndName(Category,Name);
            ItemViewModel = _Mapper.Map<IEnumerable<ItemViewModel>>(items);
            
            return View(ItemViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            
            
            return View(new ItemViewModel());
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Create(ItemViewModel itemViewModel)
        {
            if(ModelState.IsValid )//edited*********************
            {

                //using(var FileStream= new FileStream(Path.Combine(_dir,$"wwwroot/images/{item.Name}.jpg"),FileMode.Create, FileAccess.Write))
                //{
                //    item.image.Path = $"/images/{item.Name}.jpg";
                //    _UnitOfWork._itemssRepository.Add(item);
                //    item.image.ItemImage.CopyTo(FileStream);


                //}
                var file = itemViewModel.image.ItemImage;
                var FilePath=DocumentSetting.UploadFile(file, "images");//according to view
                itemViewModel.image.Path = FilePath;
               var Items = _Mapper.Map<Items>(itemViewModel);
                _UnitOfWork._itemssRepository.Add(Items);
                return RedirectToAction("Index","Home");

            }
            
            return View(itemViewModel);
        }

        public IActionResult Details(int id)
        {
           var item= _UnitOfWork._itemssRepository.GetById(id);
            var itemViewModel =_Mapper.Map<ItemViewModel>(item);
            return View(itemViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var item = _UnitOfWork._itemssRepository.GetById(id);
            var itemViewModel = _Mapper.Map<ItemViewModel>(item);
            return View(itemViewModel);
            
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Delete(ItemViewModel itemViewModel)
        {
            //var OldFile = _UnitOfWork._itemssRepository.GetByIdNoTracking(item.Id);
            // _UnitOfWork._imagesRepository.Deletefile($"wwwroot/images/{item.Name}.jpg");
            DocumentSetting.DeleteFile(itemViewModel.image.Path);

            //using (var FileStream = new FileStream(Path.Combine(_dir, $"wwwroot/images/{OldFile.Name}.jpg"), FileMode.Truncate, FileAccess.Write))
            //{

            //}
            var item = _Mapper.Map<Items>(itemViewModel);
            _UnitOfWork._itemssRepository.Delete(item);
            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Update(int id)
        {
           
            var item = _UnitOfWork._itemssRepository.GetById(id);

            // TempData["image id"] = item.image.Id;
            var itemViewModel = _Mapper.Map<ItemViewModel>(item);
            return View(itemViewModel);
        }

        [HttpPost,Authorize(Roles ="Admin")]
        public IActionResult Update(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                //var OldFile =_UnitOfWork._itemssRepository.GetByIdNoTracking(item.Id);
                //using (var FileStream = new FileStream(Path.Combine(_dir, $"wwwroot/images/{OldFile.Name}.jpg"), FileMode.Truncate, FileAccess.Write))
                //{

                //}

                ////_UnitOfWork._imagesRepository.Deletefile($"wwwroot/images/{item.Name}.jpg");
                
                ////using (var FileStream = new FileStream(Path.Combine(_dir, $"wwwroot/images/{item.Name}.jpg"), FileMode.Create, FileAccess.Write))
                ////{
                ////    item.image.Path = $"/images/{item.Name}.jpg";
                ////   // item.image.ItemID = item.Id;
                ////   // item.image.Id =(int) TempData["image id"];
                ////    _UnitOfWork._itemssRepository.Update(item);  
                ////    item.image.ItemImage.CopyTo(FileStream);
                    

                ////}
                
                var filepath= itemViewModel.image.Path;
                var file= itemViewModel.image.ItemImage;
                DocumentSetting.DeleteFile(filepath);
                itemViewModel.image.Path = DocumentSetting.UploadFile(file, "images");
                var item = _Mapper.Map<Items>(itemViewModel);
                _UnitOfWork._itemssRepository.Update(item);
                return RedirectToAction("Index", "Home");

            }

            return View(itemViewModel);
        }

        [Authorize]
        public IActionResult Order(int id)
        {
            var item = _UnitOfWork._itemssRepository.GetById(id);
             
            //TempData["item"] = _Mapper.Map<ItemViewModel>(item);
            ViewBag.ItemViewModell = _Mapper.Map<ItemViewModel>(item);
            return View(new OrdersViewModel());
        }

        [HttpPost,Authorize]
        public IActionResult Order(OrdersViewModel ordersViewModel)
        {
            var item = _UnitOfWork._itemssRepository.GetById(ordersViewModel.ItemID);
            ViewBag.ItemViewModell = _Mapper.Map<ItemViewModel>(item);
           
            if (ModelState.IsValid)//handle if count is greater than in stock*******
            {
                var order= _Mapper.Map<Order>(ordersViewModel);
                _UnitOfWork._iOrdersRepository.Add(order);
               return RedirectToAction("Index", "Home");
            }
            return View(ordersViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}