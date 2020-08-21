using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Actions
{
  public  interface IActions
    {
        void Add();
        void Edit();

        void Delete();

        void Find();

        void All();
    }
}
