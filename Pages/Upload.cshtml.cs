using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c_sharp_grad_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using c_sharp_grad_frontend.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace c_sharp_grad_frontend.Pages
{
    public class DonateModel : PageModel
    {
        public DonateModel(IConfiguration configuration, IToken _token, ITextFile _textfile)
        {
            Configuration = configuration;
            token = _token;
            textfile = _textfile;
        }
        public IConfiguration Configuration { get; }
        public IToken token { get; }
        public ITextFile textfile { get; }

        public async Task OnGetAsync()
        {

        }

        public async Task<IActionResult> OnPost(IFormFile avatar)
        {
            var imageByte = avatar;
            BinaryReader br = new BinaryReader(imageByte.OpenReadStream());

            TextModel xt = new TextModel();

            xt.AvatarOne = br.ReadBytes((int)imageByte.OpenReadStream().Length);

            var helper = new TextUploadHelper(Configuration, token, textfile);

            if (await helper.PostTextFile(xt))
                return RedirectToPage("/TextEdit");
            else
                return RedirectToPage("/Error");
        }
    }
}