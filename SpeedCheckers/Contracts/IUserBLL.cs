using System;

namespace SpeedCheckers.Contracts
{
	public interface IUserBLL
	{
		Guid CreateUser(string name);
	}
}