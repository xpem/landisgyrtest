using EndPointsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Controllers
{
    /// <summary>
    /// interface da classe de db
    /// </summary>
    /// <typeparam name="EndPoint"></typeparam>
    public interface IDatabase
    {
        Task DbInsert(EndPoint obj);

        Task DbEdit(EndPoint obj);

        Task DbDelete(EndPoint obj);

        Task<EndPoint> DbFind(string serialNumber);

        Task<List<EndPoint>> DbAll();

    }



}
