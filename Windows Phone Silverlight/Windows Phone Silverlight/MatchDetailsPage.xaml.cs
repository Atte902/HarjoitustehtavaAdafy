using System;
using System.Linq;
using System.Net;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Coding4Fun.Toolkit.Net;
using Newtonsoft.Json;
using Windows_Phone_Silverlight.MatchDetailsJson;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;
using System.Net.Http;
using AdvancedREI.Net.Http.Compression;

namespace Windows_Phone_Silverlight
{
    public partial class MatchDetailsPage : PhoneApplicationPage
    {
        private string matchId;
        private string detailsUrl = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches/"; //muista käyttää id:tä lopussa
        private MatchDetailsRootObject details = new MatchDetailsRootObject();

        public MatchDetailsPage()
        {
            InitializeComponent();
        }

        private async void DownloadMatchDetails()
        {
            HttpClient httpClient = new HttpClient(new CompressedHttpClientHandler());
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            string result = await httpClient.GetStringAsync(new Uri(detailsUrl + matchId));
            details = JsonConvert.DeserializeObject<MatchDetailsRootObject>(result);
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
    }
}