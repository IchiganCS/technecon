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
    public Artist Creator {get; private set;}

    [Column("base")]
    public int? BaseID { get; set; }
    public bool HasBase => BaseID is not null;

    [Column("title")]
    public string? Title { get; set; }

    [Column("path")]
    public string? Path { get; set; }


    private bool InitalizedWithDbContext { get; set; } = false;
    public void InitalizeWithDbContext(ApplicationDbContext context) {
        if (InitalizedWithDbContext)
            return;

        InitalizedWithDbContext = true;
        Creator = context.Artists.Find(CreatorID)!;
        
        if (HasBase && string.IsNullOrEmpty(Title)) {
            context.Opera.Find(BaseID)!.InitalizeWithDbContext(context);
            Title = context.Opera.Find(BaseID)!.Title;
        }
    }

}