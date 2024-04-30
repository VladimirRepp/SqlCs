using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public interface IBaseModel
    {
        int Id { get; set; }

        string ToParamINSERT { get; }
        string ToParamUPDATE { get; }

        string[] ToArrayStr { get; }

        void SetData(SqlDataReader reader);    
    }
}
