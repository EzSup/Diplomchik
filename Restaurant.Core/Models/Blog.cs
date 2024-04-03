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

        public string AuthorName 
        { 
            get { return AuthorName; }             
            set{ AuthorName = value; UpdateLastModified(); }
        }
        public string Content
        {
            get { return Content; }
            set { Content = value; UpdateLastModified(); }
        }
        public string Title
        {
            get { return Title; }
            set { Title = value; UpdateLastModified(); }
        }
        public DateTime Created { get; private set; }
        public DateTime LastModified { get; private set; }

        public Blog()
        {
            Created = DateTime.Now;
            LastModified = DateTime.Now;
            AuthorName = string.Empty;
            Content = string.Empty;
            Title = string.Empty;
        }

        private void UpdateLastModified() => LastModified = DateTime.Now;
    }
}
