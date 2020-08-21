using EndPointsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Controllers
{
    public class Services : IServices
    {
        public IDatabase _DataBase;

        /// <summary>
        /// método de tratamento da inserção
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> Insert(EndPoint obj)
        {
            try
            {
                if (_DataBase == null) _DataBase = new Database();

                if ((await Find(obj.SerialNumber)) != null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("EndPoint Serial Number already exists");
                    Console.WriteLine("");
                    return false;

                }

                await _DataBase.DbInsert(obj);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// método de tratamento da inserção
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> Edit(EndPoint obj)
        {
            try
            {

                if (obj.SwitchState < 0 || obj.SwitchState > 2)
                {
                    return false;
                }

                await _DataBase.DbEdit(obj);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<bool> Delete(EndPoint obj)
        {
            try
            {
                await _DataBase.DbDelete(obj);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// método de busca de endpoint pelo serial number
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<EndPoint> Find(string serialNumber)
        {
            try
            {

                if (_DataBase == null) { _DataBase = new Database(); return null; }

                return await _DataBase.DbFind(serialNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// método que retorna a lista dos endpoints
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<List<EndPoint>> All()
        {
            try
            {

                if (_DataBase == null) { _DataBase = new Database(); return null; }

                return await _DataBase.DbAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
