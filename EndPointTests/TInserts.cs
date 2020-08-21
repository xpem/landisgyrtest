using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
//
using Moq;
using EndPointsAdmin.Actions;
using EndPointsAdmin.Models;
using EndPointsAdmin.Controllers;

namespace EndPointTests
{
    [TestClass]
    public class TInserts
    {

        /// <summary>
        /// inserção de um endpoint
        /// busca ele pelo seu serial number
        /// </summary>
        [TestMethod]
        public void InsertandFind()
        {

            Mock<IServices> mockActions = new Mock<IServices>();

            //mock data
            EndPoint endPoint = new EndPoint
            {
                SerialNumber = "13",
                MeterModelId = 16,
                MeterNumber = 11,
                MeterFirmwareVersion = "11",
                SwitchState = 0
            };

            EndPoint espFindEndPoint = new EndPoint();
            EndPoint findEndPoint = new EndPoint();

            //o resultado da busca deve retornar o item
            mockActions.Setup(r => r.Find("13")).Returns(Task.FromResult(endPoint));


            Services services = new Services(); 
            Task.Run(() => services.Insert(endPoint)).Wait();
            Task.Run(() => findEndPoint = services.Find("13").Result).Wait();


            Task.Run(() => espFindEndPoint = mockActions.Object.Find("13").Result).Wait();

            Assert.AreEqual(findEndPoint, espFindEndPoint);

        }

        /// <summary>
        /// inserção de um endpoint
        /// busca ele pelo seu serial number
        /// </summary>
        [TestMethod]
        public void InvalidInsertSerialNumber()
        {

            Mock<IServices> mockActions = new Mock<IServices>();
            Database database = new Database();

            //mock data com um MeterModel
            EndPoint endPoint = new EndPoint
            {
                SerialNumber = "11",
                MeterModelId = 16,
                MeterNumber = 11,
                MeterFirmwareVersion = "11",
                SwitchState = 0
            };
            bool espInsertReturn = true, insertReturn = true;

            //o resultado deve ser retorno false como resultado do serial number encontrado
            mockActions.Setup(r => r.Insert(endPoint)).Returns(Task.FromResult(false));
            Services services = new Services();


            //inserção duplicada
            Task.Run(() => services.Insert(endPoint)).Wait();
            Task.Run(() => insertReturn = services.Insert(endPoint).Result).Wait();

            //resultado esperado da inserção duplicada
            Task.Run(() => espInsertReturn = mockActions.Object.Insert(endPoint).Result).Wait();

            Assert.AreEqual(insertReturn, espInsertReturn);

        }
    }
}
