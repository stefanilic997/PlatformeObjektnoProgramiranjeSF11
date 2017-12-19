using POP_SF_11_GUI.Model;
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
using System.Xml.Serialization;

namespace POP_SF_11_GUI.Model
{
    [Serializable]
    public class Namestaj : INotifyPropertyChanged
    {
        private int id;
        private bool obrisan;
        private string naziv;
        private double cena;
        private int kolicina;
        private int tipNamestajaId;
        private int akcijaId;
        public event PropertyChangedEventHandler PropertyChanged;
        private TipNamestaja tipNamestaja;
        private AkcijskaProdaja akcijskaProdaja;

        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(tipNamestajaId);
                }
                return tipNamestaja;
                }

            set {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
                }
        }

        public AkcijskaProdaja AkcijskaProdaja
        {
            get
            {
                if (akcijskaProdaja == null)
                {
                    akcijskaProdaja = AkcijskaProdaja.GetById(akcijaId);
                }
                return akcijskaProdaja;
            }

            set
            {
                akcijskaProdaja = value;
                AkcijaId = akcijskaProdaja.Id;
                OnPropertyChanged("AkcijskaProdaja");
            }
        }


        public int Id
        {
            get { return id; }
            set {
                OnPropertyChanged("Id");
                id = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set {
                OnPropertyChanged("Naziv");
                naziv = value; }
        }

        public double Cena
        {
            get { return cena; }
            set {
                OnPropertyChanged("Cena");
                cena = value; }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set {
                OnPropertyChanged("Kolicina");
                kolicina = value; }
        }

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                OnPropertyChanged("TipNamestajaId");

                tipNamestajaId = value; }
        }
        

        public int AkcijaId
        {
            get { return akcijaId; }
            set {
                OnPropertyChanged("AkcijaId");

                akcijaId = value;
            }
        }


        public bool Obrisan
        {
            get { return obrisan; }
            set {
                OnPropertyChanged("Obrisan");
                obrisan = value; }
        }
        
        public static Namestaj GetById(int id)
        {
            foreach (var Namestaj in Projekat.Instance.sviNamestaji)
            {
                if (Namestaj.Id == id)
                {
                    return Namestaj;
                }

            }
            return null;

        }
        public override string ToString()
        {
            return Naziv;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<Namestaj> GetAll()
        {
            var sviNamestaji = new ObservableCollection<Namestaj>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan = @Obrisan";
                cmd.Parameters.Add("@Obrisan", System.Data.SqlDbType.Bit).Value = 0;

                DataSet ds = new DataSet();//smestanje podataka koje dobijemo
                SqlDataAdapter adapter = new SqlDataAdapter();//podatke smestamo u dataset s njim
                adapter.Fill(ds, "Namestaj"); //ovde se izvrsava query nad bazom
                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var namestaj = new Namestaj();
                    namestaj.Id = int.Parse(row["Id"].ToString());
                    namestaj.Naziv = row["Naziv"].ToString();
                    namestaj.Cena = int.Parse(row["Cena"].ToString());
                    namestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                    namestaj.TipNamestajaId = int.Parse(row["TipNamestajaId"].ToString());
                    namestaj.AkcijaId = int.Parse(row["AkcijaId"].ToString());
                    namestaj.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    sviNamestaji.Add(namestaj);
                }

            }
            return sviNamestaji;

        }
        public static Namestaj Create(Namestaj namestaj)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // vise komada namestaja u racunu, ni kolicina kod dodavanja
                cmd.CommandText = $"INSERT INTO Namestaj (Naziv,Cena,Kolicina,TipNamestajaId,AkcijaId,Obrisan) VALUES(@Naziv,@Cena,@Kolicina,@TipNamestajaId,@AkcijaId,@Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", namestaj.Naziv);
                cmd.Parameters.AddWithValue("Cena", namestaj.Cena);
                cmd.Parameters.AddWithValue("Kolicina", namestaj.Kolicina);
                cmd.Parameters.AddWithValue("TipNamestajaId", namestaj.TipNamestajaId);
                cmd.Parameters.AddWithValue("AkcijaId", namestaj.AkcijaId);
                cmd.Parameters.AddWithValue("Obrisan", namestaj.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                namestaj.Id = newId;
            }
            Projekat.Instance.sviNamestaji.Add(namestaj);
            return namestaj;

        }
        public static void Update(Namestaj namestaj)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "UPDATE Namestaj SET Naziv=@Naziv,Cena=@Cena,Kolicina=@Kolicina,TipNamestajaId=@TipNamestajaId,AkcijaId=@AkcijaId,Obrisan=@Obrisan WHERE Id=@Id;";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", namestaj.Id);
                cmd.Parameters.AddWithValue("Naziv", namestaj.Naziv);
                cmd.Parameters.AddWithValue("Cena", namestaj.Cena);
                cmd.Parameters.AddWithValue("Kolicina", namestaj.Kolicina);
                cmd.Parameters.AddWithValue("TipNamestajaId", namestaj.TipNamestajaId);
                cmd.Parameters.AddWithValue("AkcijaId", namestaj.AkcijaId);
                cmd.Parameters.AddWithValue("Obrisan", namestaj.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var tn in Projekat.Instance.sviNamestaji)
                {
                    if (namestaj.Id == tn.Id)
                    {
                        namestaj.Naziv = tn.Naziv;
                        namestaj.Cena = tn.Cena;
                        namestaj.Kolicina = tn.Kolicina;
                        namestaj.TipNamestajaId = tn.TipNamestajaId;
                        namestaj.AkcijaId = tn.AkcijaId;
                        namestaj.Obrisan = tn.Obrisan;

                        break;
                    }

                }
            }

        }

        public static void Delete(Namestaj namestaj)
        {
            namestaj.Obrisan = true;
            Update(namestaj);
        }

        #endregion
    }

}
