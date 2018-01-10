
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
    public class StavkaProdajeNamestaj : INotifyPropertyChanged
    {
        private int id;
        private int kolicina;
        private int racunId;
        private int namestajId;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get { return id; }
            set
            {
                OnPropertyChanged("Id");
                id = value;

            }

        }

        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                OnPropertyChanged("Kolicina");
                kolicina = value;
            }
        }

        public int RacunId
        {
            get { return racunId; }
            set
            {
                OnPropertyChanged("RacunId");
                racunId = value;

            }

        }

        public int NamestajId
        {
            get { return namestajId; }
            set
            {
                OnPropertyChanged("NamestajId");
                namestajId = value;

            }

        }
        public static StavkaProdajeNamestaj GetById(int id)
        {
            foreach (StavkaProdajeNamestaj SPNamestaj in Projekat.Instance.SPNamestaj)
            {
                if (SPNamestaj.Id == id)
                {
                    return SPNamestaj;
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
        public static ObservableCollection<StavkaProdajeNamestaj> GetAll()
        {
            var StavkeProdajeNamestaja = new ObservableCollection<StavkaProdajeNamestaj>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                DataSet ds = new DataSet();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM  StavkeProdajeNamestaja";


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds, "StavkeProdajeNamestaja");
                foreach (DataRow row in ds.Tables["StavkeProdajeNamestaja"].Rows)
                {
                    var s = new StavkaProdajeNamestaj();
                    s.Id = int.Parse(row["Id"].ToString());
                    s.RacunId = int.Parse(row["RacunId"].ToString());
                    s.NamestajId = int.Parse(row["NamestajId"].ToString());
                    s.Kolicina = int.Parse(row["Kolicina"].ToString());

                    StavkeProdajeNamestaja.Add(s);

                }
                return StavkeProdajeNamestaja;
            }
        }
        public static StavkaProdajeNamestaj Create(StavkaProdajeNamestaj s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = $"INSERT INTO  StavkeProdajeNamestaja (RacunId,NamestajId,Kolicina) VALUES(@RacunId,@NamestajId,@Kolicina);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("RacunId", s.RacunId);
                cmd.Parameters.AddWithValue("NamestajId", s.NamestajId);
                cmd.Parameters.AddWithValue("Kolicina", s.Kolicina);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                s.Id = newId;
            }
            Projekat.Instance.SPNamestaj.Add(s);
            return s;
        }
        public static void Update(StavkaProdajeNamestaj s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.CommandText = "UPDATE StavkeProdajeNamestaja SET RacunId=@RacunId,NamestajId=@NamestajId,Kolicina=@Kolicina WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", s.Id);
                cmd.Parameters.AddWithValue("NamestajId", s.NamestajId);
                cmd.Parameters.AddWithValue("RacunId", s.RacunId);
                cmd.Parameters.AddWithValue("Kolicina", s.Kolicina);
                cmd.ExecuteNonQuery();

                foreach (var spn in Projekat.Instance.SPNamestaj)
                {
                    if (spn.Id == s.Id)
                    {
                        spn.RacunId = s.RacunId;
                        spn.NamestajId = s.NamestajId;
                        spn.Kolicina = s.Kolicina;
                        break;
                    }
                }
            }


        }
        public static void Delete(StavkaProdajeNamestaj spNamestaj)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                cmd.CommandText = "DELETE StavkeProdajeNamestaja WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", spNamestaj.Id);
                cmd.ExecuteNonQuery();
                foreach (var spdu in Projekat.Instance.SPNamestaj)
                {
                    if (spdu.Id == spNamestaj.Id)
                    {
                        Projekat.Instance.SPNamestaj.Remove(spdu);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}