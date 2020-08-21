using EndPointsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Controllers
{
   public interface IServices
    {
        Task<bool> Insert(EndPoint obj);

        Task<bool> Edit(EndPoint obj);

        Task<bool> Delete(EndPoint obj);

        Task<EndPoint> Find(string serialNumber);

        Task<List<EndPoint>> All();
    }
}
