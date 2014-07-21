using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Coding4Fun.Toolkit.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Windows_Phone_Silverlight.Json;

namespace Windows_Phone_Silverlight
{
    /// <summary>
    /// Sovelluksen etusivu, johon tulee lista otteluista, jotka ladataan serveriltä.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        private List<OtteluRootObject> ottelut;
        private Uri ottelutUrl = new Uri("http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches");

        /// <summary>
        /// Lataa suoraan ottelut serveriltä initializoituaan ensin komponentit.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            LataaOttelut();
        }

        /// <summary>
        /// Lataa ja purkaa gzipin serveriltä. Alustaa tapahtuma käsittelijän odottamaan, että lataus loppuu.
        /// </summary>
        private async void LataaOttelut()
        {
            Coding4Fun.Toolkit.Net.GzipWebClient gZipWebClient = new GzipWebClient();
            gZipWebClient.DownloadStringAsync(ottelutUrl);
            gZipWebClient.DownloadStringCompleted += gZipWebClient_DownloadStringCompleted;
        }

        /// <summary>
        /// Kun otteluiden lataus saadaan suoritettua, tämä metodi herätetään ja se suorittaa Json
        /// converttauksen, jos tulos ei ole null.
        /// </summary>
        void gZipWebClient_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Result != null)
                ottelut = JsonConvert.DeserializeObject<List<Json.OtteluRootObject>>(e.Result);
        }
    }
}
