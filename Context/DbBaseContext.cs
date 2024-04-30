using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinic_Administrator.Model;

namespace Clinic_Administrator.Context
{
    public class DbBaseContext<T> where T : IBaseModel, new()
    {
        // Поля
        protected List<T> _data;

        protected string _connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=(412)database; Integrated Security=True";
        protected string _tableName;
        protected string _query;

        protected SqlDataAdapter _adapter;
        protected DataSet _dataSet;
        protected SqlConnection _connection;

        // Свойства
        public SqlDataAdapter SqlDataAdapter => _adapter;
        public DataSet DataSet => _dataSet;
        public SqlConnection SqlConnection => _connection;

        public string TableName { get => _tableName; set => _tableName = value; }
        public int Count => _data.Count;
        public T this[int index] { get => _data[index]; set => _data[index] = value; }
        public List<T> Data => _data;

        // Методы
        public DbBaseContext()
        {
            _connection = new SqlConnection(_connectionString);
            _tableName = "default";
            _data = new List<T>();
        }

        public int GetIndexById(int id)
        {
            return _data.FindIndex(x => x.Id == id);
        }

        public bool TryGetIndexById(int id, out int index)
        {
            index = _data.FindIndex(x => x.Id == id);
            if (index == -1)
                return false;

            return true;
        }

        public T GetById(int id)
        {
            return _data.Find(x => x.Id == id);
        }

        public bool TryGetById(int id, out T found)
        {
            found = _data.Find(x => x.Id == id);

            if (found == null)
                return false;

            return true;
        }

        public bool Query_SelectById(int id)
        {
            T found = default;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM {_tableName}";
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                found = new T();
                                found.SetData(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return found != null;
        }

        public bool Query_TrySelectById(int id, out T found)
        {
            found = default;

            try
            {
                _data.Clear();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM {_tableName}";
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                found = new T();
                                found.SetData(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return found != null;
        }

        public bool Query_Select()
        {
            _query = $"SELECT * FROM {_tableName}";

            try
            {
                _data.Clear();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM {_tableName}";
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T d = new T();
                                d.SetData(reader);
                                _data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public bool Query_Insert(T d)
        {
            if (d == null)
                throw new Exception("Данные пусты!");

            bool done = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"INSERT {_tableName} {d.ToParamINSERT}";

                        int i = 0;
                        foreach(string s in d.ToArrayStr)
                        {
                            command.Parameters.AddWithValue($"@param{i++}", s);
                        }

                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            _data.Add(d);
                            done = true;
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return done;
        }

        public bool Query_Update(T d)
        {
            if (d == null)
                throw new Exception("Объект пуст!");

            bool done = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE {_tableName} SET {d.ToParamUPDATE} WHERE Id = {d.Id}";

                        int i = 0;
                        foreach (string s in d.ToArrayStr)
                        {
                            command.Parameters.AddWithValue($"@param{i++}", s);
                        }

                        connection.Open();
                        if(command.ExecuteNonQuery() == 1)
                        {
                            done = EditById(d);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return done;
        }

        public bool Query_Delete(int id)
        {
            bool done = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"DELETE {_tableName} WHERE Id = @id";
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            done = RemoveAtId(id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return done;
        }

        private bool EditById(T d)
        {
            int index = _data.FindIndex(x => x.Id == d.Id);
            if (index == -1)
                return false;

            _data[index] = d;
            return true;
        }

        private bool RemoveAtId(int id)
        {
            int index = _data.FindIndex(x => x.Id == id);
            if (index == -1)
                return false;

            _data.RemoveAt(index);
            return true;
        }
    }
}
