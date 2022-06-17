using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Technecon.Shared;

namespace Technecon.Data;

[Table("opera")]
public class Opus : IDbEntry {
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



    [Column("picture")]
    public string? PaintingPath { get; set; }


    [Column("text")]
    public string? Text { get; set; }
    public bool HasText => !string.IsNullOrEmpty(Text);


    [Column("description")]
    public string? Description { get; set; }
    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);



    public bool MatchesWords(string[] words) {
        return words.All(
            x => Title!.ToLower().Contains(x) || 
            Creator.CommonName.ToLower().Contains(x));
    }

    public override string ToString() {
        return Title!;
    }
    public int ByYear() => 0;
    public string ByName() => Title!;

    public System.Type GetPreviewType() => typeof(OpusPreview);

}