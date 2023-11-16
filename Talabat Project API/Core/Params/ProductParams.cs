namespace Talabat_Project_API.Helper
{
	public class ProductParams
	{
        
        const int MaxPageSize = 10;
		private int pageSize = 5;
		private string? search;
		public string? orderby { get; set; }
		public int? TypeId { get; set; }
		public int? BrandId { get; set; }
		public int PageSize { get { return pageSize; } set { pageSize = value > MaxPageSize ? MaxPageSize : value; } }  // if you write any thing inside set so you must write inside get to make full property
		public int PageIndex { get; set; } = 1; //default value
		

		public string? SearchValue
		{
			get { return search; }
			set { search = value?.ToLower().Trim(); }
		}

	}
}
