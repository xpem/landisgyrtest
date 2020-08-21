using EndPointsAdmin.Controllers;
using EndPointsAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Actions
{
    public class Actions : IActions
    {

        private readonly IServices Services;


        public Actions(IServices services)
        {
            Services = services;
        }

        public void Add()
        {
            Console.Clear();
            Console.WriteLine(">>>Endpoints  ADM<<<");
            Console.WriteLine(">>>Add Endpoint<<<");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string serialNumber = "", meterFirmwareVersion = "";
            int meterNumber = 0, meterModelId = 0, switchState = -1;

            while (string.IsNullOrEmpty(serialNumber))
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Serial Number: ");
                serialNumber = Console.ReadLine();
                Console.WriteLine();
                //
            }
            while ((meterModelId < 16 || meterModelId > 19))
            {
                Console.WriteLine("______________________________________");
                Console.WriteLine(" 16 - NSX1P2W");
                Console.WriteLine(" 17 - NSX1P3W");
                Console.WriteLine(" 18 - NSX2P2W");
                Console.WriteLine(" 19 - NSX3P4W");
                Console.WriteLine("Meter Model Id:");
                if (!Int32.TryParse(Console.ReadLine(), out meterModelId))
                {
                    Console.WriteLine("INSERT A VALID OPTION");
                }
                else if (meterModelId < 16 || meterModelId > 19)
                {
                    Console.WriteLine("INSERT A VALID OPTION");
                }

                //meterModelId = Console.ReadLine();
                Console.WriteLine();
            }

            while (meterNumber < 1)
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Meter Number: ");
                if (!Int32.TryParse(Console.ReadLine(), out meterNumber))
                    Console.WriteLine("INSERT ONLY NUMBERS");
                Console.WriteLine();
                //
            }

            while (string.IsNullOrEmpty(meterFirmwareVersion))
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Meter Firmware Version: ");
                meterFirmwareVersion = Console.ReadLine();
                Console.WriteLine();
                //
            }

            while (switchState < 0 || switchState > 2)
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine(" 0 - Disconnected");
                Console.WriteLine(" 1 - Connected");
                Console.WriteLine(" 2 - Armed");
                Console.WriteLine("Switch State: ");

                if (!Int32.TryParse(Console.ReadLine(), out switchState))
                {
                    Console.WriteLine("INSERT A VALID OPTION");
                }
                else if (switchState < 0 || switchState > 2)
                {
                    Console.WriteLine("INSERT A VALID OPTION");
                }

                Console.WriteLine();
                //
            }

            //confirma e cadastra endpoint
            string confirmOption = "";
            while (confirmOption != "Y" && confirmOption != "N")
            {
                Console.WriteLine(">>>>>>Confirm this Insertion? (Y/N)");
                confirmOption = Console.ReadKey().KeyChar.ToString().ToUpper();
                if (confirmOption == "Y")
                {
                    EndPoint endPoint = new EndPoint();
                    endPoint.SerialNumber = serialNumber;
                    endPoint.MeterModelId = meterModelId;
                    endPoint.MeterNumber = meterNumber;
                    endPoint.MeterFirmwareVersion = meterFirmwareVersion;
                    endPoint.SwitchState = switchState;

                    bool ret = false;
                    Task.Run(() => ret = Services.Insert(endPoint).Result).Wait();

                    
                    //para sair da função e voltar ao menu
                    Console.WriteLine("");
                    Console.WriteLine("");
                    if (ret) { Console.WriteLine("ENDPOINT INSERTED"); }                   
                    Console.WriteLine("Press any key to back to menu");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// edita um endpoint com base em seu serial number
        /// </summary>
        public void Edit()
        {
            Console.Clear();
            Console.WriteLine(">>>Endpoints  ADM<<<");
            Console.WriteLine(">>>Edit Endpoint<<<");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            string serialNumber = "";

            while (string.IsNullOrEmpty(serialNumber))
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Serial number of EndPoint:");
                serialNumber = Console.ReadLine();
                Console.WriteLine();
                //
            }

            EndPoint edtEndPoint = new EndPoint();

            Task.Run(async () => edtEndPoint = await Services.Find(serialNumber)).Wait();

            if (edtEndPoint != null)
            {
                int switchState = -1;
                while (switchState < 0 || switchState > 2)
                {

                    Console.WriteLine(">>CHANGE SWITCH STATE - SERIAL NUMBER : " + serialNumber);
                    //
                    Console.WriteLine("______________________________________");
                    Console.WriteLine(" 0 - Disconnected");
                    Console.WriteLine(" 1 - Connected");
                    Console.WriteLine(" 2 - Armed");
                    Console.WriteLine("Switch State: ");

                    if (!Int32.TryParse(Console.ReadLine(), out switchState))
                    {
                        Console.WriteLine("INSERT A VALID OPTION");
                    }
                    else if (switchState < 0 || switchState > 2)
                    {
                        Console.WriteLine("INSERT A VALID OPTION");
                    }

                    Console.WriteLine("");
                    //
                }

                //confirma e edita o swicht state do endpoint
                string confirmOption = "";
                while (confirmOption != "Y" && confirmOption != "N")
                {
                    Console.WriteLine(">>>>>>Change this Switch State? (Y/N)");
                    confirmOption = Console.ReadKey().KeyChar.ToString().ToUpper();
                    if (confirmOption == "Y")
                    {
                        edtEndPoint.SwitchState = switchState;

                        Services.Edit(edtEndPoint);

                        //para sair da função e voltar ao menu
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("SWITCH STATE UPDATED.");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine(@"Not exist Endpoint with this serial number.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Exclui um endpoint pelo seu serial number
        /// </summary>
        public void Delete()
        {
            Console.Clear();
            Console.WriteLine(">>>Endpoints  ADM<<<");
            Console.WriteLine(">>>Delete Endpoint<<<");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            string serialNumber = "";

            while (string.IsNullOrEmpty(serialNumber))
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Serial number of EndPoint:");
                serialNumber = Console.ReadLine();
                Console.WriteLine();
                //
            }

            EndPoint edtEndPoint = new EndPoint();

            Task.Run(async () => edtEndPoint = await Services.Find(serialNumber)).Wait();

            if (edtEndPoint.SerialNumber != null)
            {
                //confirma e edita o swicht state do endpoint
                string confirmOption = "";
                while (confirmOption != "Y" && confirmOption != "N")
                {
                    Console.WriteLine(">>>>>>Delete this EndPoint? (Y/N)");
                    confirmOption = Console.ReadKey().KeyChar.ToString().ToUpper();
                    if (confirmOption == "Y")
                    {
                        Services.Delete(edtEndPoint);

                        //para sair da função e voltar ao menu
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("ENDPOINT DELETED.");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Not exist Endpoint with this serial number.");
                Console.ReadKey();
            }
        }


        /// <summary>
        /// busca um endpoint pelo seu serial number
        /// </summary>
        public void Find()
        {
            Console.Clear();
            Console.WriteLine(">>>Endpoints  ADM<<<");
            Console.WriteLine(">>>Serach Endpoint<<<");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string serialNumber = "";

            while (string.IsNullOrEmpty(serialNumber))
            {
                //
                Console.WriteLine("______________________________________");
                Console.WriteLine("Serial number of EndPoint:");
                serialNumber = Console.ReadLine();
                Console.WriteLine();
                //
            }

            EndPoint endPoint = new EndPoint();

            Task.Run(async () => endPoint = await Services.Find(serialNumber)).Wait();
            if (endPoint != null)
            {
                PrintEndPoint(endPoint);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Press any key to back to menu");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Retorna a lista de endpoints
        /// </summary>
        public void All()
        {
            Console.Clear();
            Console.WriteLine(">>>Endpoints  ADM<<<");
            Console.WriteLine(">>>Endpoints List<<<");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            List<EndPoint> endPointlist = new List<EndPoint>();

            Task.Run(async () => endPointlist = await Services.All()).Wait();
            if (endPointlist != null)
            {
                foreach (EndPoint endPoint in endPointlist)
                {
                    PrintEndPoint(endPoint);
                }              
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();
        }


        protected void PrintEndPoint(EndPoint endPoint)
        {
            Console.WriteLine("______________________________________");
            Console.WriteLine("ENDPOINT");
            Console.WriteLine("Serial Number: " + endPoint.SerialNumber);
            string meterNumber = "";
            switch (endPoint.MeterModelId)
            {
                case (16):
                    meterNumber = " 16 - NSX1P2W";
                    break;
                case (17):
                    meterNumber = " 17 - NSX1P3W";
                    break;
                case (18):
                    meterNumber = " 18 - NSX2P2W";
                    break;
                case (19):
                    meterNumber = " 19 - NSX3P4W";
                    break;
            }

            Console.WriteLine("Meter Model Id: " + meterNumber);
            Console.WriteLine("Meter Number: " + endPoint.MeterNumber);
            Console.WriteLine("Meter Firmware Version: " + endPoint.MeterFirmwareVersion);

            string switchState = "";
            switch (endPoint.SwitchState)
            {
                case (0):
                    switchState = " 0 - Disconnected";
                    break;
                case (1):
                    switchState = " 1 - Connected";
                    break;
                case (2):
                    switchState = " 2 - Armed";
                    break;
            }

            Console.WriteLine("Meter Switch State: " + switchState);
            Console.WriteLine("______________________________________");
        }
    }
}
