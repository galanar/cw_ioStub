namespace Codewars;

public class DirectoryItem
{
    public string PathName { get; set; }

    public DirectoryItem? Parent { get; set; }
    
    public List<DirectoryItem> DirectoryItems { get; } = new();
}