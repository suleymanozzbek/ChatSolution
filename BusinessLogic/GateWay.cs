using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;

namespace BusinessLogic
{
    
    public class GateWay
    {
        dbc _context = new dbc();


        public MvcUsers Find(int id)
        {
            return _context.MvcUsers.Where(x => x.pkID == id).FirstOrDefault();
        }
        public List<MvcUsers> UserList()
        {
            return _context.MvcUsers.ToList();
        }
        public void insertUser(MvcUsers user)
        {
          
            _context.MvcUsers.Add(user);
            _context.SaveChanges();

        }
        public void updateUser(MvcUsers user)
        {

           
            _context.SaveChanges();

        }
        public void deleteUser(int userId)
        {
          
            var user= _context.MvcUsers.Where(x => x.pkID == userId).FirstOrDefault();
            _context.MvcUsers.Remove(user);
            _context.SaveChanges();
        }
        public static int messageSender(string _mesaj, int _msgTo, int _msgBy)
        {
            if (_mesaj.Trim().Length == 0)
                return -1;
            var ctx = new dbc();
            MvcMsg msg = new MvcMsg();
            msg.Msg = _mesaj;
            msg.MsgBy = _msgBy;
            msg.MsgTo = _msgTo;
            msg.MsgDate = DateTime.Now;
            ctx.MvcMsg.Add(msg);
            return ctx.SaveChanges();
        }
       
        public static string[,] getUsers(int userId)
        {
            var ctx = new dbc();

            var mvcUsers = (from m in ctx.MvcUsers
                            where m.pkID != (userId != 0 ? userId : m.pkID)
                            select new { m.pkID, m.UserName }).ToList();
            string[,] users = new string[mvcUsers.Count, 2]
             ;
            for (int i = 0; i < mvcUsers.Count; i++)
            {
                users[i, 0] = mvcUsers[i].pkID.ToString();
                users[i, 1] = mvcUsers[i].UserName;
            }
            if (users != null)
                return users;
            else
                return null;

        }

        public static int userLogin(MvcUsers newUser)
        {
            if (newUser.UserName.Trim().Length == 0)
                return -1;
            if (newUser.Password.Trim().Length == 0)
                return -1;
            var ctx = new dbc();

            var userInfo =
               (from m in ctx.MvcUsers
                where m.UserName == newUser.UserName && m.Password == newUser.Password
                select new { m.pkID, m.UserName }).FirstOrDefault();
            if (userInfo != null)
                return userInfo.pkID;
            else
                return -2;
        }

        private static bool isPasswordValid(string _password)
        {
            // you can create your own controls.
            if (_password.Trim().Length > 2)
                return true;
            else
                return false;
        }
        

    }
}
