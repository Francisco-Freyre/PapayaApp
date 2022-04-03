using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alimentos : ContentPage
    {
        public bool platano, fresa, manzana, blackberry, pina, papaya, mandarina, naranja, kiwi, mango, sandia, pera, durazno, uva, granada, calabazita, zana,
                    jiacama, pepino, espinaca, pimiento, cebolla, tomate, brocoli, coliflor, lechuga, apio, champiñon, betabel, esparrago, frijol, garbanzo, lenteja,
                    haba, chicharo, alubias, soya, humus, harina, pollo, carne, pescado, atun, huevo, pavo, cerdo, jamon, tofu, leche, yogur, queso, bebida, aguacate,
                    cacahuate, almendra, pecana, india, nueces, aceituna, chia, aceite, gelatina, chocolate, caramelo, ate, chicle, miel, catsup, aderezo, mayonesa, mermelada,
                    pistache, mantequilla, avellana, margarina, quesocrema, vinagreta, cremaacida, quesopanela, quesocot, quesoasadero, quesomoz, lechecoco,
                    lechealmendra, lechesoya, sardina, mariscos, nopal, chayote, acelga, ejote, porto, guayaba, higo, melon, toronja, tamarindo, tuna;
        List<string> alimentos = new List<string>();
        public Alimentos()
        {
            InitializeComponent();
        }
        public class Excluidos
        {
            public int idCliente { get; set; }

            public List<string> alimentos { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        private async void btnContinuar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Excluidos excluido = new Excluidos
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    alimentos = alimentos
                };
                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(excluido);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado)
                    {
                        await Navigation.PushAsync(new Bienvenida());
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
            }
            
        }

        private void btnPlatano_Clicked(object sender, EventArgs e)
        {
            if (platano)
            {
                btnPlatano.BorderColor = Color.White;
                platano = false;
                alimentos.Remove("Platano");
            }
            else
            {
                btnPlatano.BorderColor = Color.Gold;
                platano = true;
                alimentos.Add("Platano");
            }
        }

        private void btnLeche_Clicked(object sender, EventArgs e)
        {
            if (leche)
            {
                btnLeche.BorderColor = Color.White;
                leche = false;
                alimentos.Remove("Leche");
            }
            else
            {
                btnLeche.BorderColor = Color.Gold;
                leche = true;
                alimentos.Add("Leche");
            }
        }

        private void btnYogur_Clicked(object sender, EventArgs e)
        {
            if (yogur)
            {
                btnYogur.BorderColor = Color.White;
                yogur = false;
                alimentos.Remove("Yogur");
            }
            else
            {
                btnYogur.BorderColor = Color.Gold;
                yogur = true;
                alimentos.Add("Yogur");
            }
        }

        private void btnQueso_Clicked(object sender, EventArgs e)
        {
            if (queso)
            {
                btnQueso.BorderColor = Color.White;
                queso = false;
                alimentos.Remove("Queso");
            }
            else
            {
                btnQueso.BorderColor = Color.Gold;
                queso = true;
                alimentos.Add("Queso");
            }
        }

        private void btnBebida_Clicked(object sender, EventArgs e)
        {
            if (bebida)
            {
                btnBebida.BorderColor = Color.White;
                bebida = false;
                alimentos.Remove("Bebida vegetal");
            }
            else
            {
                btnBebida.BorderColor = Color.Gold;
                bebida = true;
                alimentos.Add("Bebida vegetal");
            }
        }

        private void btnAguacate_Clicked(object sender, EventArgs e)
        {
            if (aguacate)
            {
                btnAguacate.BorderColor = Color.White;
                aguacate = false;
                alimentos.Remove("Aguacate");
            }
            else
            {
                btnAguacate.BorderColor = Color.Gold;
                aguacate = true;
                alimentos.Add("Aguacate");
            }
        }

        private void btnCacahuate_Clicked(object sender, EventArgs e)
        {
            if (cacahuate)
            {
                btnCacahuate.BorderColor = Color.White;
                cacahuate = false;
                alimentos.Remove("Cacahuate");
            }
            else
            {
                btnCacahuate.BorderColor = Color.Gold;
                cacahuate = true;
                alimentos.Add("Cacahuate");
            }
        }

        private void btnAlmendras_Clicked(object sender, EventArgs e)
        {
            if (almendra)
            {
                btnAlmendras.BorderColor = Color.White;
                almendra = false;
                alimentos.Remove("Almendra");
            }
            else
            {
                btnAlmendras.BorderColor = Color.Gold;
                almendra = true;
                alimentos.Add("Almendra");
            }
        }

        private void btnPecana_Clicked(object sender, EventArgs e)
        {
            if (pecana)
            {
                btnPecana.BorderColor = Color.White;
                pecana = false;
                alimentos.Remove("Nuez pecana");
            }
            else
            {
                btnPecana.BorderColor = Color.Gold;
                pecana = true;
                alimentos.Add("Nuez pecana");
            }
        }

        private void btnNuezIndia_Clicked(object sender, EventArgs e)
        {
            if (india)
            {
                btnNuezIndia.BorderColor = Color.White;
                india = false;
                alimentos.Remove("Nuez india");
            }
            else
            {
                btnNuezIndia.BorderColor = Color.Gold;
                india = true;
                alimentos.Add("Nuez india");
            }
        }

        private void btnNuez_Clicked(object sender, EventArgs e)
        {
            if (nueces)
            {
                btnNuez.BorderColor = Color.White;
                nueces = false;
                alimentos.Remove("Nuez");
            }
            else
            {
                btnNuez.BorderColor = Color.Gold;
                nueces = true;
                alimentos.Add("Nuez");
            }
        }

        private void btnAceituna_Clicked(object sender, EventArgs e)
        {
            if (aceituna)
            {
                btnAceituna.BorderColor = Color.White;
                aceituna = false;
                alimentos.Remove("Aceituna");
            }
            else
            {
                btnAceituna.BorderColor = Color.Gold;
                aceituna = true;
                alimentos.Add("Aceituna");
            }
        }

        private void btnChia_Clicked(object sender, EventArgs e)
        {
            if (chia)
            {
                btnChia.BorderColor = Color.White;
                chia = false;
                alimentos.Remove("Chia");
            }
            else
            {
                btnChia.BorderColor = Color.Gold;
                chia = true;
                alimentos.Add("Chia");
            }
        }

        private void btnAceite_Clicked(object sender, EventArgs e)
        {
            if (aceite)
            {
                btnAceite.BorderColor = Color.White;
                aceite = false;
                alimentos.Remove("Aceite vegetal");
            }
            else
            {
                btnAceite.BorderColor = Color.Gold;
                aceite = true;
                alimentos.Add("Aceite vegetal");
            }
        }

        private void btnGelatina_Clicked(object sender, EventArgs e)
        {
            if (gelatina)
            {
                btnGelatina.BorderColor = Color.White;
                gelatina = false;
                alimentos.Remove("Gelatina");
            }
            else
            {
                btnGelatina.BorderColor = Color.Gold;
                gelatina = true;
                alimentos.Add("Gelatina");
            }
        }

        private void btnChocolate_Clicked(object sender, EventArgs e)
        {
            if (chocolate)
            {
                btnChocolate.BorderColor = Color.White;
                chocolate = false;
                alimentos.Remove("Chocolate");
            }
            else
            {
                btnChocolate.BorderColor = Color.Gold;
                chocolate = true;
                alimentos.Add("Chocolate");
            }
        }

        private void btnCaramelo_Clicked(object sender, EventArgs e)
        {
            if (caramelo)
            {
                btnCaramelo.BorderColor = Color.White;
                caramelo = false;
                alimentos.Remove("Caramelo");
            }
            else
            {
                btnCaramelo.BorderColor = Color.Gold;
                caramelo = true;
                alimentos.Add("Caramelo");
            }
        }

        private void btnAte_Clicked(object sender, EventArgs e)
        {
            if (ate)
            {
                btnAte.BorderColor = Color.White;
                ate = false;
                alimentos.Remove("Ate");
            }
            else
            {
                btnAte.BorderColor = Color.Gold;
                ate = true;
                alimentos.Add("Ate");
            }
        }

        private void btnChicle_Clicked(object sender, EventArgs e)
        {
            if (chicle)
            {
                btnChicle.BorderColor = Color.White;
                chicle = false;
                alimentos.Remove("Chicle");
            }
            else
            {
                btnChicle.BorderColor = Color.Gold;
                chicle = true;
                alimentos.Add("Chicle");
            }
        }

        private void btnMiel_Clicked(object sender, EventArgs e)
        {
            if (miel)
            {
                btnMiel.BorderColor = Color.White;
                miel = false;
                alimentos.Remove("Miel");
            }
            else
            {
                btnMiel.BorderColor = Color.Gold;
                miel = true;
                alimentos.Add("Miel");
            }
        }

        private void btnCatsup_Clicked(object sender, EventArgs e)
        {
            if (catsup)
            {
                btnCatsup.BorderColor = Color.White;
                catsup = false;
                alimentos.Remove("Catsup");
            }
            else
            {
                btnCatsup.BorderColor = Color.Gold;
                catsup = true;
                alimentos.Add("Catsup");
            }
        }

        private void btnAderezo_Clicked(object sender, EventArgs e)
        {
            if (aderezo)
            {
                btnAderezo.BorderColor = Color.White;
                aderezo = false;
                alimentos.Remove("Aderezo");
            }
            else
            {
                btnAderezo.BorderColor = Color.Gold;
                aderezo = true;
                alimentos.Add("Aderezo");
            }
        }

        private void btnMayonesa_Clicked(object sender, EventArgs e)
        {
            if (mayonesa)
            {
                btnMayonesa.BorderColor = Color.White;
                mayonesa = false;
                alimentos.Remove("Mayonesa");
            }
            else
            {
                btnMayonesa.BorderColor = Color.Gold;
                mayonesa = true;
                alimentos.Add("Mayonesa");
            }
        }

        private void btnMermelada_Clicked(object sender, EventArgs e)
        {
            if (mermelada)
            {
                btnMermelada.BorderColor = Color.White;
                mermelada = false;
                alimentos.Remove("Mermelada");
            }
            else
            {
                btnMermelada.BorderColor = Color.Gold;
                mermelada = true;
                alimentos.Add("Mermelada");
            }
        }

        private void btnCalabacita_Clicked(object sender, EventArgs e)
        {
            if (calabazita)
            {
                btnCalabacita.BorderColor = Color.White;
                calabazita = false;
                alimentos.Remove("Calabacita");
            }
            else
            {
                btnCalabacita.BorderColor = Color.Gold;
                calabazita = true;
                alimentos.Add("Calabacita");
            }
        }

        private void btnZanahoria_Clicked(object sender, EventArgs e)
        {
            if (zana)
            {
                btnZanahoria.BorderColor = Color.White;
                zana = false;
                alimentos.Remove("Zanahoria");
            }
            else
            {
                btnZanahoria.BorderColor = Color.Gold;
                zana = true;
                alimentos.Add("Zanahoria");
            }
        }

        private void btnJicama_Clicked(object sender, EventArgs e)
        {
            if (jiacama)
            {
                btnJicama.BorderColor = Color.White;
                jiacama = false;
                alimentos.Remove("Jicama");
            }
            else
            {
                btnJicama.BorderColor = Color.Gold;
                jiacama = true;
                alimentos.Add("Jicama");
            }
        }

        private void btnPepino_Clicked(object sender, EventArgs e)
        {
            if (pepino)
            {
                btnPepino.BorderColor = Color.White;
                pepino = false;
                alimentos.Remove("Pepino");
            }
            else
            {
                btnPepino.BorderColor = Color.Gold;
                pepino = true;
                alimentos.Add("Pepino");
            }
        }

        private void btnEspinaca_Clicked(object sender, EventArgs e)
        {
            if (espinaca)
            {
                btnEspinaca.BorderColor = Color.White;
                espinaca = false;
                alimentos.Remove("Espinaca");
            }
            else
            {
                btnEspinaca.BorderColor = Color.Gold;
                espinaca = true;
                alimentos.Add("Espinaca");
            }
        }

        private void btnPimiento_Clicked(object sender, EventArgs e)
        {
            if (pimiento)
            {
                btnPimiento.BorderColor = Color.White;
                pimiento = false;
                alimentos.Remove("Pimiento");
            }
            else
            {
                btnPimiento.BorderColor = Color.Gold;
                pimiento = true;
                alimentos.Add("Pimiento");
            }
        }

        private void btnCebolla_Clicked(object sender, EventArgs e)
        {
            if (cebolla)
            {
                btnCebolla.BorderColor = Color.White;
                cebolla = false;
                alimentos.Remove("Cebolla");
            }
            else
            {
                btnCebolla.BorderColor = Color.Gold;
                cebolla = true;
                alimentos.Add("Cebolla");
            }
        }

        private void btnTomate_Clicked(object sender, EventArgs e)
        {
            if (tomate)
            {
                btnTomate.BorderColor = Color.White;
                tomate = false;
                alimentos.Remove("Tomate");
            }
            else
            {
                btnTomate.BorderColor = Color.Gold;
                tomate = true;
                alimentos.Add("Tomate");
            }
        }

        private void btnBrocoli_Clicked(object sender, EventArgs e)
        {
            if (brocoli)
            {
                btnBrocoli.BorderColor = Color.White;
                brocoli = false;
                alimentos.Remove("Brocoli");
            }
            else
            {
                btnBrocoli.BorderColor = Color.Gold;
                brocoli = true;
                alimentos.Add("Brocoli");
            }
        }

        private void btnColiflor_Clicked(object sender, EventArgs e)
        {
            if (coliflor)
            {
                btnColiflor.BorderColor = Color.White;
                coliflor = false;
                alimentos.Remove("Coliflor");
            }
            else
            {
                btnColiflor.BorderColor = Color.Gold;
                coliflor = true;
                alimentos.Add("Coliflor");
            }
        }

        private void btnLechuga_Clicked(object sender, EventArgs e)
        {
            if (lechuga)
            {
                btnLechuga.BorderColor = Color.White;
                lechuga = false;
                alimentos.Remove("Lechuga");
            }
            else
            {
                btnLechuga.BorderColor = Color.Gold;
                lechuga = true;
                alimentos.Add("Lechuga");
            }
        }

        private void btnApio_Clicked(object sender, EventArgs e)
        {
            if (apio)
            {
                btnApio.BorderColor = Color.White;
                apio = false;
                alimentos.Remove("Apio");
            }
            else
            {
                btnApio.BorderColor = Color.Gold;
                apio = true;
                alimentos.Add("Apio");
            }
        }

        private void btnChampi_Clicked(object sender, EventArgs e)
        {
            if (champiñon)
            {
                btnChampi.BorderColor = Color.White;
                champiñon = false;
                alimentos.Remove("Champiñones");
            }
            else
            {
                btnChampi.BorderColor = Color.Gold;
                champiñon = true;
                alimentos.Add("Champiñones");
            }
        }

        private void btnBetabel_Clicked(object sender, EventArgs e)
        {
            if (betabel)
            {
                btnBetabel.BorderColor = Color.White;
                betabel = false;
                alimentos.Remove("Betabel");
            }
            else
            {
                btnBetabel.BorderColor = Color.Gold;
                betabel = true;
                alimentos.Add("Betabel");
            }
        }

        private void btnEsparragos_Clicked(object sender, EventArgs e)
        {
            if (esparrago)
            {
                btnEsparragos.BorderColor = Color.White;
                esparrago = false;
                alimentos.Remove("Esparragos");
            }
            else
            {
                btnEsparragos.BorderColor = Color.Gold;
                esparrago = true;
                alimentos.Add("Esparragos");
            }
        }

        private void btnFrijol_Clicked(object sender, EventArgs e)
        {
            if (frijol)
            {
                btnFrijol.BorderColor = Color.White;
                frijol = false;
                alimentos.Remove("Frijol");
            }
            else
            {
                btnFrijol.BorderColor = Color.Gold;
                frijol = true;
                alimentos.Add("Frijol");
            }
        }

        private void btnGarbanzo_Clicked(object sender, EventArgs e)
        {
            if (garbanzo)
            {
                btnGarbanzo.BorderColor = Color.White;
                garbanzo = false;
                alimentos.Remove("Garbanzo");
            }
            else
            {
                btnGarbanzo.BorderColor = Color.Gold;
                garbanzo = true;
                alimentos.Add("Garbanzo");
            }
        }

        private void btnLenteja_Clicked(object sender, EventArgs e)
        {
            if (lenteja)
            {
                btnLenteja.BorderColor = Color.White;
                lenteja = false;
                alimentos.Remove("Lentejas");
            }
            else
            {
                btnLenteja.BorderColor = Color.Gold;
                lenteja = true;
                alimentos.Add("Lentejas");
            }
        }

        private void btnHaba_Clicked(object sender, EventArgs e)
        {
            if (haba)
            {
                btnHaba.BorderColor = Color.White;
                haba = false;
                alimentos.Remove("Haba");
            }
            else
            {
                btnHaba.BorderColor = Color.Gold;
                haba = true;
                alimentos.Add("Haba");
            }
        }

        private void btnChicharo_Clicked(object sender, EventArgs e)
        {
            if (chicharo)
            {
                btnChicharo.BorderColor = Color.White;
                chicharo = false;
                alimentos.Remove("Chicharos");
            }
            else
            {
                btnChicharo.BorderColor = Color.Gold;
                chicharo = true;
                alimentos.Add("Chicharos");
            }
        }

        private void btnAlubias_Clicked(object sender, EventArgs e)
        {
            if (alubias)
            {
                btnAlubias.BorderColor = Color.White;
                alubias = false;
                alimentos.Remove("Alubias");
            }
            else
            {
                btnAlubias.BorderColor = Color.Gold;
                alubias = true;
                alimentos.Add("Alubias");
            }
        }

        private void btnSoya_Clicked(object sender, EventArgs e)
        {
            if (soya)
            {
                btnSoya.BorderColor = Color.White;
                soya = false;
                alimentos.Remove("Soya");
            }
            else
            {
                btnSoya.BorderColor = Color.Gold;
                soya = true;
                alimentos.Add("Soya");
            }
        }

        private void btnHumus_Clicked(object sender, EventArgs e)
        {
            if (humus)
            {
                btnHumus.BorderColor = Color.White;
                humus = false;
                alimentos.Remove("Hummus");
            }
            else
            {
                btnHumus.BorderColor = Color.Gold;
                humus = true;
                alimentos.Add("Hummus");
            }
        }

        private void btnHarina_Clicked(object sender, EventArgs e)
        {
            if (harina)
            {
                btnHarina.BorderColor = Color.White;
                harina = false;
                alimentos.Remove("Harina");
            }
            else
            {
                btnHarina.BorderColor = Color.Gold;
                harina = true;
                alimentos.Add("Harina");
            }
        }

        private void btnPollo_Clicked(object sender, EventArgs e)
        {
            if (pollo)
            {
                btnPollo.BorderColor = Color.White;
                pollo = false;
                alimentos.Remove("Pollo");
            }
            else
            {
                btnPollo.BorderColor = Color.Gold;
                pollo = true;
                alimentos.Add("Pollo");
            }
        }

        private void btnCarne_Clicked(object sender, EventArgs e)
        {
            if (carne)
            {
                btnCarne.BorderColor = Color.White;
                carne = false;
                alimentos.Remove("Carne");
            }
            else
            {
                btnCarne.BorderColor = Color.Gold;
                carne = true;
                alimentos.Add("Carne");
            }
        }

        private void btnPescado_Clicked(object sender, EventArgs e)
        {
            if (pescado)
            {
                btnPescado.BorderColor = Color.White;
                pescado = false;
                alimentos.Remove("Pescado");
            }
            else
            {
                btnPescado.BorderColor = Color.Gold;
                pescado = true;
                alimentos.Add("Pescado");
            }
        }

        private void btnAtun_Clicked(object sender, EventArgs e)
        {
            if (atun)
            {
                btnAtun.BorderColor = Color.White;
                atun = false;
                alimentos.Remove("Atun");
            }
            else
            {
                btnAtun.BorderColor = Color.Gold;
                atun = true;
                alimentos.Add("Atun");
            }
        }

        private void btnHuevo_Clicked(object sender, EventArgs e)
        {
            if (huevo)
            {
                btnHuevo.BorderColor = Color.White;
                huevo = false;
                alimentos.Remove("Huevo");
            }
            else
            {
                btnHuevo.BorderColor = Color.Gold;
                huevo = true;
                alimentos.Add("Huevo");
            }
        }

        private void btnPavo_Clicked(object sender, EventArgs e)
        {
            if (pavo)
            {
                btnPavo.BorderColor = Color.White;
                pavo = false;
                alimentos.Remove("Pavo");
            }
            else
            {
                btnPavo.BorderColor = Color.Gold;
                pavo = true;
                alimentos.Add("Pavo");
            }
        }

        private void btnCerdo_Clicked(object sender, EventArgs e)
        {
            if (cerdo)
            {
                btnCerdo.BorderColor = Color.White;
                cerdo = false;
                alimentos.Remove("Cerdo");
            }
            else
            {
                btnCerdo.BorderColor = Color.Gold;
                cerdo = true;
                alimentos.Add("Cerdo");
            }
        }

        private void btnJamon_Clicked(object sender, EventArgs e)
        {
            if (jamon)
            {
                btnJamon.BorderColor = Color.White;
                jamon = false;
                alimentos.Remove("Jamon");
            }
            else
            {
                btnJamon.BorderColor = Color.Gold;
                jamon = true;
                alimentos.Add("Jamon");
            }
        }

        private void btnTofu_Clicked(object sender, EventArgs e)
        {
            if (tofu)
            {
                btnTofu.BorderColor = Color.White;
                tofu = false;
                alimentos.Remove("Tofu");
            }
            else
            {
                btnTofu.BorderColor = Color.Gold;
                tofu = true;
                alimentos.Add("Tofu");
            }
        }

        private void btnFresa_Clicked(object sender, EventArgs e)
        {
            if (fresa)
            {
                btnFresa.BorderColor = Color.White;
                fresa = false;
                alimentos.Remove("Fresa");
            }
            else
            {
                btnFresa.BorderColor = Color.Gold;
                fresa = true;
                alimentos.Add("Fresa");
            }
        }

        private void btnManzana_Clicked(object sender, EventArgs e)
        {
            if (manzana)
            {
                btnManzana.BorderColor = Color.White;
                manzana = false;
                alimentos.Remove("Manzana");
            }
            else
            {
                btnManzana.BorderColor = Color.Gold;
                manzana = true;
                alimentos.Add("Manzana");
            }
        }

        private void btnBlackberry_Clicked(object sender, EventArgs e)
        {
            if (blackberry)
            {
                btnBlackberry.BorderColor = Color.White;
                blackberry = false;
                alimentos.Remove("Mora azul");
            }
            else
            {
                btnBlackberry.BorderColor = Color.Gold;
                blackberry = true;
                alimentos.Add("Mora azul");
            }
        }

        private void btnPina_Clicked(object sender, EventArgs e)
        {
            if (pina)
            {
                btnPina.BorderColor = Color.White;
                pina = false;
                alimentos.Remove("Piña");
            }
            else
            {
                btnPina.BorderColor = Color.Gold;
                pina = true;
                alimentos.Add("Piña");
            }
        }

        private void btnPapaya_Clicked(object sender, EventArgs e)
        {
            if (papaya)
            {
                btnPapaya.BorderColor = Color.White;
                papaya = false;
                alimentos.Remove("Papaya");
            }
            else
            {
                btnPapaya.BorderColor = Color.Gold;
                papaya = true;
                alimentos.Add("Papaya");
            }
        }

        private void btnMandarina_Clicked(object sender, EventArgs e)
        {
            if (mandarina)
            {
                btnMandarina.BorderColor = Color.White;
                mandarina = false;
                alimentos.Remove("Mandarina");
            }
            else
            {
                btnMandarina.BorderColor = Color.Gold;
                mandarina = true;
                alimentos.Add("Mandarina");
            }
        }

        private void btnNaranja_Clicked(object sender, EventArgs e)
        {
            if (naranja)
            {
                btnNaranja.BorderColor = Color.White;
                naranja = false;
                alimentos.Remove("Naranja");
            }
            else
            {
                btnNaranja.BorderColor = Color.Gold;
                naranja = true;
                alimentos.Add("Naranja");
            }
        }

        private void btnKiwi_Clicked(object sender, EventArgs e)
        {
            if (kiwi)
            {
                btnKiwi.BorderColor = Color.White;
                kiwi = false;
                alimentos.Remove("Kiwi");
            }
            else
            {
                btnKiwi.BorderColor = Color.Gold;
                kiwi = true;
                alimentos.Add("Kiwi");
            }
        }

        private void btnMango_Clicked(object sender, EventArgs e)
        {
            if (mango)
            {
                btnMango.BorderColor = Color.White;
                mango = false;
                alimentos.Remove("Mango");
            }
            else
            {
                btnMango.BorderColor = Color.Gold;
                mango = true;
                alimentos.Add("Mango");
            }
        }

        private void btnSandia_Clicked(object sender, EventArgs e)
        {
            if (sandia)
            {
                btnSandia.BorderColor = Color.White;
                sandia = false;
                alimentos.Remove("Sandia");
            }
            else
            {
                btnSandia.BorderColor = Color.Gold;
                sandia = true;
                alimentos.Add("Sandia");
            }
        }

        private void btnPera_Clicked(object sender, EventArgs e)
        {
            if (pera)
            {
                btnPera.BorderColor = Color.White;
                pera = false;
                alimentos.Remove("Pera");
            }
            else
            {
                btnPera.BorderColor = Color.Gold;
                pera = true;
                alimentos.Add("Pera");
            }
        }

        private void btnDurazno_Clicked(object sender, EventArgs e)
        {
            if (durazno)
            {
                btnDurazno.BorderColor = Color.White;
                durazno = false;
                alimentos.Remove("Durazno");
            }
            else
            {
                btnDurazno.BorderColor = Color.Gold;
                durazno = true;
                alimentos.Add("Durazno");
            }
        }

        private void btnUva_Clicked(object sender, EventArgs e)
        {
            if (uva)
            {
                btnUva.BorderColor = Color.White;
                uva = false;
                alimentos.Remove("Uva");
            }
            else
            {
                btnUva.BorderColor = Color.Gold;
                uva = true;
                alimentos.Add("Uva");
            }
        }

        private void btnGranada_Clicked(object sender, EventArgs e)
        {
            if (granada)
            {
                btnGranada.BorderColor = Color.White;
                granada = false;
                alimentos.Remove("Granadilla");
            }
            else
            {
                btnGranada.BorderColor = Color.Gold;
                granada = true;
                alimentos.Add("Granadilla");
            }
        }

        void btnPistache_Clicked(System.Object sender, System.EventArgs e)
        {
            if (pistache)
            {
                btnPistache.BorderColor = Color.White;
                pistache = false;
                alimentos.Remove("Pistache");
            }
            else
            {
                btnPistache.BorderColor = Color.Gold;
                pistache = true;
                alimentos.Add("Pistache");
            }
        }

        void btnMantequilla_Clicked(System.Object sender, System.EventArgs e)
        {
            if (mantequilla)
            {
                btnMantequilla.BorderColor = Color.White;
                mantequilla = false;
                alimentos.Remove("Mantequilla");
            }
            else
            {
                btnMantequilla.BorderColor = Color.Gold;
                mantequilla = true;
                alimentos.Add("Mantequilla");
            }
        }

        void btnAvellana_Clicked(System.Object sender, System.EventArgs e)
        {
            if (avellana)
            {
                btnAvellana.BorderColor = Color.White;
                avellana = false;
                alimentos.Remove("Avellana");
            }
            else
            {
                btnAvellana.BorderColor = Color.Gold;
                avellana = true;
                alimentos.Add("Avellana");
            }
        }

        void btnMargarina_Clicked(System.Object sender, System.EventArgs e)
        {
            if (margarina)
            {
                btnMargarina.BorderColor = Color.White;
                margarina = false;
                alimentos.Remove("Margarina");
            }
            else
            {
                btnMargarina.BorderColor = Color.Gold;
                margarina = true;
                alimentos.Add("Margarina");
            }
        }

        void btnQuesoCrema_Clicked(System.Object sender, System.EventArgs e)
        {
            if (quesocrema)
            {
                btnQuesoCrema.BorderColor = Color.White;
                quesocrema = false;
                alimentos.Remove("Queso crema");
            }
            else
            {
                btnQuesoCrema.BorderColor = Color.Gold;
                quesocrema = true;
                alimentos.Add("Queso crema");
            }
        }

        void btnVinagreta_Clicked(System.Object sender, System.EventArgs e)
        {
            if (vinagreta)
            {
                btnVinagreta.BorderColor = Color.White;
                vinagreta = false;
                alimentos.Remove("Vinagreta");
            }
            else
            {
                btnVinagreta.BorderColor = Color.Gold;
                vinagreta = true;
                alimentos.Add("Vinagreta");
            }
        }

        void btnCremaAcida_Clicked(System.Object sender, System.EventArgs e)
        {
            if (cremaacida)
            {
                btnCremaAcida.BorderColor = Color.White;
                cremaacida = false;
                alimentos.Remove("Crema acida");
            }
            else
            {
                btnCremaAcida.BorderColor = Color.Gold;
                cremaacida = true;
                alimentos.Add("Crema acida");
            }
        }

        void btnQuesoPanela_Clicked(System.Object sender, System.EventArgs e)
        {
            if (quesopanela)
            {
                btnQuesoPanela.BorderColor = Color.White;
                quesopanela = false;
                alimentos.Remove("Queso panela");
            }
            else
            {
                btnQuesoPanela.BorderColor = Color.Gold;
                quesopanela = true;
                alimentos.Add("Queso panela");
            }
        }

        void btnQuesoCot_Clicked(System.Object sender, System.EventArgs e)
        {
            if (quesocot)
            {
                btnQuesoCot.BorderColor = Color.White;
                quesocot = false;
                alimentos.Remove("Queso cottage");
            }
            else
            {
                btnQuesoCot.BorderColor = Color.Gold;
                quesocot = true;
                alimentos.Add("Queso cottage");
            }
        }

        void btnQuesoAsadero_Clicked(System.Object sender, System.EventArgs e)
        {
            if (quesoasadero)
            {
                btnQuesoAsadero.BorderColor = Color.White;
                quesoasadero = false;
                alimentos.Remove("Queso asadero");
            }
            else
            {
                btnQuesoAsadero.BorderColor = Color.Gold;
                quesoasadero = true;
                alimentos.Add("Queso asadero");
            }
        }

        void btnQuesoMoz_Clicked(System.Object sender, System.EventArgs e)
        {
            if (quesomoz)
            {
                btnQuesoMoz.BorderColor = Color.White;
                quesomoz = false;
                alimentos.Remove("Queso mozzarella");
            }
            else
            {
                btnQuesoMoz.BorderColor = Color.Gold;
                quesomoz = true;
                alimentos.Add("Queso mozzarella");
            }
        }

        void btnLecheCoco_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lechecoco)
            {
                btnLecheCoco.BorderColor = Color.White;
                lechecoco = false;
                alimentos.Remove("Leche de coco");
            }
            else
            {
                btnLecheCoco.BorderColor = Color.Gold;
                lechecoco = true;
                alimentos.Add("Leche de coco");
            }
        }

        void btnLecheAlmendra_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lechealmendra)
            {
                btnLecheAlmendra.BorderColor = Color.White;
                lechealmendra = false;
                alimentos.Remove("Leche de almendra");
            }
            else
            {
                btnLecheAlmendra.BorderColor = Color.Gold;
                lechealmendra = true;
                alimentos.Add("Leche de almendra");
            }
        }

        void btnLecheSoya_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lechesoya)
            {
                btnLecheSoya.BorderColor = Color.White;
                lechesoya = false;
                alimentos.Remove("Leche de soya");
            }
            else
            {
                btnLecheSoya.BorderColor = Color.Gold;
                lechesoya = true;
                alimentos.Add("Leche de soya");
            }
        }

        void btnSardina_Clicked(System.Object sender, System.EventArgs e)
        {
            if (sardina)
            {
                btnSardina.BorderColor = Color.White;
                sardina = false;
                alimentos.Remove("Sardina");
            }
            else
            {
                btnSardina.BorderColor = Color.Gold;
                sardina = true;
                alimentos.Add("Sardina");
            }
        }

        void btnMariscos_Clicked(System.Object sender, System.EventArgs e)
        {
            if (mariscos)
            {
                btnMariscos.BorderColor = Color.White;
                mariscos = false;
                alimentos.Remove("Mariscos");
            }
            else
            {
                btnMariscos.BorderColor = Color.Gold;
                mariscos = true;
                alimentos.Add("Mariscos");
            }
        }

        void btnNopal_Clicked(System.Object sender, System.EventArgs e)
        {
            if (nopal)
            {
                btnNopal.BorderColor = Color.White;
                nopal = false;
                alimentos.Remove("Nopal");
            }
            else
            {
                btnNopal.BorderColor = Color.Gold;
                nopal = true;
                alimentos.Add("Nopal");
            }
        }

        void btnChayote_Clicked(System.Object sender, System.EventArgs e)
        {
            if (chayote)
            {
                btnChayote.BorderColor = Color.White;
                chayote = false;
                alimentos.Remove("Chayote");
            }
            else
            {
                btnChayote.BorderColor = Color.Gold;
                chayote = true;
                alimentos.Add("Chayote");
            }
        }

        void btnAcelga_Clicked(System.Object sender, System.EventArgs e)
        {
            if (acelga)
            {
                btnAcelga.BorderColor = Color.White;
                acelga = false;
                alimentos.Remove("Acelga");
            }
            else
            {
                btnAcelga.BorderColor = Color.Gold;
                acelga = true;
                alimentos.Add("Acelga");
            }
        }

        void btnEjote_Clicked(System.Object sender, System.EventArgs e)
        {
            if (ejote)
            {
                btnEjote.BorderColor = Color.White;
                ejote = false;
                alimentos.Remove("Ejote");
            }
            else
            {
                btnEjote.BorderColor = Color.Gold;
                ejote = true;
                alimentos.Add("Ejote");
            }
        }

        void btnPorto_Clicked(System.Object sender, System.EventArgs e)
        {
            if (porto)
            {
                btnPorto.BorderColor = Color.White;
                porto = false;
                alimentos.Remove("Portobello");
            }
            else
            {
                btnPorto.BorderColor = Color.Gold;
                porto = true;
                alimentos.Add("Portobello");
            }
        }

        void btnGuayaba_Clicked(System.Object sender, System.EventArgs e)
        {
            if (guayaba)
            {
                btnGuayaba.BorderColor = Color.White;
                guayaba = false;
                alimentos.Remove("Guayaba");
            }
            else
            {
                btnGuayaba.BorderColor = Color.Gold;
                guayaba = true;
                alimentos.Add("Guayaba");
            }
        }

        void btnHigo_Clicked(System.Object sender, System.EventArgs e)
        {
            if (higo)
            {
                btnHigo.BorderColor = Color.White;
                higo = false;
                alimentos.Remove("Higo");
            }
            else
            {
                btnHigo.BorderColor = Color.Gold;
                higo = true;
                alimentos.Add("Higo");
            }
        }

        void btnMelon_Clicked(System.Object sender, System.EventArgs e)
        {
            if (melon)
            {
                btnMelon.BorderColor = Color.White;
                melon = false;
                alimentos.Remove("Melon");
            }
            else
            {
                btnMelon.BorderColor = Color.Gold;
                melon = true;
                alimentos.Add("Melon");
            }
        }

        void btnToronja_Clicked(System.Object sender, System.EventArgs e)
        {
            if (toronja)
            {
                btnToronja.BorderColor = Color.White;
                toronja = false;
                alimentos.Remove("Toronja");
            }
            else
            {
                btnToronja.BorderColor = Color.Gold;
                toronja = true;
                alimentos.Add("Toronja");
            }
        }

        void btnTamarindo_Clicked(System.Object sender, System.EventArgs e)
        {
            if (tamarindo)
            {
                btnTamarindo.BorderColor = Color.White;
                tamarindo = false;
                alimentos.Remove("Tamarindo");
            }
            else
            {
                btnTamarindo.BorderColor = Color.Gold;
                tamarindo = true;
                alimentos.Add("Tamarindo");
            }
        }

        void Tunas_Clicked(System.Object sender, System.EventArgs e)
        {
            if (tuna)
            {
                Tunas.BorderColor = Color.White;
                tuna = false;
                alimentos.Remove("Tunas");
            }
            else
            {
                Tunas.BorderColor = Color.Gold;
                tuna = true;
                alimentos.Add("Tunas");
            }
        }
    }
}