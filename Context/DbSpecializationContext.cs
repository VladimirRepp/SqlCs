using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinic_Administrator.Model;

namespace Clinic_Administrator.Context
{
    public class DbSpecializationContext : DbBaseContext<MSpecialization>
    {
        public DbSpecializationContext()
        {
            _tableName = "specializations";
        }
    }
}
