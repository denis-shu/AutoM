using Logs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Logs.Business
{
    public class UserDbActions
    {
        CXZAdminContext _context;

        public UserDbActions()
        {
            _context = new CXZAdminContext();
        }

        #region Get Users

        public async Task<UserViewModel> GetUsers(int page = 1)
        {
            var pageSize = 10;

            IEnumerable<User> users = await _context.Users
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Users.CountAsync();

            var pager = new Pager(totalItems, page);

            var viewModel = new UserViewModel
            {
                Users = users,
                Pager = pager
            };

            return viewModel;

        }

        #endregion


        #region Get Users Details

        public UserDetail GetUserDetails(int id)
        {
            // UserDetailModel uDm = new UserDetailModel();

            var userDetail = _context.UserDetails
                .FirstOrDefault(x => x.UserId == id);

            return userDetail;
        }

        #endregion


        #region Get LOggins

        public string[] GetLoggins(int id)
        {
            return _context.UserLogins
                .Where(x => x.User_Id == id)
                .Select(s => s.Logins)
                .ToArray();
        }


        #endregion


        #region Search User

        public User SearchUser(string login)
        {
            //return _context.Users.Select(s => new
            //{
            //    id = s.Id,
            //    auth = s.IsAuthenticated,
            //    login = s.Login
            //}).AsEnumerable().Select(s => new User
            //{
            //    Id = s.id,
            //    Login = s.login,
            //    IsAuthenticated = s.auth
            //})
            //    .FirstOrDefault(u => u.Login == login);
            return _context.Users
                .FirstOrDefault(u => u.Login == login);
        }

        #endregion


        #region Authenticated 
        //Change  isAuthentication property
        public bool Authenticated(string id)
        {
            int ids = Convert.ToInt32(id);
            bool newAuth = false;

            var user = _context.Users
                .FirstOrDefault(u => u.Id == ids);

            if (user != null)
            {
                user.IsAuthenticated = user.IsAuthenticated ? false : true;

                newAuth = user.IsAuthenticated;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return newAuth;
        }
        #endregion


        #region Delete User

        public bool DeleteUser(int id)
        {
            bool exist = _context
                .Users.Count(x => x.Id == id) > 0;

            if (!exist)
                return false;

            var fakeUser = new User() { Id = id };

            _context.Users.Attach(fakeUser);
            _context.Users.Remove(fakeUser);

            _context.SaveChanges();

            return true;
        }
        #endregion

    }
}
