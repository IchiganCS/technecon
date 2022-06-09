using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technecon.Data;


[Table("artists")]
public class Artist {

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


    [Required]
    [Column("id")]
    public int ID { get; set; }

    [Column("sex")]
    public int Sex { get; set; }

    [Column("occupations")]
    public int Occupations { get; set; }

    public string OccupationString => Localizer.GetOccupation(this);


    [Column("markdown")]
    public string MarkdownPath { get; set; }

    [Column("picture")]
    public string PicturePath { get; set; }

    [Column("birthday")]
    public DateTime Birthday { get; set; }

    [Column("obit")]
    public DateTime Obit { get; set; }


    public string QuoteString(Func<string, string> relativToAbsPathConverter) {
        return System.IO.File.ReadAllLines($"wwwroot{MarkdownPath}")[0];
    }
    public string BiographyString(Func<string, string> relativToAbsPathConverter) {
        string text = System.IO.File.ReadAllText($"wwwroot{MarkdownPath}");
        return text.Substring(text.IndexOf('\n')).Trim();
    }
}