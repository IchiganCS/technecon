using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technecon.Models;

[Table("artists")]
public class Artist {

    [Column("surname")]
    public string? Surname { get; set; }


    [Column("firstnames")]
    public string? Firstnames { get; set; }


    [Required]
    [Column("id")]
    public int ID { get; set; }
}