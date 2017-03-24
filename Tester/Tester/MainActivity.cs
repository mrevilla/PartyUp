using Android.App;
using Android.Widget;
using Android.OS;
using RestSharp;
using System.Net;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.IO;

namespace Tester
{
    [Activity(Label = "Tester", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

  
            Button btnPost = (Button)FindViewById(Resource.Id.btnPost);
            btnPost.Click += BtnPost_Click;

            Button btnLogin = (Button)FindViewById(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.0.104/PartyUp/token";
            string data = "grant_type=password&username=hannahstopbarking%40ymail.com&password=Password%40123";
            string response = await MakePostRequest(url, data, false);

            TextView tvResponse = (TextView)FindViewById(Resource.Id.tvResponse);
            tvResponse.Text = response; 
        }

        //http://172.27.104.185/PartyUp/api/Account/Register url used to access local IIS
        private async void BtnPost_Click(object sender, System.EventArgs e)
        {
            //set destination and payload
            //call for post
            string url = "http://192.168.0.104/PartyUp/api/Account/Register";
            string data = "Email=hannahstopbarking%40ymail.com&Password=Password%40123&ConfirmPassword=Password%40123";
            string response = await MakePostRequest(url, data, false);
        }

        public async Task<string> MakePostRequest(string url, string serializedDataString, bool isJson)
        {
            //simple request function 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (isJson)
                request.ContentType = "application/json";
            else
                request.ContentType = "application/x-www-form-urlencoded";

            request.Method = "POST";
            var stream = await request.GetRequestStreamAsync();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(serializedDataString);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();

            using (StreamReader sr = new StreamReader(respStream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}

