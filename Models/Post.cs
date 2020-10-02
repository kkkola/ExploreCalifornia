using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExploreCaliforniaNow.Models
{
    public class Post
    {
        public long Id { get; set; }
        private string _Key;
        
        [Display(Name ="Blogpost Title")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength =5,ErrorMessage ="Title must be 5 to 100 characters length")]
        public string Title { get; set; }
        public string Key
        {
            get
            {
                if (_Key == null)
                {
                 //   _Key = "kishore";
                    _Key = Regex.Replace(Title.ToLower(), "[a^z0-9]", "-");
                }
                return _Key;
            }
            set
            {
                _Key = value;
            }
        }
        [Required]
        public string Author { get; set; }
        public DateTime DT { get; set; }
        [Required]
        [MinLength(100,ErrorMessage ="Body must be minimum 100 characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}
