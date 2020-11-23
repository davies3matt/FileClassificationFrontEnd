using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c_sharp_grad_frontend.Helpers;
using c_sharp_grad_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace c_sharp_grad_frontend.Pages
{
    public class TextEditModel : PageModel
    {
        public ITextFile textfile;
        public IConfiguration Configuration { get; }
        IToken token { get; }

        public readonly List<TextModel> TextList;

        public TextModel[] PostList = new TextModel[1000];


        public TextEditModel(IConfiguration configuration, IToken _token, ITextFile _textfile)
        {
            textfile = _textfile;
            Configuration = configuration;
            token = _token;
            TextList = textfile.listtext;
        }


        public void OnGet()
        {
            
        }


        public void OnPost(List<TextModel> textFile)
        {

            var guy = textFile;

            var helper = new TextUploadHelper(Configuration, token, textfile);

            helper.PostToDB(textFile);


        }
    }
}
