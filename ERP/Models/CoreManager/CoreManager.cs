using ERP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Models.CoreManager
{
    public class CoreManager
    {
        ErpDB db = new ErpDB();
        public bool IsUserInRole(string loginName, string roleName)
        {
            Usr SU = db.Usr.Where(o => o.UserName.ToLower().Equals(loginName))?.FirstOrDefault();
            if (SU != null)
            {
                var roles = from q in db.UserRole
                            join r in db.Role on q.RoleID equals r.ID
                            where r.RoleName.Equals(roleName) && q.UserID.Equals(SU.ID)
                                  && r.IsActive == true
                                  && r.ValidFrom <= System.DateTime.Now
                            select r.RoleName;

                if (roles != null)
                {
                    return roles.Any();
                }
            }
            return false;
        }
        public bool IsLoginNameExist(string loginName)
        {
            return db.Usr.Where(o => o.UserName.Equals(loginName)
                                     && o.IsActive == true
                                     && o.ValidFrom <= System.DateTime.Now).Any();
        }
        public bool IsLoginName(string loginName, int id)
        {
            return db.Usr.Where(o => o.UserName.Equals(loginName) && o.UserID != id
                                     && o.IsActive == true
                                     && o.ValidFrom <= System.DateTime.Now).Any();
        }
        public string GUP(string lname)
        {
            var user = db.Usr.Where(o => o.UserName.ToLower().Equals(lname)
                                          && o.IsActive == true
                                          && o.ValidFrom <= System.DateTime.Now).Select(o => o.UserKey).FirstOrDefault();
            if (user != null)
                return user;
            else
                return string.Empty;
        }
        public void adduser(userinfo eh)
        {
            var uid = db.Usr.Max(v => v.ID) + 1;
            Usr ei = new Usr();
            Secur sr = new Secur();
            UserRole ur = new UserRole();
            ei.UserID = uid;
            ei.EmpID = eh.EmpID;
            var emp = db.EmpInfo.Where(x => x.ID == eh.EmpID).FirstOrDefault();
            emp.Usr = eh.UserName.Trim();
            ei.UserName = eh.UserName.Trim();
            ei.UserKey = sr.Encrypt(eh.UserKey.Trim());
            ei.EntryKey = "000";
            ei.Role = eh.Role;
            ei.Email = eh.Email;
            ei.Telephone = eh.Telephone;
            ei.IsActive = eh.IsActive;
            ei.ValidFrom = eh.ValidFrom;
            ei.CreatedBy = eh.Createby;
            ei.CreatedDate = DateTime.Today;
            ei.CreatedTime = DateTime.Now.TimeOfDay;
            ur.UserID = uid;
            ur.RoleID = eh.Role;
            ur.IsActive = eh.IsActive;
            ur.ValidFrom = DateTime.Today;
            ur.CreatedBy = eh.Createby;
            ur.CreatedDate = DateTime.Today;
            ur.CreatedTime = DateTime.Now.TimeOfDay;
            db.Usr.Add(ei);
            db.SaveChanges();
            db.UserRole.Add(ur);
            db.SaveChanges();
        }
    }
}