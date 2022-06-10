using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Technecon.Pages;

public class OpusListModel : PageModel {
    public ApplicationDbContext DbContext { get; private set; }

    public OpusListModel(ApplicationDbContext context) {
        DbContext = context;
    }

    public void OnGet() {

    }
}