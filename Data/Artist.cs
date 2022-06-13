using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technecon.Data;


[Table("artists")]
public class Artist : IDbEntry {
    public string FileDirectory => $"wwwroot/artists/{ID}/";

    [Column("surname")]
    [Required]
    public string? Surname { get; set; }


    [Column("allfirstnames")]
    [Required]
    public string? AllFirstnames { get; set; }

    [Column("commonfirstname")]
    public string? CommonFirstname { get; set; }

    public string Fullname => AllFirstnames + " " + Surname;
    public string CommonName => (CommonFirstname ?? AllFirstnames) + " " + Surname;


    [Column("id")]
    [Key]
    public int ID { get; set; }

    [Column("sex")]
    public int Sex { get; set; }

    [Column("occupations")]
    public int Occupations { get; set; }

    public string OccupationString => Localizer.GetOccupation(this);


    private string MarkdownPath => FileDirectory + "markdown.md";
    public bool HasMarkdown => File.Exists(MarkdownPath);


    private string PicturePath => FileDirectory + "image.jpg";
    public string HtmlPicturePath => $"/artists/{ID}/image.jpg";
    public bool HasPicture => File.Exists(PicturePath);


    [Column("birthday")]
    public DateTime Birthday { get; set; }

    [Column("obit")]
    public DateTime Obit { get; set; }


    public string QuoteString => 
        System.IO.File.ReadAllLines(MarkdownPath)[0];

    public string BiographyString {
        get {
            var text = System.IO.File.ReadAllLines(MarkdownPath).Skip(1);
            if (text.Any())
                return text.Aggregate((x, y) => $"{x}\n{y}");
            else return string.Empty;
        }
    }

    public string LifeDates => $"{Localizer.GetDayString(Birthday)} - {Localizer.GetDayString(Obit)}";
    public string YearLifeDates => $"{Localizer.GetYearString(Birthday)} - {Localizer.GetYearString(Obit)}";

    public bool FitsSearchString(string str) {
        string[] words = str.ToLower().Split();
        return words.All(
            x => Fullname.ToLower().Contains(x));
    }

    public override string ToString() {
        return CommonName;
    }
}