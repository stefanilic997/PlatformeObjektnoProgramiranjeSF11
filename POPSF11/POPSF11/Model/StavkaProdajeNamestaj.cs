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
                id = value;
                OnPropertyChanged("Id");
            }

        }
        
        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; OnPropertyChanged("Kolicina"); }
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
        
        public int NamestajId
        {
            get { return namestajId; }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
            }

        }
        public static StavkaProdajeNamestaj GetById(int id)
        {
            foreach (var Namestaj in Projekat.Instance.StavkeProdajeNamestaja)
            {
                if (Namestaj.Id == id)
                {
                    return Namestaj;
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
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM  StavkeNamestaja";

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds, "StavkeNamestaja"); 
                foreach (DataRow row in ds.Tables["StavkaNametsaja"].Rows)
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
                cmd.CommandText = $"INSERT INTO  StavkaNametsaja (RacunId,NamestajId,Kolicina) VALUES(@RacunId,@NamestajId,@Kolicina);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("RacunId", s.RacunId);
                cmd.Parameters.AddWithValue("NamestajId", s.NamestajId);
                cmd.Parameters.AddWithValue("Kolicina", s.Kolicina);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                s.Id = newId;
            }
            Projekat.Instance.StavkeProdajeNamestaja.Add(s);
            return s;
        }
        public static void Update(StavkaProdajeNamestaj s)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.CommandText = "UPDATE StavkaNametsaja SET RacunId=@RacunId,NamestajId=@NamestajId,Kolicina=@Kolicina WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", s.Id);
                cmd.Parameters.AddWithValue("NamestajId", s.NamestajId);
                cmd.Parameters.AddWithValue("RacunId", s.RacunId);
                cmd.Parameters.AddWithValue("Kolicina", s.Kolicina);
                cmd.ExecuteNonQuery();

                foreach (var spn in Projekat.Instance.StavkeProdajeNamestaja)
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

        #endregion
    }
}
