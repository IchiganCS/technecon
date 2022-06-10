using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Technecon.Pages;

public class IndexModel : PageModel
{
    public ApplicationDbContext DbContext { get; set; }
    public IndexModel(ApplicationDbContext context)
    {
        DbContext = context;
    }

    public void OnGet()
    {

    }
}
