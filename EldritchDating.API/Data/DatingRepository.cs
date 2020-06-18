using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldritchDating.API.Helpers;
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

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = context.Users.Include(p => p.Photos)
                .OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.ID != userParams.UserId);

            if (userParams?.Devotion != null)
                users = users.Where(u => u.Devotion == userParams.Devotion);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.ID));
            } 
            if (userParams.Likees) 
            {
                var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.ID));
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {   
                switch (userParams.OrderBy) {
                    case "created": 
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default: 
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhotoAsync(int photoId) {
            var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);

            return photo;
        }

        public async Task<Photo> GetMainPhotoForUserAsync(int userId)
        {
            return await context.Photos.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await context.Likes.FirstOrDefaultAsync(u => 
                u.LikerId == userId && u.LikeeId == recipientId);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int userId, bool likers) 
        {
            var user = await context.Users.Include(x => x.Likers).Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.ID == userId);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == userId).Select(l => l.LikerId);
            }
            else 
            {
                return user.Likees.Where(u => u.LikerId == userId).Select(l => l.LikeeId);
            }
        }

        public async Task<Message> GetMessage(int id)
        {
            return await context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.IsRead == false);
                    break;

            }

            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.RecipientId == userId && m.SenderId == recipientId
                    || m.RecipientId == recipientId && m.SenderId == userId)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}