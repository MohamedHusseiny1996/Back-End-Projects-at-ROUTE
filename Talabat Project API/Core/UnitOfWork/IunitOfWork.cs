using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
	public interface IunitOfWork:IAsyncDisposable
	{
		IGenericRepository<Entity>? Repository<Entity>() where Entity : BaseEntity;
		Task<int> CompleteAsync();
	}
}
