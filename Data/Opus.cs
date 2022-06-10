using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technecon.Data;

[Table("opera")]
public class Opus {
    [Column("id")]
    [Required]
    public int ID { get; set; }

    [Column("creator")]
    public int CreatorID { get; set; }
    public Artist GetCreator(ApplicationDbContext context)
        => context.Artists.Find(CreatorID)!;

    [Column("base")]
    public int? BaseID { get; set; }
    public bool HasBase => BaseID is not null;

    [Column("title")]
    public string? Title { get; set; }

    [Column("path")]
    public string? Path { get; set; }

    public string GetTitle(ApplicationDbContext context) {
        if (!HasBase)
            return Title ?? string.Empty;

        return Title ?? context.Opera.Find(BaseID)!.GetTitle(context);
    }

}