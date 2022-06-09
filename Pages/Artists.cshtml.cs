
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Technecon.Data;

namespace Technecon.Pages;

public class ArtistModel : PageModel {
    public Artist Artist { get; private set; }

    public IActionResult OnGet(int? id) {
        if (id is null)
            return NotFound();

        using (var db = ApplicationDbContext.CreateDefault()) {
            Artist? ar = db.Artists.Find(id);
            if (ar is null)
                return NotFound();
            else
                Artist = ar;
        }
        return Page();
    }
}