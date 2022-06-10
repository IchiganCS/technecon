using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Technecon.Data;

namespace Technecon.Pages;

public class ArtistModel : PageModel {
    public Artist Artist { get; private set; }

    public ApplicationDbContext DbContext { get; private set; }

    public ArtistModel(ApplicationDbContext context) {
        DbContext = context;
    }

    public IActionResult OnGet(int? id) {
        if (id is null)
            return NotFound();

        Artist? ar = DbContext.Artists.Find(id);
        if (ar is null)
            return NotFound();
        else
            Artist = ar;

        return Page();
    }
}