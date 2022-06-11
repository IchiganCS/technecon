using System.Globalization;

namespace Technecon.Data;

public static class Localizer {
    public static string GetOccupation(Artist artist) {
        if ((artist.Occupations & 1) != 0)
            return artist.Sex == 0 ? "Autor" : "Autorin";
        else if ((artist.Occupations & 2) != 0)
            return artist.Sex == 0 ? "Maler" : "Malerin";
        else
            return artist.Sex == 0 ? "Komponist" : "Komponistin";
    }

    public static string GetDayString(DateTime date) {
        return date.ToString("dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
    }

    internal static object GetYearString(DateTime date) {
        return date.ToString("yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
    }

    internal static string GetTypeString(Opus.Type type) {
        return type switch {
            Opus.Type.Music => "Musikstück",
            Opus.Type.Text => "Schriftstück",
            Opus.Type.Painting => "Gemälde",
            _ => ""
        };
    }
}