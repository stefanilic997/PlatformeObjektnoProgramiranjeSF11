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
    [Serializable]
    public class AkcijskaProdaja : INotifyPropertyChanged

    {
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavresetka;
        private int popust;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                OnPropertyChanged("Id");
                id = value;
            }
        }

        public int Popust
        {
            get { return popust; }
            set
            {
                OnPropertyChanged("Popust");
                popust = value;
            }
        }


        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                OnPropertyChanged("DatumPocetka");
                datumPocetka = value;
            }
        }

        public DateTime DatumZavresetka
        {
            get { return datumZavresetka; }
            set
            {
                OnPropertyChanged("DatumZavresetka");
                datumZavresetka = value;
            }
        }


        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                OnPropertyChanged("Obrisan");
                obrisan = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static AkcijskaProdaja GetById(int id)
        {
            foreach (var akcijskaProdaja in Projekat.Instance.AkcijskeProdaje)
            {
                if (akcijskaProdaja.Id == id)
                {
                    return akcijskaProdaja;
                }

            }
            return null;

        }


        public override string ToString()
        {
            return ($"Popust:{Popust}% Pocetak:{DatumPocetka.ToShortDateString()} Kraj:{DatumZavresetka.ToShortDateString()}");
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<AkcijskaProdaja> GetAll()
        {
            var sveAkcije = new ObservableCollection<AkcijskaProdaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Akcije WHERE Obrisan =0";


                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Akcije");
                foreach (DataRow row in ds.Tables["Akcije"].Rows)
                {
                    var akcija = new AkcijskaProdaja();
                    akcija.Id = int.Parse(row["Id"].ToString());
                    akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akcija.DatumZavresetka = DateTime.Parse(row["DatumZavresetka"].ToString());
                    akcija.Popust = int.Parse(row["Popust"].ToString());
                    akcija.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    sveAkcije.Add(akcija);

                }
            }
            return sveAkcije;
        }

        public static AkcijskaProdaja Create(AkcijskaProdaja akcijskaProdaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = $"INSERT INTO Akcije (DatumPocetka,DatumZavresetka,Popust,Obrisan) VALUES (@DatumPocetka,@DatumZavresetka,@Popust,@Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("DatumPocetka", akcijskaProdaja.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavresetka", akcijskaProdaja.DatumZavresetka);
                cmd.Parameters.AddWithValue("Popust", akcijskaProdaja.Popust);
                cmd.Parameters.AddWithValue("Obrisan", akcijskaProdaja.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                akcijskaProdaja.Id = newId;
            }
            Projekat.Instance.AkcijskeProdaje.Add(akcijskaProdaja);
            return akcijskaProdaja;
        }

        public static void Update(AkcijskaProdaja akcijskaProdaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "UPDATE Akcije SET DatumPocetka=@DatumPocetka,DatumZavresetka=@DatumZavresetka,Popust=@Popust,Obrisan=@Obrisan WHERE Id=@Id;";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", akcijskaProdaja.Id);
                cmd.Parameters.AddWithValue("DatumPocetka", akcijskaProdaja.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavresetka", akcijskaProdaja.DatumZavresetka);
                cmd.Parameters.AddWithValue("Popust", akcijskaProdaja.Popust);
                cmd.Parameters.AddWithValue("Obrisan", akcijskaProdaja.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var ak in Projekat.Instance.AkcijskeProdaje)
                {
                    if (akcijskaProdaja.Id == ak.Id)
                    {
                        akcijskaProdaja.DatumPocetka = ak.DatumPocetka;
                        akcijskaProdaja.DatumZavresetka = ak.DatumZavresetka;
                        akcijskaProdaja.Popust = ak.Popust;
                        akcijskaProdaja.Obrisan = ak.Obrisan;

                        break;
                    }

                }
            }
        }
        public static ObservableCollection<AkcijskaProdaja> Sort(String orderBy, string orderHack)
        {
            var sveAkcije = new ObservableCollection<AkcijskaProdaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                DataSet ds = new DataSet();

                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.CommandText = "SELECT * FROM Akcije WHERE Obrisan=0 ORDER BY " + orderBy + "  " + orderHack;

                adapter.Fill(ds, "Akcije");
                foreach (DataRow row in ds.Tables["Akcije"].Rows)
                {
                    var akcija = new AkcijskaProdaja();
                    akcija.Id = int.Parse(row["Id"].ToString());
                    akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akcija.DatumZavresetka = DateTime.Parse(row["DatumZavresetka"].ToString());
                    akcija.Popust = int.Parse(row["Popust"].ToString());
                    akcija.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    sveAkcije.Add(akcija);

                }
            }
            return sveAkcije;
        }

        public static ObservableCollection<AkcijskaProdaja> Pretrazi(String searchBy, string searchQuery)
        {
            var sveAkcije = new ObservableCollection<AkcijskaProdaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                DataSet ds = new DataSet();

                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                cmd.CommandText = "SELECT * FROM Akcije WHERE Obrisan=0 AND " + searchBy + " LIKE" + " '%" + searchQuery + "%'";

                adapter.Fill(ds, "Akcije");
                foreach (DataRow row in ds.Tables["Akcije"].Rows)
                {
                    var akcija = new AkcijskaProdaja();
                    akcija.Id = int.Parse(row["Id"].ToString());
                    akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akcija.DatumZavresetka = DateTime.Parse(row["DatumZavresetka"].ToString());
                    akcija.Popust = int.Parse(row["Popust"].ToString());
                    akcija.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    sveAkcije.Add(akcija);

                }
            }
            return sveAkcije;
        }

        public static void Delete(AkcijskaProdaja akcijskaProdaja)
        {
            akcijskaProdaja.Obrisan = true;
            Update(akcijskaProdaja);
        }

        #endregion

    }
}