using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Blog
    {
        public Guid Id { get; set; }

        public string AuthorName {  get; set; }
        public string Content {  get; set; }
        public string Title { get; set; }
        public DateTime Created { get; private set; }
        public DateTime LastModified { get; private set; }

        public Blog() : this(string.Empty, string.Empty, string.Empty)
        {
        }
        public Blog(string authorName,  string title, string content)
        {
            Created = DateTime.Now.ToUniversalTime();
            LastModified = DateTime.Now.ToUniversalTime();
            AuthorName = authorName;
            Content = content;
            Title = title;
        }

        private void UpdateLastModified() => LastModified = DateTime.Now;
    }
}
