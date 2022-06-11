using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Technecon.Data;

[Table("opera")]
public class Opus {
    public enum Type {
        Text = 0,
        Music = 1,
        Painting = 2
    }

    public Opus() { }
    private ILazyLoader LazyLoader { get; set; }
    private Opus(ILazyLoader ll) {
        LazyLoader = ll;
    }

    [Column("id")]
    [Key]
    public int ID { get; set; }


    [Column("creator")]
    public int CreatorID { get; set; }
    private Artist _creator;
    [ForeignKey("CreatorID")]
    public Artist Creator {
        get => LazyLoader.Load(this, ref _creator!)!;
        set => _creator = value;
    }


    [Column("base")]
    public int? BaseID { get; set; }
    public bool HasBase => BaseID is not null;
    private Opus _base;
    [ForeignKey("BaseID")]
    public Opus Base {
        get => LazyLoader.Load(this, ref _base!)!;
        set => _base = value;
    }




    [Column("type")]
    [Required]
    public int TypeInt { get; set; }
    public Type Kind => (Type)TypeInt;
    public string KindString => Localizer.GetTypeString(Kind);



    public string? _title;
    [Column("title")]
    public string? Title {
        get => _title ?? Base.Title;
        set => _title = value;
    }



    private string FileDirectory => $"wwwroot/opera/{ID}/";
    public string MarkdownPath => FileDirectory + "markdown.md";
    public string PaintingPath => FileDirectory + "image.jpg";
    public string HtmlPaintingPath => $"/opera/{ID}/image.jpg";
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
}