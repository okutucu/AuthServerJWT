using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Models;

namespace AuthServer.Core.Repositories
{
    public interface IUserAppRepository : IGenericRepository<UserApp>
    {

        public void SpeacialMethodForUserApp();
    }
}
