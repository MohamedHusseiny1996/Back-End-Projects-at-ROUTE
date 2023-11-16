using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class SpecificationBase<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get ; set ; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>>? OrderBy { get; set; }
		public Expression<Func<T, object>>? OrderByDesc { get; set; }
		public int Take { get; set; }
		public int Skip { get; set; }
		public bool IsPaginationEnabled { get; set; }


		public SpecificationBase()
        {
			Includes = new List<Expression<Func<T, object>>>();
			
        }

		public SpecificationBase(Expression<Func<T, bool>> Criteria)
		{
			this.Criteria = Criteria;
			Includes = new List<Expression<Func<T, object>>>();
		}

	}
}
