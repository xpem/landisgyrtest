using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EndPointsAdmin.Actions;
using EndPointsAdmin.Controllers;
using EndPointsAdmin.Models;
using SimpleInjector;

namespace EndPointsAdmin
{
    class Program
    {

        //aa
        static readonly Container dependenciesRegister;

        static Program()
        {
            //interfaces
            dependenciesRegister = new Container();

            dependenciesRegister.Register<IEndPoint, EndPoint>(Lifestyle.Singleton);
            dependenciesRegister.Register<IDatabase, Database>(Lifestyle.Singleton);           
            dependenciesRegister.Register<IServices, Services>(Lifestyle.Singleton);
            dependenciesRegister.Register<Actions.Actions>(Lifestyle.Singleton);
            
            dependenciesRegister.Verify();
            //
        }

        static void Main(string[] args)
        {
            int selectedOption = 0;

            while (true)
            {
                #region menu inicial
                string option = "";

                Console.WriteLine(">>>Endpoints  ADM<<<");

                Console.WriteLine("1 - Insert a new");
                Console.WriteLine("2 - Edit");
                Console.WriteLine("3 - Delete");
                Console.WriteLine("4 - List all");
                Console.WriteLine("5 - Find by serial number");
                Console.WriteLine("6 - Exit");
                Console.WriteLine("Insert a valid option: ");

                string input = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine("");
                #endregion

                if (!Int32.TryParse(input, out selectedOption))
                {
                    Console.Clear();
                    Console.WriteLine("INVALID INSERTION. Please enter a valid option.");
                }
                else
                {
                   IActions actions =  dependenciesRegister.GetInstance<Actions.Actions>();
                    switch (selectedOption)
                    {
                        case 1:
                            actions.Add();
                            break;
                        case 2:
                            actions.Edit();
                            break;
                        case 3:
                            actions.Delete();
                            break;
                        case 4:
                            actions.All();
                            break;
                        case 5:
                            actions.Find();
                            break;
                        case 6:
                            Console.WriteLine("Finalize(the saved data will be lost)? (F - finalize):");
                            string exitOption = Console.ReadKey().KeyChar.ToString().ToUpper();
                            if (exitOption == "F")
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.Clear();
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.Write("Invalid insertion. Please enter a valid option.");
                            break;
                    }
                }
            }

        }
    }
}
