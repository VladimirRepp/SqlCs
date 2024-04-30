using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public class MEmployee : IBaseModel
    {
        public int _id;
        public string Fullname;
        public string Position;
        public string Phone;

        public int Id { get => _id; set => _id = value; }

        public string ToParamINSERT => $"([Fullname], [Position], [Phone]) " +
                 $"VALUES (@param0, @param1, @param2)";

        public string ToParamUPDATE => $"Fullname = @param0, Position = @param1, Phone = @param2";

        public string[] ToArrayStr
        {
            get
            {
                string[] values = new string[3];
                values[0] = Fullname;
                values[1] = Position;
                values[2] = Phone;

                return values;
            }
        }

        public MEmployee() { }

        public MEmployee(int Id = 0, string Fullname = "", string Position = "", string Phone = "")
        {
            this._id = Id;
            this.Fullname = Fullname;
            this.Position = Position;
            this.Phone = Phone;
        }

        public MEmployee(SqlDataReader reader)
        {
            SetData(reader);
        }

        public void SetData(SqlDataReader reader)
        {
            _id = Convert.ToInt32(reader["Id"]);
            Fullname = reader["Fullname"].ToString();
            Position = reader["Position"].ToString();
            Phone = reader["Phone"].ToString();
        }       
    }
}
