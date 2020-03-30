using SpeedCheckers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.BLL
{// Creats the user with a unique id and adds him to the db
	public class UserBLL : IUserBLL
	{
		public Guid CreateUser(string name) {
			var id = Guid.NewGuid();
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				db.Users.Add(new Models.User
				{
					ID = id,
					Name = name
				});
				db.SaveChanges();
			}
			return id;
		}
	}
}