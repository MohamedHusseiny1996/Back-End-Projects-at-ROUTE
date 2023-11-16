using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> QueryBase , ISpecification<T> spec)
		{
			var query = QueryBase;
			
			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}

			

			if (spec.IsPaginationEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take);
			}

			if (spec.OrderBy != null)
			{
				query = query.OrderBy(spec.OrderBy);
			}

			if (spec.OrderByDesc != null)
			{
				query = query.OrderByDescending(spec.OrderByDesc);
			}
	

			query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

			
			return query;
		}
	}
}
