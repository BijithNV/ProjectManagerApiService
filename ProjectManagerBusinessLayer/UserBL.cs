using Microsoft.EntityFrameworkCore;
using ProjectManagerDAL;
using ProjectManagerEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerBusinessLayer
{
    public class UserBL : IProjectMangerBL<User>
    {
        private readonly IProjectManagerContext _context;

        public UserBL(IProjectManagerContext context)
        {
            _context = context;
        }

        public int CreateNew(User objDetails)
        {
            _context.Users.Add(objDetails);
            _context.SaveChanges();
            return objDetails.UserId;
        }

        public void Delete(int Id)
        {
            var user = _context.Users.SingleOrDefault(row => row.UserId == Id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> RetrieveAllData()
        {
            return _context.Users.ToList();
        }

        public User GetById(int userId)
        {
            var projectdetails = _context.Users.FirstOrDefault(row => row.UserId == userId);
            return projectdetails;
        }

        public IEnumerable<User> SearchByKey(string searchText)
        {
            return _context.Users
                .Where(row =>
                        string.IsNullOrEmpty(searchText)
                        || (row.FirstName.Contains(searchText)
                        || row.LastName.Contains(searchText)
                        || row.EmployeeId.Contains(searchText)))
                .ToList();
        }

        public User Update(User objDetails)
        {
            _context.Users.Update(objDetails);
            _context.SaveChanges();
            return objDetails;
        }

        //public IEnumerable<User> RetriveAllUsers()
        //{
        //    return _context.Users.ToList();
        //}

        //public IEnumerable<User> SearchUsers(string searchText)
        //{
        //    return _context.Users
        //        .Where(row =>
        //                string.IsNullOrEmpty(searchText)
        //                || (row.FirstName.Contains(searchText)
        //                || row.LastName.Contains(searchText)
        //                || row.EmployeeId.Contains(searchText)))
        //        .ToList();
        //}

        //public int AddUser(User userDetails)
        //{
        //    _context.Users.Add(userDetails);
        //    _context.SaveChanges();
        //    return userDetails.UserId;
        //}

        //public User UpdateUser(User userDetails)
        //{
        //    _context.Users.Update(userDetails);
        //    _context.SaveChanges();
        //    return userDetails;
        //}

        //public void DeleteUser(int userId)
        //{
        //    var user = _context.Users.SingleOrDefault(row => row.UserId == userId);
        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //}
    }
}
