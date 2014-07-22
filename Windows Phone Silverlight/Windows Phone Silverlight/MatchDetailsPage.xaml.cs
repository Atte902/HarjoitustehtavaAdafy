using System;
using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using Windows_Phone_Silverlight.MatchDetailsJson;
using System.Net.Http;
using AdvancedREI.Net.Http.Compression;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace Windows_Phone_Silverlight
{
    /// <summary>
    /// Sivulla esitetään matsin tarkemmat tiedot, jotka ladataan serveriltä.
    /// </summary>
    public partial class MatchDetailsPage : PhoneApplicationPage
    {
        private string matchId;
        private string detailsUrl = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches/"; //muista käyttää id:tä lopussa
        private MatchDetailsRootObject details = new MatchDetailsRootObject();
        private const int TimeOutTime = 15; //Seconds

        /// <summary>
        /// Yksinkertainen constructori.
        /// </summary>
        public MatchDetailsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ladataan matsin tarkemmat tiedot serveriltä käyttäen httpclienttiä ja
        /// gzipin purkuun compressedhttpclienthandleriä. Virheen sattuessa käyttäjälle
        /// ilmoitetaan siitä messageboxilla. Lopuksi latausikoni pois.
        /// </summary>
        private async void DownloadMatchDetails()
        {
            BusyIndicator.IsRunning = true;
            try
            {
                HttpClient httpClient = new HttpClient(new CompressedHttpClientHandler());
                httpClient.Timeout = TimeSpan.FromSeconds(TimeOutTime);
                string result = await httpClient.GetStringAsync(new Uri(detailsUrl + matchId));
                details = JsonConvert.DeserializeObject<MatchDetailsRootObject>(result);
                SetDetails();
            }
            catch(System.Net.Http.HttpRequestException)
            {
                //log
                Debug.WriteLine("Ei internetyhteyttä");
                Telerik.Windows.Controls.RadMessageBox.Show("Ei internetyhteyttä", Telerik.Windows.Controls.MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                //log
                Debug.WriteLine("Virhe yhteyttä muodostettaessa");
                Telerik.Windows.Controls.RadMessageBox.Show("Virhe yhteyttä muodostettaessa", Telerik.Windows.Controls.MessageBoxButtons.OK);
            }
            BusyIndicator.IsRunning = false;
        }

        /// <summary>
        /// Asettaa joukkueiden logot, nimet, päivämäärän, tulksen ja ottelun tapahtumat.
        /// </summary>
        private void SetDetails()
        {
            HomeTeamLogo.Source = new BitmapImage(new Uri(details.Match.HomeTeam.LogoUrl));
            AwayTeamLogo.Source = new BitmapImage(new Uri(details.Match.AwayTeam.LogoUrl));
            HomeTeamNameTextBlock.Text = details.Match.HomeTeam.Name;
            AwayTeamNameTextBlock.Text = details.Match.AwayTeam.Name;
            ResultTextBlock.Text = details.Match.HomeGoals + " - " + details.Match.AwayGoals;
            MatchActionsListBox.ItemsSource = details.MatchActions;
            MatchDateTextBlock.Text = DateTime.Parse(details.Match.MatchDate).ToShortDateString();
        }

        /// <summary>
        /// Tallennetaan ottelun id, jotta voidaan hakea tietyn ottelun
        /// tietoja serveriltä.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("matchid"))
            {
                matchId = NavigationContext.QueryString["matchid"];
                DownloadMatchDetails();
            }
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Lataa matsit uudelleen, kun nappia painetaan.
        /// </summary>
        private void ReloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReloadButton.Visibility = System.Windows.Visibility.Collapsed;
            DownloadMatchDetails();
        }
    }
}