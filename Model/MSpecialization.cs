using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Administrator.Model
{
    public class MSpecialization : IBaseModel
    {
        public int _id;
        public int IdEmployee;
        public string Name;
        public string Office;
        public float Cost;

        public int Id { get => _id; set => _id = value; }

        public string ToParamINSERT => $"([IdEmployee], [Name], [Office], [Cost]) " +
                    $"VALUES (@param0, @param1, @param2, @param3)";

        public string ToParamUPDATE => $"IdEmployee = @param0, Name = @param1, Office = @param2, Cost = @param3";

        public string[] ToArrayStr
        {
            get
            {
                string[] values = new string[4];
                values[0] = Name;
                values[1] = Cost.ToString();
                values[2] = IdEmployee.ToString();
                values[3] = Office;

                return values;
            }
        }

        public MSpecialization() { }

        public MSpecialization(int Id = 0, int IdEmployee = 0, string Name = "", string Office = "", float Cost = 0)
        {
            this._id = Id; 
            this.IdEmployee = IdEmployee; 
            this.Name = Name; 
            this.Office = Office; 
            this.Cost = Cost;
        }

        public MSpecialization(SqlDataReader reader)
        {
            SetData(reader);
        }

        public void SetData(SqlDataReader reader)
        {
            _id = Convert.ToInt32(reader["Id"]);
            IdEmployee = Convert.ToInt32(reader["IdEmployee"]);
            Name = reader["Name"].ToString();
            Office = reader["Office"].ToString();
            Cost = Convert.ToSingle(reader["Cost"]);
        }
    }
}
