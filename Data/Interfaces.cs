namespace Technecon.Data;

public interface IPreviewable {
    public Type GetPreviewType();
}

public interface ISearchable {
    public bool MatchesWords(string[] words);
}

public interface ISortable {
    public int ByYear();
    public string ByName();
}