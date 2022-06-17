using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Technecon.Shared;

namespace Technecon.Data;


[Table("artists")]
public class Artist : IDbEntry {
    public string FileDirectory => $"wwwroot/artists/{ID}/";

    [Column("surname")]
    [Required]
    public string Surname { get; set; }


    [Column("allfirstnames")]
    [Required]
    public string AllFirstnames { get; set; }

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



    [Column("picture")]
    public string? PicturePath { get; set; }
    public bool HasPicture => !string.IsNullOrEmpty(PicturePath);


    [Column("birthday")]
    public DateTime Birthday { get; set; }

    [Column("obit")]
    public DateTime Obit { get; set; }


    [Column("quote")]
    public string? Quote { get; set; }

    [Column("biography")]
    public string? Biography { get; set; }

    public string LifeDates => $"{Localizer.GetDayString(Birthday)} - {Localizer.GetDayString(Obit)}";
    public string YearLifeDates => $"{Localizer.GetYearString(Birthday)} - {Localizer.GetYearString(Obit)}";

    public bool MatchesWords(string[] words) {
        return words.All(
            x => Fullname.ToLower().Contains(x));
    }

    public override string ToString() {
        return CommonName;
    }

    public int ByYear() => Birthday.Year;
    public string ByName() => Surname;
    public Type GetPreviewType() => typeof(ArtistPreview);
}