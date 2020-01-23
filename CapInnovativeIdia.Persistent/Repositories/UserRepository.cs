using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapInnovativeIdia.Persistent.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CapInnovativeIdiaDbContext capInnovativeIdiaDbContext) : base(capInnovativeIdiaDbContext)
        {

        }
        public void CreateUser(CreateUserViewModel createUserViewModel, int userId)
        {
            createUserViewModel.User.CreatedDate = DateTime.Now;
            createUserViewModel.User.CreatedBy = userId;
            createUserViewModel.User.IsVarified = 0;
            createUserViewModel.User.IsActive = 1;

            CapInnovativeIdiaDbContext.User.Add(createUserViewModel.User);
            CapInnovativeIdiaDbContext.SaveChanges();

            foreach (var role in createUserViewModel.RoleIds)
            {
                var assignRole = new UserRoleMapping
                {
                    RoleId = role,
                    UserId = createUserViewModel.User.Id,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    IsActive = 1
                };

                CapInnovativeIdiaDbContext.UserRoleMapping.Add(assignRole);
            }

            CapInnovativeIdiaDbContext.SaveChanges();
        }

        public void UpdateUser(CreateUserViewModel createUserViewModel, int userId)
        {
            var user = CapInnovativeIdiaDbContext.User.Where(x=>x.Id==createUserViewModel.User.Id).SingleOrDefault();

            if (user != null)
            {
                user.FirstName = createUserViewModel.User.FirstName;
                user.LastName = createUserViewModel.User.LastName;
                user.Email = createUserViewModel.User.Email;
                user.Gender = createUserViewModel.User.Gender;
                user.ModifiedBy = userId;
                user.ModifiedDate = DateTime.Now;

                CapInnovativeIdiaDbContext.User.Update(user);
                CapInnovativeIdiaDbContext.SaveChanges();

                foreach (var role in createUserViewModel.RoleIds)
                {
                    var currentRole = CapInnovativeIdiaDbContext.UserRoleMapping.Where(x => x.RoleId == role && x.UserId == createUserViewModel.User.Id).SingleOrDefault();
                    if (currentRole != null)
                    {
                        if (currentRole.IsActive == 1)
                        {
                            continue;
                        }

                        else
                        {
                            currentRole.IsActive = 1;
                            currentRole.ModifiedBy = userId;
                            currentRole.ModifiedDate = DateTime.Now;

                            CapInnovativeIdiaDbContext.UserRoleMapping.Update(currentRole);
                            CapInnovativeIdiaDbContext.SaveChanges();
                        }
                    }

                    var newRole = new UserRoleMapping
                    {

                        RoleId = role,
                        UserId = createUserViewModel.User.Id,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        IsActive = 1
                    };

                    CapInnovativeIdiaDbContext.UserRoleMapping.Add(newRole);
                    CapInnovativeIdiaDbContext.SaveChanges();
                }

                var removeRoles = CapInnovativeIdiaDbContext.UserRoleMapping.Where(x => x.UserId == createUserViewModel.User.Id && !createUserViewModel.RoleIds.Contains(x.RoleId)).ToList();

                if (removeRoles != null)
                {
                    removeRoles.ForEach(x => x.IsActive = 0);
                    CapInnovativeIdiaDbContext.SaveChanges();
                }
            }
        }

        public void DeleteUser(int id)
        {
            var userDetails=CapInnovativeIdiaDbContext.User.Where(u => u.Id == id).SingleOrDefault();

            if (userDetails != null)
            {
                CapInnovativeIdiaDbContext.User.Remove(userDetails);
                CapInnovativeIdiaDbContext.SaveChanges();

                var userRoles = CapInnovativeIdiaDbContext.UserRoleMapping.Where(r => r.UserId == id).ToList();
                CapInnovativeIdiaDbContext.UserRoleMapping.RemoveRange(userRoles);
                CapInnovativeIdiaDbContext.SaveChanges();
            }
        }
        public CapInnovativeIdiaDbContext CapInnovativeIdiaDbContext
        {
            get { return Context as CapInnovativeIdiaDbContext; }
        }
    }
}
