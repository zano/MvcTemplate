using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using RiderAspNetMvc.Models;
using RiderAspNetMvc.Services;

namespace RiderAspNetMvc.DataAccess {
    public class InMemoryRepository<T> : IRepository<T> where T : Student {
        private static readonly ConcurrentDictionary<int, T> dict = new ConcurrentDictionary<int, T>();        

        private static int IndexOf(T t) => dict.Keys.FirstOrDefault(k => dict[k] == t);
        private static T Remove(T t) => dict.TryRemove(IndexOf(t), out var val) ? val : null;

        public IQueryable<T> GetAll() => dict.Values.AsQueryable();
        public T GetById(int id) => dict.TryGetValue(id, out var t) ? t : null;
        public Task<T> GetByIdAsync(int id) => new Task<T>(() => GetById(id));
        public void Insert(T t) => dict.TryAdd(t.Id, t);
        public void Update(T t) => dict[t.Id] = Remove(t) ?? t;
        public void Delete(T t) => Remove(t);
        
        public Result Save() => Result.Success();
        public Task<Result> SaveAsync() => new Task<Result>(Result.Success);

        public void Dispose() { }
    }
}
