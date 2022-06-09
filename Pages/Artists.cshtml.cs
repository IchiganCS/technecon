using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Technecon.Pages;

public class ArtistModel : PageModel {

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
    }

    public Artist? CurrentArtist { get; private set; }

    public IActionResult OnGet(int? id) {
        if (id is null)
            return NotFound();

        using (var db = ApplicationDbContext.CreateDefault()) {
            Artist? ar = db.Artists.Find(id);
            if (ar is null)
                return NotFound();
            else
                CurrentArtist = ar;
        }
        return Page();
    }
}