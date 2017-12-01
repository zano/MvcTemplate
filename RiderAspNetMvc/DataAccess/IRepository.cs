using System;
using System.Linq;
using System.Threading.Tasks;
using RiderAspNetMvc.Services;

namespace RiderAspNetMvc.DataAccess {
    public interface IRepository<T> : IDisposable {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        void Insert(T t);
        void Update(T t);
        void Delete(T t);

        Result Save();
        Task<Result> SaveAsync();
    }
}

