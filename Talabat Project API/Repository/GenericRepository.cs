using Core.Entities;
using Core.Repositories;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		public StoreContext _Context { get; set; }

		public GenericRepository(StoreContext context)
        {
			_Context = context;
		}

		

		public async Task<IReadOnlyList<T>> GetAllAsyn()
		{
			var items =await _Context.Set<T>().ToListAsync();
			return items;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			
			var item = await _Context.Set<T>().FindAsync(id);
	
			return item;
		}

		

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			//var baseQuery = _Context.Set<T>();
		 // return await	SpecificationEvaluator<T>.GetQuery(baseQuery, spec).ToListAsync();

			return await ApplySpec(spec).ToListAsync();
		}

		public async Task<T> GetEntityWithSpecAsync( ISpecification<T> spec)
		{
			//var baseQuery = _Context.Set<T>();
			//return await SpecificationEvaluator<T>.GetQuery(baseQuery, spec).FirstOrDefaultAsync();
			return await ApplySpec(spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpec(spec).CountAsync();
		}

		private  IQueryable<T> ApplySpec(ISpecification<T> spec)
		{
			var baseQuery = _Context.Set<T>();
			return  SpecificationEvaluator<T>.GetQuery(baseQuery, spec);
		}

		public async Task AddAsync(T entity)
		{
			await _Context.Set<T>().AddAsync(entity);
		}

		public  void Delete(T entity)
		{
			 _Context.Set<T>().Remove(entity);
		}

		
		public void Update(T entity)
		{
			_Context.Set<T>().Update(entity);
		}
	}
}
