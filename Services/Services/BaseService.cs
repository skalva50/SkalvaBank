using SkalvaBank.Dal;
using SkalvaBank.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkalvaBank.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        protected readonly IAsyncRepository<T> _repositoryAsync;
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository, IAsyncRepository<T> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
        }

        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public Task<T> AddAsync(T entity)
        {
            return _repositoryAsync.AddAsync(entity);
        }

        public int Count(ISpecification<T> spec)
        {
            return _repository.Count(spec);
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            return _repositoryAsync.CountAsync(spec);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public Task DeleteAsync(T entity)
        {
            return _repositoryAsync.DeleteAsync(entity);
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return _repositoryAsync.GetByIdAsync(id);
        }
        
        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return _repository.GetSingleBySpec(spec);
        }

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            return _repository.List(spec);
        }

        public IEnumerable<T> ListAll()
        {
            return _repository.ListAll();
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            return _repositoryAsync.ListAllAsync();
        }

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return _repositoryAsync.ListAsync(spec);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public Task UpdateAsync(T entity)
        {
            return _repositoryAsync.UpdateAsync(entity);
        }
    }

}