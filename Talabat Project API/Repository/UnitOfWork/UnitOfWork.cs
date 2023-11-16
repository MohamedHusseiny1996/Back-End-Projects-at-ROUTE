using Core.Entities;
using Core.Repositories;
using Core.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
	public class UnitOfWork : IunitOfWork
	{
		private readonly StoreContext _context;
		private readonly Hashtable _repository = new Hashtable(); // we used hash tables but not dictionaries because dictionaries
		// need type of keys and values but here i use generic key so i don't know it's type so i used hash tables
		// because they accept objects of what ever type .
		// hash tables are like dictionaries they have key and value but keys arn't unique and it accepts objects
		// with any type [not type safe]
		
		public UnitOfWork(StoreContext context)
        {
			_context = context;
		}


        public IGenericRepository<Entity>? Repository<Entity>() where Entity : BaseEntity
		{
			if (!_repository.ContainsKey(typeof(Entity).Name))
			{
				_repository[typeof(Entity).Name] = new GenericRepository<Entity>(_context);
			}

			return _repository[typeof(Entity).Name] as GenericRepository<Entity>; // we need to cast the hash table
			// value because it is saved as object type 
			
		}


		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}


		public async ValueTask DisposeAsync()
		{
			await _context.DisposeAsync();
		}
	}
}
