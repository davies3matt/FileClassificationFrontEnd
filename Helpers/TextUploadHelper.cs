using c_sharp_grad_frontend.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_grad_frontend.Helpers
{
    public class TextUploadHelper
    {
        IToken token;
        ITextFile textfile;
        IConfiguration configuration;
        ITextFile textFile;

        public TextUploadHelper(IConfiguration _configuration, IToken _token, ITextFile _textfile)
        {
            this.token = _token;
            this.configuration = _configuration;
            this.textfile = _textfile;
        }

        public async Task<bool> PostTextFile(TextModel textFile)
        {
            var json = JsonConvert.SerializeObject(textFile);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient(SSLHelper.GetSSL());
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.token);
            var response = await httpClient.PostAsync(configuration.GetConnectionString("PostTextFile"), httpContent);



            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            textfile.listtext = JsonConvert.DeserializeObject<List<TextModel>>(responseString);

            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }


        public async Task<bool> PostToDB(List<TextModel> tm)
        {
            var json = JsonConvert.SerializeObject(tm);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient(SSLHelper.GetSSL());
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.token);
            var response = await httpClient.PostAsync(configuration.GetConnectionString("WriteTextFile"), httpContent);



            var responseString = await response.Content.ReadAsStringAsync();



            return true;
        }
    }
}
