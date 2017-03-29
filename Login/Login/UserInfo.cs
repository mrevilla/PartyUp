using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Login
{
    [Activity(Label = "UserInfo")]
    public class UserInfo : Activity
    {
        TextView tvUserInfo;
        private static string AccessToken;
        private static string Email;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "UserInfo";

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserInfo);
            tvUserInfo = (TextView)FindViewById(Resource.Id.tvUserInfo);
            AccessToken = Intent.GetStringExtra("token");
            if(AccessToken == null)
            {
                tvUserInfo.Text = "Did not get token";
                Finish();
            }
            else
            {
                string url = "http://172.27.104.185/PartyUp/api/account/userinfo";
                string response = await MakeGetRequest(url);

                tvUserInfo.Text = response;

                dynamic jsonData = JsonConvert.DeserializeObject(response);
                Email = jsonData.Email;

                tvUserInfo.Text = tvUserInfo.Text + '\n' +Email;
            }

            Button btnLogout = (Button)FindViewById(Resource.Id.btnLogout);
            btnLogout.Click += BtnLogout_Click;
        }

        private async void BtnLogout_Click(object sender, EventArgs e)
        {
            string url = "http://172.27.104.185/partyup/api/Account/logout";
            string data = "Email=" + Email;
            string response = await MakePostRequest(url, data, false);
            if(response == "")
            {
                tvUserInfo.Text = "Logged Out";
                Finish();
            }
            else
            {
                //parse out the data and figure out whats wrong
            }
        }

        public static async Task<string> MakeGetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + AccessToken);

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();
            respStream.Flush();

            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                respStream = null;
                return strContent;
            }
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
            request.Headers.Add("Authorization", "Bearer " + AccessToken);
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