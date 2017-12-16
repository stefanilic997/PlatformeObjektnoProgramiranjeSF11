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
    public class TipNamestaja: INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private bool obrisan;


        public int Id
        {
            get { return id; }
            set {
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

        public static TipNamestaja GetById(int id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if (tipNamestaja.Id == id)
                {
                    return tipNamestaja;
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
        public static ObservableCollection<TipNamestaja> GetAll()
        {
            var tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand smd = con.CreateCommand();
                smd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan = @Obrisan";

                DataSet ds = new DataSet();//smestanje podataka koje dobijemo
                SqlDataAdapter adapter = new SqlDataAdapter();//podatke smestamo u dataset s njim
                adapter.Fill(ds, "TipNamestaja"); //ovde se izvrsava query nad bazom
                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    var tipNamestaja = new TipNamestaja();
                    tipNamestaja.Id = int.Parse(row["Id"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    tipoviNamestaja.Add(tipNamestaja);
                }

            }
            return tipoviNamestaja;

        }
        public static TipNamestaja Create(TipNamestaja tipNamestaja)
        {
            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO TipNamestaja (Naziv,Obrisan) VALUES(@Naziv,@Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", tipNamestaja.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tipNamestaja.Obrisan);

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                tipNamestaja.Id = newId;
            }
            Projekat.Instance.TipoviNamestaja.Add(tipNamestaja);
            return tipNamestaja;

        }
        public static void Update (TipNamestaja tipNamestaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja SET Naziv=@Naziv,Obrisan=@Obrisan WHERE Id=@Id;";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Id", tipNamestaja.Id);
                cmd.Parameters.AddWithValue("Naziv", tipNamestaja.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tipNamestaja.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (var tn in Projekat.Instance.TipoviNamestaja)
                {
                    if(tipNamestaja.Id == tn.Id)
                    {
                        tipNamestaja.Naziv = tn.Naziv;
                        tipNamestaja.Obrisan = tn.Obrisan;
                        break;
                    }

                }
            }
            
        }

        public static void Delete(TipNamestaja tipNamestaja)
        {
            tipNamestaja.Obrisan = true;
            Update(tipNamestaja);
        }

        #endregion
    }
    
}
