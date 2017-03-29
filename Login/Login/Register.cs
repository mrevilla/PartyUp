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

namespace Login
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        EditText etEmail;
        EditText etPassword;
        EditText etConfirmPassword;
        TextView tvTest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            etEmail = (EditText)FindViewById(Resource.Id.etEmail);
            etPassword = (EditText)FindViewById(Resource.Id.etPassword);
            etConfirmPassword = (EditText)FindViewById(Resource.Id.etConfirmPassword);
            tvTest = (TextView)FindViewById(Resource.Id.tvTest);

            Button btnRegister = (Button)FindViewById(Resource.Id.btnRegister);
            btnRegister.Click += BtnRegister_Click;

        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            string url = "http://172.27.104.185/PartyUp/api/Account/Register";
            string data = "Email=" + etEmail.Text +"&Password=" + etPassword.Text +"&ConfirmPassword=" + etConfirmPassword.Text;
            string response = await MakePostRequest(url, data, false);
            if (response == "")
            {
                Finish();
            }
            else
            {
                tvTest.Text = response;
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