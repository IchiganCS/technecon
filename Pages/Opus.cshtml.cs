using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Technecon.Data;

namespace Technecon.Pages;

public class OpusModel : PageModel {
    public Opus Opus { get; private set; }
    public ApplicationDbContext DbContext { get; set; }

    public OpusModel(ApplicationDbContext context) {
        DbContext = context;
    }

    public IActionResult OnGet(int? id) {
        if (id is null)
            return NotFound();

        Opus? op = DbContext.Opera.Find(id);
        if (op is null)
            return NotFound();
        else
            Opus = op;
            
        return Page();
    }
}