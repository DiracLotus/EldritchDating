using System.Collections.Generic;
using System.Threading.Tasks;
using EldritchDating.API.Helpers;
using EldritchDating.API.Models;

namespace EldritchDating.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhotoAsync(int photoId);
         Task<Photo> GetMainPhotoForUserAsync(int userId);
    }
}