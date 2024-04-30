using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public class MContract : IBaseModel
    {
        public int _id;
        public int IdClient;
        public int IdSchedule;
        public string RegistrationDate;

        public int Id { get => _id; set => _id = value; }

        public string ToParamINSERT => $"([IdClient], [IdSchedule], [RegistrationDate]) " +
                    $"VALUES (@param0, @param1, @param2)";

        public string ToParamUPDATE => $"IdClient = @param0, IdSchedule = @param1, RegistrationDate = @param2";

        public string[] ToArrayStr
        {
            get
            {
                string[] values = new string[3];
                values[0] = IdClient.ToString();
                values[1] = IdSchedule.ToString();
                values[2] = RegistrationDate;

                return values;
            }
        }

        public MContract() { }

        public MContract(int Id = 0, int IdClient = 0, int IdSchedule = 0, string RegistrationDate = "00.00.0000")
        {
            this._id = Id;
            this.IdClient = IdClient;
            this.IdSchedule = IdSchedule;
            this.RegistrationDate = RegistrationDate;
        }

        public MContract(SqlDataReader reader)
        {
            SetData(reader);
        }

        public void SetData(SqlDataReader reader)
        {
            _id = Convert.ToInt32(reader["Id"]);
            IdClient = Convert.ToInt32(reader["IdClient"]);
            IdSchedule = Convert.ToInt32(reader["IdSchedule"]);

            if (reader["RegistrationDate"] != DBNull.Value)
                RegistrationDate = reader["RegistrationDate"].ToString();
            else
                RegistrationDate = "00.00.0000";
        }
    }
}
