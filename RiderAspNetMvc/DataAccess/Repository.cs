using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RiderAspNetMvc.Services;

namespace RiderAspNetMvc.DataAccess {
    public class Repository<T> : DbSet<T>, IRepository<T> where T : class {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(AppDbContext dbContext = null) {
            context = dbContext ?? AppDbContext.Create();
            dbSet = context.Set<T>();
        }

        public static implicit operator DbSet(Repository<T> r) => r.dbSet;
        public IQueryable<T> GetAll() => dbSet;

        public T GetById(int id) => dbSet.Find(id);
        public async Task<T> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Insert(T t) => dbSet.Add(t);
        public void Update(T t) => context.Entry(t).State = EntityState.Modified;
        public void Delete(T t) => dbSet.Remove(t);

        public Result Save() {
            try {
                context.SaveChanges();
                return Result.Success();
            } catch (Exception e) {
                return Result.Fail(e);
            }
        }

        public async Task<Result> SaveAsync() {
            try {
                await context.SaveChangesAsync();
                return Result.Success();
            } catch (Exception e) {
                return Result.Fail(e);
            }
        }

        public async void Dispose() {
            await SaveAsync();
            context.Dispose();
        }
    }
}