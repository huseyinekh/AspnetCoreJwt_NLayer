﻿using SharedLibrary.Dtos;
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
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<ResponseDto<TDto>> AddAsync(TDto entity);
        Task<ResponseDto<NoDataDto>> Remove(int id);
        Task<ResponseDto<NoDataDto>> Update(TDto entity, int id);
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
