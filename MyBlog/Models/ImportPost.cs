using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBlog.Models
{
    public class ImportPost
    {
        public int ID { get; set; }

        public int post_author { get; set; }

        public DateTime? post_date { get; set; }

        //public DateTime? post_date_gmt { get; set; }

        public string post_content { get; set; } = string.Empty;

        public string post_title { get; set; } = string.Empty;

        public string post_excerpt { get; set; } = string.Empty;

        public string post_status { get; set; } = string.Empty;

        public string comment_status { get; set; } = string.Empty;

        public string ping_status { get; set; } = string.Empty;

        public string post_password { get; set; } = string.Empty;

        public string post_name { get; set; } = string.Empty;

        public string to_ping { get; set; } = string.Empty;

        public string pinged { get; set; } = string.Empty;

        public DateTime? post_modified { get; set; }

        //public DateTime? post_modified_gmt { get; set; }

        public string post_content_filtered { get; set; } = string.Empty;

        public int post_parent { get; set; }

        public string guid { get; set; } = string.Empty;

        public int menu_order { get; set; }

        public string post_type { get; set; } = string.Empty;

        public string post_mime_type { get; set; } = string.Empty;

        public long comment_count { get; set; }
    }
}