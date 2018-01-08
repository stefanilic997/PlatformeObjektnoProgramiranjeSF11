using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_11_GUI.Model
{
    public class StavkaProdajeDodatnaUsluga
    {
        private int id;
        private int racunId;
        private int dodatnaUslugaId;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id

        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }

        }
        
        public int RacunId
        {
            get { return racunId; }
            set
            {
                racunId = value;
                OnPropertyChanged("RacunId");
            }

        }
        
        public int DodatnaUslugaId
        {
            get { return dodatnaUslugaId; }
            set
            {
                dodatnaUslugaId = value;
                OnPropertyChanged("DodatnaUslugaId");
            }

        }
        public static StavkaProdajeDodatnaUsluga GetById(int id)
        {
            foreach (var DodatnaUsluga in Projekat.Instance.StavkeProdajeDodatneUsluge)
            {
                if (DodatnaUsluga.Id == id)
                {
                    return DodatnaUsluga;
                }

            }
            return null;

        }

        


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }
        #region Database
        public static ObservableCollection<StavkaProdajeDodatnaUsluga> GetAll()
        {
            var stavkeProdajeDodatneUsluge = new ObservableCollection<StavkaProdajeDodatnaUsluga>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM  StavkeDodatneUsluge ";

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds, "StavkeDodatneUsluge");
                foreach (DataRow row in ds.Tables["StavkeDodatneUsluge"].Rows)
                {
                    var s = new StavkaProdajeDodatnaUsluga();
                    s.Id = int.Parse(row["Id"].ToString());
                    s.RacunId = int.Parse(row["RacunId"].ToString());
                    s.DodatnaUslugaId = int.Parse(row["DodatnaUslugaId"].ToString());


                    stavkeProdajeDodatneUsluge.Add(s);

                }
                return stavkeProdajeDodatneUsluge;
            }
        }
        public static StavkaProdajeDodatnaUsluga Create(StavkaProdajeDodatnaUsluga s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.CommandText = $"INSERT INTO  StavkaDodatneUsluge (RacunId,DUId) VALUES(@RacunId,@DUId);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("RacunId", s.RacunId);
                cmd.Parameters.AddWithValue("DodatnaUslugaId", s.DodatnaUslugaId);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); 
                s.Id = newId;


            }
            Projekat.Instance.StavkeProdajeDodatneUsluge.Add(s);
            return s;
        }
        public static void Update(StavkaProdajeDodatnaUsluga s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE  StavkaDUsluge SET RacunId=@RacunId,DodatnaUslugaId=@DodatnaUslugaId WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", s.Id);
                cmd.Parameters.AddWithValue("DUId", s.DodatnaUslugaId);
                cmd.Parameters.AddWithValue("RacunId", s.RacunId);

                cmd.ExecuteNonQuery();

                foreach (var stavkaProdajeDodatnaUsluga in Projekat.Instance.StavkeProdajeDodatneUsluge)
                {
                    if (stavkaProdajeDodatnaUsluga.Id == s.Id)
                    {
                        stavkaProdajeDodatnaUsluga.RacunId = s.RacunId;
                        stavkaProdajeDodatnaUsluga.DodatnaUslugaId = s.DodatnaUslugaId;

                        break;
                    }
                }
            }


        }

        #endregion
    }
}
