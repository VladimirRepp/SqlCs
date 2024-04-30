using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public class MClient : IBaseModel
    {
        public int _id;
        public string Fullname;
        public string Passport;
        public string Phone;

        public string ToParamINSERT => $"([Fullname], [Passport], [Phone]) " +
                   $"VALUES (@param0, @param1, @param2)";

        public string ToParamUPDATE => $"Fullname = @param0, Passport = @param1, Phone = @param2";

        public string[] ToArrayStr
        {
            get
            {
                string[] values = new string[3];
                values[0] = Fullname;
                values[1] = Passport;
                values[2] = Phone;

                return values;
            }
        }

        public int Id { get => _id; set => _id = value; }

        public MClient() { }

        public MClient(int Id = 0, string Fullname = "", string Passport = "", string Phone = "")
        {
            this._id = Id;
            this.Fullname = Fullname;
            this.Passport = Passport;
            this.Phone = Phone;
        }

        public MClient(SqlDataReader reader)
        {
            SetData(reader);
        }

        public void SetData(SqlDataReader reader)
        {
            _id = Convert.ToInt32(reader["Id"]);
            Fullname = reader["Fullname"].ToString();
            Passport = reader["Passport"].ToString();
            Phone = reader["Phone"].ToString();
        }
    }
}
