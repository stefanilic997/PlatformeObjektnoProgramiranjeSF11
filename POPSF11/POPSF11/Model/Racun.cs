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
    public class Racun : INotifyPropertyChanged
    {
        
        public const double PDV = 0.02;
        private int id;
        private int kolicina;
        private List<StavkaProdajeNamestaj> listaNamestaja;
        private DateTime datumProdaje;
        private string brojRacuna;
        private string kupac;
        private List<StavkaProdajeDodatnaUsluga> listaDodatnihUsluga;
        private double ukupnaCena;
        private bool obrisan;


        public event PropertyChangedEventHandler PropertyChanged;


        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value; }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set {
                OnPropertyChanged("Kolicina");
                kolicina = value; }
        }

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set { datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }


        public string BrojRacuna
        {
            get { return brojRacuna; }
            set { brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        public string Kupac
        {
            get { return kupac; }
            set { kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        
        public List<StavkaProdajeDodatnaUsluga> DodatnaUsluga { get; set; }

        
        public List<StavkaProdajeNamestaj> ListaNamestaja { get; set; }



        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set { ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }
        public static Racun GetById(int id)
        {
            foreach (var Racun in Projekat.Instance.Racuni)
            {
                if (Racun.Id == id)
                {
                    return Racun;
                }

            }
            return null;

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region Database
        public static ObservableCollection<Racun> GetAll()
        {
            var racun = new ObservableCollection<Racun>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Racuni WHERE Obrisan = 0";

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds, "Racuni"); 
                foreach (DataRow row in ds.Tables["Racuni"].Rows)
                {
                    var r = new Racun();
                    r.Id = int.Parse(row["Id"].ToString());
                    r.datumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    r.Kupac = row["Kupac"].ToString();
                    r.UkupnaCena = double.Parse(row["UkupnaCena"].ToString());

                    racun.Add(r);

                }
                return racun;
            }
        }
        public static Racun Create(Racun racun)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO Racuni (DatumProdaje,Kupac,UkupnaCena) VALUES(@Dp,@Kupac,@UkupnaCena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("DatumProdaje", racun.DatumProdaje);
                cmd.Parameters.AddWithValue("Kupac", racun.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", racun.UkupnaCena);
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); 
                racun.Id = newId;


            }
            Projekat.Instance.Racuni.Add(racun);
            return racun;
        }
        public static void Update(Racun racun)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Racuni SET DatumProdaje=@DatumProdaje,Kupac=@Kupac,UkupnaCena=@UkupnaCena";
                cmd.Parameters.AddWithValue("Id", racun.Id);
                cmd.Parameters.AddWithValue("Dp", racun.DatumProdaje);
                cmd.Parameters.AddWithValue("Kupac", racun.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", racun.UkupnaCena);

                cmd.ExecuteNonQuery();

                foreach (var izmenjeniRacun in Projekat.Instance.Racuni)
                {
                    if (izmenjeniRacun.Id == racun.Id)
                    {
                        izmenjeniRacun.DatumProdaje = racun.DatumProdaje;
                        izmenjeniRacun.Kupac = racun.Kupac;
                        izmenjeniRacun.UkupnaCena = racun.UkupnaCena;
                        break;
                    }
                }
            }


        }
        public static void Delete(Racun racun)
        {
            racun.Obrisan = true;
            Update(racun);
        }

        #endregion
    }

}
