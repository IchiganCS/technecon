using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technecon.Data;

[Table("opera")]
public class Opus {
    public enum Type {
        Text = 0,
        Music = 1,
        Painting = 2
    }

    [Column("id")]
    [Required]
    public int ID { get; set; }

    [Column("creator")]
    public int CreatorID { get; set; }
    public Artist Creator { get; private set; }

    [Column("base")]
    public int? BaseID { get; set; }
    public bool HasBase => BaseID is not null;
    public Opus Base { get; private set; }


    [Column("type")]
    [Required]
    private int TypeInt { get; set; }
    public Type Kind => (Type)TypeInt;


    [Column("title")]
    public string? Title { get; set; }


    private string FileDirectory => $"wwwroot/opera/{ID}/";
    public string MarkdownPath => FileDirectory + "markdown.md";
    public string PaintingPath => FileDirectory + "image.jpg";
    public bool HasMarkdown => File.Exists(MarkdownPath);
    public IEnumerable<string> TextLines {
        get {
            var lines = File.ReadAllLines(MarkdownPath)
                .TakeWhile(x => x != "---")
                .SkipWhile(x => string.IsNullOrWhiteSpace(x))
                .ToList();

            for (int i = lines.Count - 1; i >= 0; i--)
                if (!string.IsNullOrWhiteSpace(lines[i])) {
                    return lines.SkipLast(lines.Count - i - 1);
                }

            return new string[] { };
        }
    }
    public string Text => TextLines.Aggregate((x, y) => $"{x}\n{y}");
    public bool HasText => TextLines.Any();
    public string Description {
        get {
            var lines = File.ReadAllLines(MarkdownPath)
                .SkipWhile(x => x != "---")
                .Skip(1);

            if (lines.Any())
                return lines.Aggregate((x, y) => $"{x}\n{y}");

            return "";
        }
    }
    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);



    private bool InitalizedWithDbContext { get; set; } = false;
    public void InitalizeWithDbContext(ApplicationDbContext context) {
        if (InitalizedWithDbContext)
            return;

        InitalizedWithDbContext = true;
        Creator = context.Artists.Find(CreatorID)!;

        if (HasBase && string.IsNullOrEmpty(Title)) {
            Base = context.Opera.Find(BaseID)!;
            Base.InitalizeWithDbContext(context);
            Title = Base.Title;
        }
    }

}