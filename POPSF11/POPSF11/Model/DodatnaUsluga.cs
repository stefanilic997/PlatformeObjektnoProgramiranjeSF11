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
    public class DodatnaUsluga : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private double cena;
        private bool obrisan;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get {
                return id;
                }
            set
            {

                OnPropertyChanged("Id");
                id = value;
            }
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

        public bool Obrisan
        {
            get { return obrisan; }
            set {
                OnPropertyChanged("Obrisan");
                obrisan = value; }
        }

        public static DodatnaUsluga GetById(int id)
        {
            foreach (var DodatnaUsluga in Projekat.Instance.DodatneUsluge)
            {
                if (DodatnaUsluga.Id == id)
                {
                    return DodatnaUsluga;
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
        public static ObservableCollection<DodatnaUsluga> GetAll()
        {
            var sveUsluge = new ObservableCollection<DodatnaUsluga>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "SELECT * FROM Usluge WHERE Obrisan =@Obrisan";
                cmd.Parameters.Add("@Obrisan", System.Data.SqlDbType.Bit).Value = 0;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Usluge");
                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var usluga = new DodatnaUsluga();
                    usluga.Id = int.Parse(row["Id"].ToString());
                    usluga.Naziv = row["Naziv"].ToString();
                    usluga.Cena = int.Parse(row["Cena"].ToString());
                    usluga.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    sveUsluge.Add(usluga);
                }
            }
            return sveUsluge;
        }
        public static DodatnaUsluga Create(DodatnaUsluga usluga)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))

            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = $"INSERT INTO Usluge (Naziv,Cena,Obrisan) VALUES(@Naziv,@Cena,@Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", usluga.Naziv);
                cmd.Parameters.AddWithValue("Cena", usluga.Cena);
                cmd.Parameters.AddWithValue("Obrisan", usluga.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                usluga.Id = newId;
            }
            Projekat.Instance.DodatneUsluge.Add(usluga);
            return usluga;
        }
        public static void Update (DodatnaUsluga usluga)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "UPDATE Usluge SET Naziv=@Naziv,Cena=@Cena,Obrisan=@Obrisan;";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", usluga.Id);
                cmd.Parameters.AddWithValue("Naziv", usluga.Naziv);
                cmd.Parameters.AddWithValue("Cena", usluga.Cena);
                cmd.Parameters.AddWithValue("Obrisan", usluga.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var du in Projekat.Instance.DodatneUsluge)
                {
                    if(usluga.Id == du.Id)
                    {
                        usluga.Naziv = du.Naziv;
                        usluga.Cena = du.Cena;
                        usluga.Obrisan = du.Obrisan;
                        break;

                    }
                }
            }
           

        }
        public static void Delete(DodatnaUsluga usluga)
        {
            usluga.Obrisan = true;
            Update(usluga);
        }
        #endregion
    }
}
