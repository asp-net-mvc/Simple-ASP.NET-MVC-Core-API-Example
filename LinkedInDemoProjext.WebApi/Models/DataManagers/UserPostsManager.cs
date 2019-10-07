using LinkedInDemoProjext.WebApi.Models.Contexts;
using LinkedInDemoProjext.WebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInDemoProjext.WebApi.Models.DataManagers
{
    public class UserPostsManager : IDataRepository<UserPosts>
    {
        private readonly UserPostsContext _userPostsContext;
        public UserPostsManager(UserPostsContext userPostsContext)
        {
            _userPostsContext = userPostsContext;
        }
        public void Add(UserPosts entity)
        {
            _userPostsContext.UserPosts.Add(entity);
            _userPostsContext.SaveChanges();
        }

        public void Delete(UserPosts entity)
        {
            _userPostsContext.UserPosts.Remove(entity);
            _userPostsContext.SaveChanges();
        }

        public UserPosts Get(string id)
        {
            return _userPostsContext.UserPosts.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<UserPosts> GetAll(string refUserId)
        {
            return _userPostsContext.UserPosts.Where(p => p.refUserId == refUserId).ToList();
        }

        public void Update(UserPosts dbEntity, UserPosts entity)
        {
            dbEntity.Content = entity.Content;

            _userPostsContext.SaveChanges();
        }
    }
}
