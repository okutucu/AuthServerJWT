using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Service
{
    public interface IServiceGeneric<TEntity,TDto> where TEntity : class where TDto: class
    {
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsyn();
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseDto<TDto>> AddAsync(TDto entity);
        Task<ResponseDto<NoDataDto>> Remove(int id);
        Task<ResponseDto<NoDataDto>> Update(TDto entity, int id);
    }
}
