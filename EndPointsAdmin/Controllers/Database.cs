using EndPointsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Controllers
{
    /// <summary>
    /// Banco de dados
    /// </summary>
    /// <typeparam name="EndPoint"></typeparam>
    public class Database : IDatabase
    {

        //BANCO DE DADOS
        protected static List<EndPoint> DB = new List<EndPoint>();
        //

        public Database()
        {

        }



        /// <summary>
        /// insere o endpoint
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task DbInsert(EndPoint obj)
        {
            try
            {
                
                    await Task.Run(() => DB.Add(obj));
            }
            catch (Exception ex)
            {
                //retorno con a exception(não tratada)
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Edita o endpoint
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task DbEdit(EndPoint obj)
        {
            try
            {
                //atualiza o objeto de acordo com o indice atual dele.
                await Task.Run(() => DB[(DB.FindIndex(p => p.SerialNumber == obj.SerialNumber))] = obj);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }    
        }

        /// <summary>
        /// Exclui um endpoint
        /// </summary>
        /// <param name="obj"></param>
        public virtual async Task DbDelete(EndPoint obj)
        {
            try
            {
                //atualiza o objeto de acordo com o indice atual dele.
                await Task.Run(() => DB.Remove(obj));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }




        /// <summary>
        /// Busca um endpoint pelo seu serial number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item unico</returns>
        public virtual async Task<EndPoint> DbFind(string serialNumber)
        {
            return await Task.Run(() => DB.Find(d => d.SerialNumber.Equals(serialNumber)));
        }


        /// <summary>
        /// Todos os itens
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna todos os itens da lista</returns>
        public virtual async Task<List<EndPoint>> DbAll()
        {
            return await Task.Run(() => DB);
        }

    }
}
