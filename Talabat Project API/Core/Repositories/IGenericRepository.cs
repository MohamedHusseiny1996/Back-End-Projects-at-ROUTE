using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public  Task<IReadOnlyList<T>> GetAllAsyn();// async cannot be used with function without body
		public  Task<T> GetByIdAsync(int id);// async cannot be used with function without body
		public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
		public Task<T> GetEntityWithSpecAsync( ISpecification<T> spec);
		public Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
		public Task AddAsync(T entity);
		public void Delete(T entity);
		public void Update(T entity);

	}
}
