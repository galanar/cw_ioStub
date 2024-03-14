namespace Codewars;

public class IoStub
{
    private readonly DirectoryItem _root;

    public IoStub()
    {
        _root =new DirectoryItem{PathName = "/"};
    }

    public void MkDir(string path)
    {
        var parentItem = _root;
        var pathElements = path.Split('/');
        
        for (var i = 1; i < pathElements.Length-1; i++)
        {
            parentItem = parentItem.DirectoryItems.First(x => x.PathName == $"/{pathElements[i]}");
        }
        
        parentItem.DirectoryItems.Add(new DirectoryItem{ PathName = path[path.LastIndexOf('/')..], Parent = parentItem});
    }

    public void RmDir(string path)
    {
        var parentItem = _root;
        path = path.EndsWith('/') ? path.Remove(path.LastIndexOf('/')) : path;
        var pathElements = path.Split('/');
        
        for (var i = 1; i < pathElements.Length-1; i++)
        {
            parentItem = parentItem.DirectoryItems.First(x => x.PathName == $"/{pathElements[i]}");
        }
        
        parentItem.DirectoryItems.RemoveAll(x => x.PathName == $"/{pathElements[^1]}");
    }

    public string[] LsDir(string path)
    {
        if (path.Contains("//"))
            throw new Exception("Invalid path name");
        
        path = path.EndsWith('/') ? path.Remove(path.LastIndexOf('/')) : path;
        var pathElements = path.Split('/');

        if(pathElements.Length == 1) 
            return _root.DirectoryItems.Select(x => x.PathName).ToArray();
        
        var requestedPath = _root;
        
        for (var i = 1; i < pathElements.Length; i++)
        {
            requestedPath = requestedPath.DirectoryItems.First(x => x.PathName ==  $"/{pathElements[i]}");
        }

        var returnValue = new List<string>();

        foreach (var directoryItem in requestedPath.DirectoryItems)
        {
            var fullPath = new List<string> { directoryItem.PathName };
            var parent = directoryItem.Parent;
            while (parent != null)
            {
                fullPath.Add(parent.PathName);
                parent = parent.Parent;
            }

            fullPath.Reverse();
            fullPath.RemoveAt(0);
            returnValue.Add(string.Join("", fullPath));
        }

        return returnValue.ToArray();
    }
}