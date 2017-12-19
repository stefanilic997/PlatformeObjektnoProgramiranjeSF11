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
    public class Salon : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private string adresa;
        private string telefon;
        private string email;
        private string adresaSajta;
        private int pib;
        private int maticniBroj;
        private string brojZiroRacuna;
        private bool obrisan;
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
        public string Naziv
        {
            get { return naziv; }
            set
            {
                OnPropertyChanged("Naziv");
                naziv = value;
            }
        }

        public string Adresa
        {
            get { return adresa; }
            set {
                OnPropertyChanged("Adresa");
                adresa = value;
            }
        }

        public string Telefon
        {
            get { return telefon; }
            set {
                OnPropertyChanged("Telefon");

                telefon = value;
            }
        }


        public string Email
        {
            get { return email; }
            set {
                OnPropertyChanged("Email");
                email = value;
            }
        }


        public string AdresaSajta
        {
            get {
                OnPropertyChanged("Telefon");
                return adresaSajta; }
            set { adresaSajta = value; }
        }


        public int PIB
        {
            get {
                OnPropertyChanged("PIB");

                return pib; }
            set { pib = value; }
        }



        public int MaticniBroj
        {
            get { return maticniBroj; }
            set {
                OnPropertyChanged("MaticniBroj");
                maticniBroj = value; }
        }


        public string BrojZiroRacuna
        {
            get {
                OnPropertyChanged("PIB");
                return brojZiroRacuna; }
            set { brojZiroRacuna = value; }
        }

        public bool Obrisan
        {
            get {
                OnPropertyChanged("Obrisan");
                return obrisan; }
            set { obrisan = value; }
        }
        public static Salon GetById(int id)
        {
            foreach (var Salon in Projekat.Instance.Saloni)
            {
                if (Salon.Id == id)
                {
                    return Salon;
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
        public static ObservableCollection<Salon> GetAll()
        {
            var sviSaloni = new ObservableCollection<Salon>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))

            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Saloni WHERE Obrisan =@Obrisan;";
                cmd.Parameters.Add("@Obrisan", System.Data.SqlDbType.Bit).Value = 0;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Saloni");
                foreach (DataRow row in ds.Tables["Saloni"].Rows)
                {
                    var salon = new Salon();
                    salon.Id = int.Parse(row["Id"].ToString());
                    salon.Naziv = row["Naziv"].ToString();
                    salon.Adresa = row["Adresa"].ToString();
                    salon.Telefon = row["Telefon"].ToString();
                    salon.Email = row["Email"].ToString();
                    salon.AdresaSajta = row["AdresaSajta"].ToString();
                    salon.PIB = int.Parse(row["PIB"].ToString());
                    salon.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                    salon.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();
                    salon.Obrisan =bool.Parse(row["Obrisan"].ToString());
                    sviSaloni.Add(salon);

                }

            }
            return sviSaloni;
        }

        public static Salon Create (Salon salon)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = $"INSERT INTO Saloni (Naziv,Adresa,Telefon,Email,AdresaSajta,PIB,MaticniBroj,BrojZiroRacuna,Obrisan VALUES(@Naziv,@Adresa,@Telefo,@Email,@AdresaSajta,@PIB,@MaticniBroj,@BrojZiroRacuna,@Obrisan)";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", salon.Naziv);
                cmd.Parameters.AddWithValue("Adresa", salon.Adresa);
                cmd.Parameters.AddWithValue("Telefon", salon.Telefon);
                cmd.Parameters.AddWithValue("Email", salon.Email);
                cmd.Parameters.AddWithValue("AdresaSajta", salon.AdresaSajta);
                cmd.Parameters.AddWithValue("PIB", salon.PIB);
                cmd.Parameters.AddWithValue("MaticniBroj", salon.MaticniBroj);
                cmd.Parameters.AddWithValue("BrojZiroRacuna", salon.BrojZiroRacuna);
                cmd.Parameters.AddWithValue("Obrisan", salon.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                salon.Id = newId;


            }
            Projekat.Instance.Saloni.Add(salon);
            return salon;
        }
        public static void Update(Salon salon)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "UPDATE Saloni SET Naziv=@Naziv,Adresa=@Adresa,Telefon=@Telefon,Email=@Email,AdresaSajta=@AdresaSajta,PIB=@PIB,MaticniBroj=@MaticniBroj,BrojZiroRacuna=@BrojZiroRacuna,Obrisan=@Obrisan WHERE Id=@Id;" ;
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", salon.Id);
                cmd.Parameters.AddWithValue("Naziv", salon.Naziv);
                cmd.Parameters.AddWithValue("Adresa", salon.Adresa);
                cmd.Parameters.AddWithValue("Telefon", salon.Telefon);
                cmd.Parameters.AddWithValue("Email", salon.Email);
                cmd.Parameters.AddWithValue("AdresaSajta", salon.AdresaSajta);
                cmd.Parameters.AddWithValue("PIB", salon.PIB);
                cmd.Parameters.AddWithValue("MaticniBroj", salon.MaticniBroj);
                cmd.Parameters.AddWithValue("BrojZiroRacuna", salon.BrojZiroRacuna);
                cmd.Parameters.AddWithValue("Obrisan", salon.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var s in Projekat.Instance.Saloni)
                {
                    if (salon.Id == s.Id)
                    {
                        salon.Naziv = s.Naziv;
                        salon.Adresa = s.Adresa;
                        salon.Telefon = s.Telefon;
                        salon.Email = s.Email;
                        salon.AdresaSajta = s.AdresaSajta;
                        salon.PIB = s.PIB;
                        salon.MaticniBroj = s.MaticniBroj;
                        salon.BrojZiroRacuna = s.BrojZiroRacuna;
                        salon.Obrisan = s.Obrisan;

                        break;
                    }
                }
            }
        }

        public static void Delete(Salon salon)
        {
            salon.Obrisan = true;
            Update(salon);
        }
#endregion
    }

    
}
