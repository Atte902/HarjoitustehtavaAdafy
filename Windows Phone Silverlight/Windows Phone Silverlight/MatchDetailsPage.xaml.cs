using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Coding4Fun.Toolkit.Net;
using Newtonsoft.Json;
using Windows_Phone_Silverlight.MatchDetailsJson;

namespace Windows_Phone_Silverlight
{
    public partial class MatchDetailsPage : PhoneApplicationPage
    {
        private string matchId;
        private string detailsUrl = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches/"; //muista käyttää id:tä lopussa
        private MatchDetailsRootObject Details = new MatchDetailsRootObject();
        public MatchDetailsPage()
        {
            InitializeComponent();
        }

        private async void DownloadMatchDetails()
        {
            Coding4Fun.Toolkit.Net.GzipWebClient gZipWebClient = new GzipWebClient();
            gZipWebClient.DownloadStringAsync(new Uri(detailsUrl + matchId));
            gZipWebClient.DownloadStringCompleted += gZipWebClient_DownloadStringCompleted;
        }

        /// <summary>
        /// 
        /// </summary>
        private void gZipWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //log
                return;
            }
            Details = JsonConvert.DeserializeObject<MatchDetailsRootObject>(e.Result);
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