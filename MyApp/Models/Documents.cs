
using SQLite;

namespace MyApp.Models
{
    [Table("Documents")]
    public class Documents
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(40)]
        public string name { get; set; }
        [MaxLength(40)]
        public string imagePath { get; set; }
        [MaxLength(40)]
        public string dataBirthday { get; set; } //(day.mounth.year)
        [MaxLength(40)]
        public string familia { get; set; }
        [MaxLength(40)]
        public string otchestvo { get; set; }
        [MaxLength(40)]
        public string nameEng { get; set; }
        [MaxLength(40)]
        public string familiaEng { get; set; }
        [MaxLength(140)]
        public string pidpis { get; set; }
        public int NumberOfPage { get; set; }//номер страницы в карусели
    }
}
