using System.Collections.Generic;
using System.Threading.Tasks;
using EldritchDating.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EldritchDating.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext context;

        public DatingRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.ID == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhotoAsync(int photoId) {
            var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);

            return photo;
        }
    }
}