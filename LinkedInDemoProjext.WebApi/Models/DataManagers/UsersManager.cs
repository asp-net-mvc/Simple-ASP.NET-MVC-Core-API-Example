using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInDemoProjext.WebApi.Models;
using LinkedInDemoProjext.WebApi.Models.Contexts;
using LinkedInDemoProjext.WebApi.Models.Repositories;

namespace LinkedInDemoProjext.WebApi.Models.DataManagers
{
    public class UsersManager : IDataRepository<Users>
    {
        private readonly UsersContext _usersContext;
        public UsersManager(UsersContext context)
        {
            _usersContext = context;
        }
        public void Add(Users entity)
        {
            _usersContext.Users.Add(entity);
            _usersContext.SaveChanges();
        }

        public void Delete(Users entity)
        {
            _usersContext.Users.Remove(entity);
            _usersContext.SaveChanges();
        }

        public Users Get(string id)
        {
            return _usersContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Users> GetAll(string id = "")
        {
            return _usersContext.Users.ToList();
        }

        public void Update(Users dbEntity, Users entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Surname = entity.Surname;
            dbEntity.Gender = entity.Gender;
            dbEntity.Email = entity.Email;
            dbEntity.PasswordHash = entity.PasswordHash;
            dbEntity.Token = entity.Token;

            _usersContext.SaveChanges();
        }
    }
}
