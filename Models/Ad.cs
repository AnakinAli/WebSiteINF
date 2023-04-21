namespace WebSiteINF.Models
{
    public class Ad
    {
        public int ID { get; set; }
        public int AdminID { get; set; }
        public string ImageLink { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public Ad() { }
    }
}
