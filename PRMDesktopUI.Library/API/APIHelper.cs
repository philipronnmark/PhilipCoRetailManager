 using PRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PRMDesktopUI.Library.API
{
    public class APIHelper : IAPIHelper
    {
        //One HttpClient for the lifespan of our client, that way we dont have a bunch of clients that clog up our network
        private HttpClient _apiClient { get; set; }
        private ILoggedInUserModel _loggedInUser;


        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type", "password"),
                new KeyValuePair<string,string>("username", username),
                new KeyValuePair<string,string>("password", password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }
        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear(); 
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
                if(response.IsSuccessStatusCode)
            {
                    var results = await response.Content.ReadAsAsync<LoggedInUserModel>();
            
                    _loggedInUser.CreatedDate = results.CreatedDate;
                    _loggedInUser.EmailAddress = results.EmailAddress;
                    _loggedInUser.FirstName = results.FirstName;
                    _loggedInUser.LastName = results.LastName;
                    _loggedInUser.Id = results.Id;
                    _loggedInUser.Token = token;
            }

            else
                {
                    throw new Exception(response.ReasonPhrase);
                }

        }




    }

}

