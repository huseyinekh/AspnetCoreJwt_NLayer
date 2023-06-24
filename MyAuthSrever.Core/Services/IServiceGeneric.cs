using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthSrever.Core.Services
{
    public interface IServiceGeneric<TEntity,TDto> where TEntity : class where TDto: class
    {
        Task<ResponseDto<TDto>> GetByIdAsync(int Id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync(int Id);
        Task<ResponseDto<TDto>> AddAsync(TEntity entity);
        Task<ResponseDto<NoDataDto>> Remove(TEntity entity);
        Task<ResponseDto<NoDataDto>> Update(TEntity entity);
        IEnumerable<ResponseDto<TDto>> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
