using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinic_Administrator.Model;

namespace Clinic_Administrator.Context
{
    public class DbContractContext : DbBaseContext<MContract>
    {
        public DbContractContext()
        {
            _tableName = "contracts";
        }
    }
}
