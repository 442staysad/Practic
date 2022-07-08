using System;
using System.Collections.Generic;
using System.Text;
using Onion.Core.Entities;

namespace Onion.Core.Interfaces
{
    public interface IEmployee
    {

        bool IsAuthenticatedLogin(string login, string password);

        // bool IsRegistered(string userName);

        int CurrentUserId(string login);
    }
}
