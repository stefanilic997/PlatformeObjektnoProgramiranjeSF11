﻿
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
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string ime;
        private string prezime;
        private string korisnickoIme;
        private string lozinka;
        private TipKorisnika tipKorisnika;
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

        public string Ime
        {
            get { return ime; }
            set
            {
                OnPropertyChanged("Ime");
                ime = value;
            }
        }

        public string Prezime
        {
            get { return prezime; }
            set
            {
                OnPropertyChanged("Prezime");
                prezime = value;
            }
        }


        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set
            {
                OnPropertyChanged("KorisnickoIme");
                korisnickoIme = value;
            }
        }

        public string Lozinka
        {
            get { return lozinka; }
            set
            {
                OnPropertyChanged("Lozinka");
                lozinka = value;
            }
        }

        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set
            {
                OnPropertyChanged("TipKorisnika");
                tipKorisnika = value;
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






        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        public static ObservableCollection<Korisnik> GetAll()
        {
            var sviKorisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnici WHERE Obrisan=0";

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Korisnici");
                foreach (DataRow row in ds.Tables["Korisnici"].Rows)
                {
                    var korisnik = new Korisnik();
                    korisnik.Id = int.Parse(row["Id"].ToString());
                    korisnik.Ime = row["Ime"].ToString();
                    korisnik.Prezime = row["Prezime"].ToString();
                    korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                    korisnik.Lozinka = row["Lozinka"].ToString();
                    //RESENO//nije reseno,u bazi koristi enum kao int,  ima na stackoverflowu
                    korisnik.TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), row["TipKorisnika"].ToString());
                    korisnik.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    sviKorisnici.Add(korisnik);

                }

            }
            return sviKorisnici;
        }
        public static Korisnik Create(Korisnik korisnik)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = $"INSERT INTO Korisnici (Ime,Prezime,KorisnickoIme,Lozinka,TipKorisnika,Obrisan) VALUES(@Ime,@Prezime,@KorisnickoIme,@Lozinka,@TipKorisnika,@Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Ime", korisnik.Ime);
                cmd.Parameters.AddWithValue("Prezime", korisnik.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", korisnik.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", korisnik.Lozinka);
                cmd.Parameters.AddWithValue("TipKorisnika", korisnik.TipKorisnika.ToString());
                cmd.Parameters.AddWithValue("Obrisan", korisnik.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                //System.Data.SqlClient.SqlException:
                //'The INSERT statement conflicted with the CHECK constraint "CK__Korisnici__TipKo__25869641". 
                //The conflict occurred in database "POP", table "dbo.Korisnici", column 'TipKorisnika'.
                // The statement has been terminated.'
                korisnik.Id = newId;
            }
            Projekat.Instance.Korisnici.Add(korisnik);
            return korisnik;
        }
        public static void Update(Korisnik korisnik)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "UPDATE Korisnici SET Ime=@Ime,Prezime=@Prezime,KorisnickoIme=@KorisnickoIme,Lozinka=@Lozinka,TipKorisnika=@TipKorisnika,Obrisan=@Obrisan WHERE Id=@Id;";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", korisnik.Id);
                cmd.Parameters.AddWithValue("Ime", korisnik.Ime);
                cmd.Parameters.AddWithValue("Prezime", korisnik.Prezime);
                cmd.Parameters.AddWithValue("KorisnickoIme", korisnik.KorisnickoIme);
                cmd.Parameters.AddWithValue("Lozinka", korisnik.Lozinka);
                cmd.Parameters.AddWithValue("TipKorisnika", korisnik.TipKorisnika.ToString());
                cmd.Parameters.AddWithValue("Obrisan", korisnik.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var k in Projekat.Instance.Korisnici)
                {
                    if (korisnik.Id == k.Id)
                    {
                        korisnik.Ime = k.Ime;
                        korisnik.Prezime = k.Prezime;
                        korisnik.KorisnickoIme = k.KorisnickoIme;
                        korisnik.Lozinka = k.Lozinka;
                        korisnik.TipKorisnika = k.TipKorisnika;
                        korisnik.Obrisan = k.Obrisan;

                        break;
                    }
                }
            }
        }
        public static ObservableCollection<Korisnik> Sort(string orderBy, string orderHack)
        {
            var sviKorisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnici WHERE Obrisan=0 ORDER BY " + orderBy + " " + orderHack;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Korisnici");
                foreach (DataRow row in ds.Tables["Korisnici"].Rows)
                {
                    var korisnik = new Korisnik();
                    korisnik.Id = int.Parse(row["Id"].ToString());
                    korisnik.Ime = row["Ime"].ToString();
                    korisnik.Prezime = row["Prezime"].ToString();
                    korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                    korisnik.Lozinka = row["Lozinka"].ToString();
                    //RESENO//nije reseno,u bazi koristi enum kao int,  ima na stackoverflowu
                    korisnik.TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), row["TipKorisnika"].ToString());
                    korisnik.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    sviKorisnici.Add(korisnik);

                }

            }
            return sviKorisnici;
        }

        public static ObservableCollection<Korisnik> Pretrazi(string searchBy, string searchQuery)
        {
            var sviKorisnici = new ObservableCollection<Korisnik>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnici WHERE Obrisan=0 AND " + searchBy + " LIKE" + " '%" + searchQuery + "%'";

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Korisnici");
                foreach (DataRow row in ds.Tables["Korisnici"].Rows)
                {
                    var korisnik = new Korisnik();
                    korisnik.Id = int.Parse(row["Id"].ToString());
                    korisnik.Ime = row["Ime"].ToString();
                    korisnik.Prezime = row["Prezime"].ToString();
                    korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                    korisnik.Lozinka = row["Lozinka"].ToString();
                    //RESENO//nije reseno,u bazi koristi enum kao int,  ima na stackoverflowu
                    korisnik.TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), row["TipKorisnika"].ToString());
                    korisnik.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    sviKorisnici.Add(korisnik);

                }

            }
            return sviKorisnici;
        }

        public static void Delete(Korisnik korisnik)
        {
            korisnik.Obrisan = true;
            Update(korisnik);

        }
        #endregion
    }

}