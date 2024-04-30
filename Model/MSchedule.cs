using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public class MSchedule : IBaseModel
    {
        public int _id;
        public int IdSpecialization;
        public string Date;
        public string Time;

        public int Id { get => _id; set => _id = value; }

        public string ToParamINSERT => $"([IdSpecialization], [Date], [Time]) " +
                     $"VALUES (@param0, @param1, @param2)";

        public string ToParamUPDATE => $"IdSpecialization = @param0, Date = @param1, Time = @param2";

        public string[] ToArrayStr
        {
            get
            {
                string[] values = new string[3];
                values[0] = IdSpecialization.ToString();
                values[1] = Date;
                values[2] = Time;

                return values;
            }
        }

        public MSchedule() { }

        public MSchedule(int Id = 0, int IdSpecialization = 0, string Date = "", string Time = "")
        {
            this._id = Id;
            this.IdSpecialization = IdSpecialization;
            this.Date = Date;
            this.Time = Time;
        }

        public MSchedule(SqlDataReader reader)
        {
            SetData(reader);
        }

        public void SetData(SqlDataReader reader)
        {
            _id = Convert.ToInt32(reader["Id"]);
            IdSpecialization = Convert.ToInt32(reader["IdSpecialization"]);
            Date = reader["Date"].ToString();
            Time = reader["Time"].ToString();
        }
    }
}
