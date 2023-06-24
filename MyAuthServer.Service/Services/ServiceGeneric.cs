using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MyAuthServer.Core.Repositories;
using MyAuthSrever.Core.Services;
using MyAuthSrever.Core.UnitOfWork;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServer.Service.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepostiory;

         public ServiceGeneric(IUnitOfWork unitOfWork,IGenericRepository<TEntity> gr)
        {
            _unitOfWork = unitOfWork;
            _genericRepostiory = gr;

        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            var newEntity =ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepostiory.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newDto =ObjectMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto, 200);

        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper
                .Map<List<TDto>>(await _genericRepostiory.GetAllAsync());
            return ResponseDto<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int Id)
        {
            var product=ObjectMapper.Mapper.Map<TDto>(await _genericRepostiory.GetByIdAsync(Id));
            if(product == null)
            {
                return ResponseDto<TDto>.Fail("Id not found",404);
            }
             return ResponseDto<TDto> .Success(product, 200);
        }

        public async Task<ResponseDto<NoDataDto>> Remove(int id)
        {
            var isExist = await _genericRepostiory.GetByIdAsync(id);
            if(isExist == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found",404);
            }
            _genericRepostiory.Remove(isExist);
          await  _unitOfWork.CommitAsync();

            return ResponseDto<NoDataDto>.Success( 204);   
        }

      

        public async Task<ResponseDto<NoDataDto>> Update(TDto entity, int id)
        {
            var isExist =await _genericRepostiory.GetByIdAsync(id);
            if (isExist == null)
            {
                return ResponseDto<NoDataDto>.Fail($"{typeof(TDto)} not found", 404);
            }
             _genericRepostiory.Update(isExist);

           await  _unitOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success( 204);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list =_genericRepostiory.Where(predicate);

            return ResponseDto<IEnumerable<TDto>>.
                Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),200);
        }
    }
}
