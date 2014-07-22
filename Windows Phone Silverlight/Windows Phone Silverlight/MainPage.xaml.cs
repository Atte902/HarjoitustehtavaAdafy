using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using Windows_Phone_Silverlight.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using AdvancedREI.Net.Http.Compression;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Windows_Phone_Silverlight
{
    /// <summary>
    /// Sovelluksen etusivu, johon tulee lista otteluista, jotka ladataan serveriltä.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<MatchRootObject> matches = new ObservableCollection<MatchRootObject>();
        private Uri ottelutUrl = new Uri("http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches");
        private Uri settingsPageUri = new Uri("/SettingsPage.xaml", UriKind.Relative);
        private string matchDetailsPageUri = "/MatchDetailsPage.xaml"; // Muista lisätä match id.
        private const int TimeOutTime = 15; //Seconds
        /// <summary>
        /// Lataa suoraan ottelut serveriltä initializoituaan ensin komponentit.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            DownloadMatches();
            Loaded += MainPage_Loaded;
        }

        /// <summary>
        /// Alustetaan itemsource MatchesListboxille, kun sivu on ladattu kokonaan.
        /// </summary>
        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MatchesListBox.ItemsSource = matches;
        }

        /// <summary>
        /// Lataa ja purkaa gzipin serveriltä. Kun otteluiden lataus saadaan suoritettua suoritetaan Json
        /// converttauksen. Lopuksi lisätään ladatut ottelut matches ObservableCollectioniin,
        /// jotta lista päivittyy ui:n puolella. Lisäksi otetaan latausikoni pois päältä.
        /// </summary>
        private async void DownloadMatches()
        {
            BusyIndicator.IsRunning = true;
            HttpClient httpClient = new HttpClient(new CompressedHttpClientHandler());
            httpClient.Timeout = TimeSpan.FromSeconds(TimeOutTime);
            try
            {
                string result = await httpClient.GetStringAsync(ottelutUrl);
                ObservableCollection<MatchRootObject> downloadedMatches = new ObservableCollection<MatchRootObject>
                        (JsonConvert.DeserializeObject<ObservableCollection<MatchRootObject>>(result).OrderByDescending
                        (x => x.MatchDate).Where(x => x.MatchDate <= DateTime.Now).ToArray()); // Järjestää niin että ylhäällä on uusimmat ja sitten ottaa vain ne, jotka on jo pelattu
                matches.Clear();
                foreach (MatchRootObject match in downloadedMatches)
                    matches.Add(match);
            }
            catch(System.Net.Http.HttpRequestException)
            {
                //log
                Debug.WriteLine("Ei internetyhteyttä");
                Telerik.Windows.Controls.RadMessageBox.Show("Ei internetyhteyttä",Telerik.Windows.Controls.MessageBoxButtons.OK);
                ReloadButton.Visibility = System.Windows.Visibility.Visible;
            }
            catch(Exception)
            {
                //log
                Debug.WriteLine("Virhe yhteyttä muodostettaessa");
                Telerik.Windows.Controls.RadMessageBox.Show("Virhe yhteyttä muodostettaessa", Telerik.Windows.Controls.MessageBoxButtons.OK);
                ReloadButton.Visibility = System.Windows.Visibility.Visible;
            }
            BusyIndicator.IsRunning = false;
        }

        /// <summary>
        /// Lataa uuden sivun, jossa näytetään tarkemmat tiedot ottelusta. MatchDetailsPageUriin lisätään parametrina
        /// ottelun id.
        /// </summary>
        private void MatchDetailsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(matchDetailsPageUri + "?matchid=" + //lisätään match id, jotta ottelua voidaan hakea serveriltä.
                ((Json.MatchRootObject)MatchesListBox.SelectedItem).Id.ToString() ,UriKind.Relative));
        }

        /// <summary>
        /// Lataa matsit uudelleen, kun nappia painetaan.
        /// </summary>
        private void ReloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReloadButton.Visibility = System.Windows.Visibility.Collapsed;
            DownloadMatches();
        }
    }
}
