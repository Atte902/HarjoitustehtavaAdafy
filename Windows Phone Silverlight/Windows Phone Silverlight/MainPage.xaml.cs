using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Coding4Fun.Toolkit.Net;
using Newtonsoft.Json;
using Windows_Phone_Silverlight.Json;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Windows_Phone_Silverlight
{
    /// <summary>
    /// Sovelluksen etusivu, johon tulee lista otteluista, jotka ladataan serveriltä.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<MatchRootObject> matches = new ObservableCollection<MatchRootObject>();
        private Uri ottelutUrl = new Uri("http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches");

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
        /// Lataa ja purkaa gzipin serveriltä. Alustaa tapahtuma käsittelijän odottamaan, että lataus loppuu.
        /// </summary>
        private async void DownloadMatches()
        {
            Coding4Fun.Toolkit.Net.GzipWebClient gZipWebClient = new GzipWebClient();
            gZipWebClient.DownloadStringAsync(ottelutUrl);
            gZipWebClient.DownloadStringCompleted += gZipWebClient_DownloadStringCompleted;
        }

        /// <summary>
        /// Kun otteluiden lataus saadaan suoritettua, tämä metodi herätetään ja se suorittaa Json
        /// converttauksen, jos tulos ei ole null. Lopuksi lisätään ladatut ottelut matches ObservableCollectioniin,
        /// jotta lista päivittyy ui:n puolella.
        /// </summary>
        void gZipWebClient_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            List<Json.MatchRootObject> downloadedMatches = new List<MatchRootObject>();
            if (e.Result != null)
                downloadedMatches = new List<Json.MatchRootObject>(JsonConvert.DeserializeObject<List<Json.MatchRootObject>>(e.Result).OrderByDescending(x => x.MatchDate).ToArray());
            foreach (MatchRootObject match in downloadedMatches)
                matches.Add(match);
        }

        /// <summary>
        /// Lataa uuden sivun, jossa näytetään tarkemmat tiedot ottelusta.
        /// </summary>
        private void MatchDetailsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MatchDetailsPage.xaml", UriKind.Relative));
        }
    }
}
