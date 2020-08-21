using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//
using Moq;
using EndPointsAdmin.Actions;
using EndPointsAdmin.Models;
using EndPointsAdmin.Controllers;

namespace EndPointTests
{
    [TestClass]
    public class TDelete
    {
        /// <summary>
        /// Exclusão de um endpoint
        /// Inclui, exclui e tenta buscar o item excluido.
        /// </summary>
        [TestMethod]
        public void DeleteAndTryFind()
        {

            Mock<IServices> mockActions = new Mock<IServices>();
            Database database = new Database();

            //mock data
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

            //inseridos no teste de insert e delete
            //Task.Run(() => services.Insert(endPointa)).Wait();
            //Task.Run(() => services.Insert(endPointb)).Wait();


            EndPoint ExpEndPoint = null;
            EndPoint RetEndPoint = new EndPoint();

            //o resultado da busca deve retornar nada
            mockActions.Setup(r => r.Find("12")).Returns(Task.FromResult(ExpEndPoint));

            Services services = new Services();
            //insere dois itens
       
            //exclui o segundo item
            Task.Run(() => services.Delete(endPointb).Result).Wait();

            //busca o item excluido, deixando o objeto atual nulo
            Task.Run(() => RetEndPoint = services.Find("12").Result).Wait();
            
            //caso busque um que nao foi excluido, o teste falha
            //Task.Run(() => RetEndPoint = services.Find("11").Result).Wait();
            //
            
            //resultado esperado
            Task.Run(() => ExpEndPoint = mockActions.Object.Find("12").Result).Wait();


            Assert.AreEqual(RetEndPoint, ExpEndPoint);

        }
    }
}
