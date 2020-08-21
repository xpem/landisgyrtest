using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Moq;
using EndPointsAdmin.Actions;
using EndPointsAdmin.Models;
using EndPointsAdmin.Controllers;

namespace EndPointTests
{
    [TestClass]
    public class TEdits
    {

        /// <summary>
        /// Edição de um endpoint
        /// busca ele pelo seu serial number e verifica o atual estado do switch
        /// </summary>
        [TestMethod]
        public void EditAndFind()
        {
            Mock<IServices> mockActions = new Mock<IServices>();
            Database database = new Database();

            //mock data --inserido no teste do insert
            EndPoint endPointa = new EndPoint
            {
                SerialNumber = "11",
                MeterModelId = 16,
                MeterNumber = 11,
                MeterFirmwareVersion = "11",
                SwitchState = 0
            };
            EndPoint endPointb = new EndPoint
            {
                SerialNumber = "12",
                MeterModelId = 17,
                MeterNumber = 12,
                MeterFirmwareVersion = "12",
                SwitchState = 1
            };

            EndPoint UpdatedEndPointb = new EndPoint
            {
                SerialNumber = "12",
                MeterModelId = 17,
                MeterNumber = 12,
                MeterFirmwareVersion = "12",
                SwitchState = 2
            };

            EndPoint findEndPoint = new EndPoint(), expFindEndPoint = new EndPoint();

            //o resultado da busca deve retornar o item alterado
            mockActions.Setup(r => r.Find("12")).Returns(Task.FromResult(UpdatedEndPointb));


            Services services = new Services();
            //insere dois itens
            //Task.Run(() => services.Insert(endPointa)).Wait();
            Task.Run(() => services.Insert(endPointb)).Wait();
            //Edita o segundo item
            Task.Run(() => services.Edit(UpdatedEndPointb).Result).Wait();

            //busca o item editado
            Task.Run(() => findEndPoint = services.Find("12").Result).Wait();
            //resultado esperado
            Task.Run(() => expFindEndPoint = mockActions.Object.Find("12").Result).Wait();


            Assert.AreEqual(findEndPoint, expFindEndPoint);
        }

        /// <summary>
        /// tenta inserir um estado inválido.
        /// </summary>
        [TestMethod]
        public void InvalidEdit()
        {
            Mock<IServices> mockActions = new Mock<IServices>();
            Database database = new Database();
            //mock data
            EndPoint endPoint = new EndPoint
            {
                SerialNumber = "12",
                MeterModelId = 16,
                MeterNumber = 11,
                MeterFirmwareVersion = "11",
                SwitchState = 0
            };

            EndPoint InvalidEndPoint = new EndPoint
            {
                SerialNumber = "12",
                MeterModelId = 17,
                MeterNumber = 12,
                MeterFirmwareVersion = "12",
                SwitchState = 3
            };

            EndPoint findEndPoint = new EndPoint(), espFindEndPoint = new EndPoint();

            //o resultado da busca deve retornar o item alterado
            mockActions.Setup(r => r.Edit(InvalidEndPoint)).Returns(Task.FromResult(false));


            Services services = new Services();
            //insere dois itens
            Task.Run(() => services.Insert(endPoint)).Wait();

            //atribuição de estado inválida
            endPoint.SwitchState = 3;


            bool ret = true, expRet = true;
            //Edita o segundo item
            Task.Run(() => ret = services.Edit(InvalidEndPoint).Result).Wait();

            //resultado esperado
            Task.Run(() => expRet = mockActions.Object.Edit(InvalidEndPoint).Result).Wait();


            Assert.AreEqual(ret, expRet);
        }
    }
}
