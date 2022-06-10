using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Technecon.Pages;

public class ArtistListModel : PageModel {

    public ApplicationDbContext DbContext { get; private set; }

    public ArtistListModel(ApplicationDbContext context) {
        DbContext = context;
    }

    public void OnGet() {

    }
}