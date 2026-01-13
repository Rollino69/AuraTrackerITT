using System.Collections.Generic;
using System.Windows;       // Base per WPF
using System.Windows.Media; // Serve per i colori (Brushes)

namespace AuraTrackerWPF
{
    // Classe per i dati
    public class AzioneAura
    {
        public string Descrizione { get; set; }
        public int Punteggio { get; set; }

        // Importante: serve a mostrare il testo corretto nella tendina
        public override string ToString()
        {
            return Descrizione;
        }
    }

    public partial class MainWindow : Window
    {
        private long auraTotale = 1000;
        List<AzioneAura> databaseAzioni = new List<AzioneAura>();

        public MainWindow()
        {
            InitializeComponent();
            CaricaDatabase();
            AggiornaInterfaccia();
        }

        private void CaricaDatabase()
        {
            // Popoliamo la lista
            databaseAzioni.Add(new AzioneAura { Descrizione = "Hai aiutato una signora anziana", Punteggio = 500 });
            databaseAzioni.Add(new AzioneAura { Descrizione = "Hai fatto palestra alle 6 di mattina", Punteggio = 1000 });
            databaseAzioni.Add(new AzioneAura { Descrizione = "Hai inciampato davanti alla crush", Punteggio = -5000 });
            databaseAzioni.Add(new AzioneAura { Descrizione = "Hai pagato la cena agli amici", Punteggio = 200 });
            databaseAzioni.Add(new AzioneAura { Descrizione = "Hai risposto male al cameriere", Punteggio = -1000 });
            databaseAzioni.Add(new AzioneAura { Descrizione = "Non hai salutato entrando in ascensore", Punteggio = -50 });

            // In WPF si usa ItemsSource
            cmbAzioni.ItemsSource = databaseAzioni;

            // Seleziona automaticamente il primo elemento per comodità
            if (databaseAzioni.Count > 0) cmbAzioni.SelectedIndex = 0;
        }

        // Questo evento è collegato al Click nel file XAML
        private void BtnCalcola_Click(object sender, RoutedEventArgs e)
        {
            // Prendiamo l'oggetto selezionato
            AzioneAura azioneScelta = cmbAzioni.SelectedItem as AzioneAura;

            if (azioneScelta != null)
            {
                auraTotale += azioneScelta.Punteggio;

                if (azioneScelta.Punteggio >= 0)
                {
                    txtRisultato.Text = $"📈 GAIN! +{azioneScelta.Punteggio}";
                    txtRisultato.Foreground = Brushes.Green; // In WPF si usa Brushes
                }
                else
                {
                    txtRisultato.Text = $"📉 LOSS... {azioneScelta.Punteggio}";
                    txtRisultato.Foreground = Brushes.Red;
                }

                AggiornaInterfaccia();
            }
        }

        private void AggiornaInterfaccia()
        {
            txtTotale.Text = $"Aura Totale: {auraTotale}";

            // Cambio colore dinamico
            if (auraTotale > 2000) txtTotale.Foreground = Brushes.Gold;
            else if (auraTotale < 0) txtTotale.Foreground = Brushes.Gray;
            else txtTotale.Foreground = Brushes.Black;
        }
    }
}