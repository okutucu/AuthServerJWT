using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Repositories;
using AuthServer.Core.Service;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;

namespace AuthServer.Service.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            TEntity newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsyn()
        {
            List<TDto> products = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsyn());

            return ResponseDto<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            TEntity product = await _repository.GetByIdAsync(id);

            if(product ==null)
            {
                return ResponseDto<TDto>.Fail("Id not found", 404,true);
            }

            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<ResponseDto<NoDataDto>> Remove(int id)
        {
            TEntity isExistEntity = await _repository.GetByIdAsync(id);

            if(isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found",404,true);
            }

            _repository.Remove(isExistEntity);

            await _unitOfWork.CommitAsync();

            return ResponseDto<NoDataDto>.Success(204);
        }

        public async Task<ResponseDto<NoDataDto>> Update(TDto entity,int id)
        {
            TEntity isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
            }

            TEntity updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            _repository.Update(updateEntity);
            await _unitOfWork.CommitAsync();

            return ResponseDto<NoDataDto>.Success(204);

        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> list = _repository.Where(predicate);

            return ResponseDto<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),200);

        }
    }
}
