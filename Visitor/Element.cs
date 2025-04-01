public interface IElement
{
  public abstract void Accept(Visitor visitor);
}

public abstract class Entry : IElement
{
  public abstract string Name { get; }
  public abstract int Size { get; }

  public void Accept(Visitor visitor)
  {
    // 
    dynamic v = this;
    visitor.Visit(v);
  }
  public override string ToString()
  {
    return $"{Name} ({Size})";
  }
}

public class File : Entry
{
  public override string Name { get; }
  public override int Size { get; }

  public File(string name, int size)
  {
    Name = name;
    Size = size;
  }
}

public class Directory : Entry
{
  public override string Name { get; }
  public override int Size => _entries.Count;

  private List<Entry> _entries = new List<Entry>();

  public Directory(string name)
  {
    Name = name;
  }

  public void Add(Entry entry)
  {
    _entries.Add(entry);
  }

  public IEnumerable<Entry> Entries => _entries;
}
 