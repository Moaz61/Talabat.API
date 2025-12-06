using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Infrastructure.Generic_Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;  //_dbContext.Set<Product>()

            if (spec.Criteria is not null)  // P => P.Id = 1
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)  // P => P.Name
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)  //P => P.Price
                query = query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            // query = _dbContext.Set<Product>().where(P => P.Id = 1)
            // Include Expressions
            // 1. P => P.Brand
            // 2. P => P.Category

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            //                    currentQuery                          includeExpression
            // query = _dbContext.Set<Product>().where(P => P.Id = 1).Include(P => P.Brand)
            // query = _dbContext.Set<Product>().where(P => P.Id = 1).Include(P => P.Brand).Include(P => P.Category)

            return query;
        }
    }
}
