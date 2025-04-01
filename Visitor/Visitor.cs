public abstract class Visitor
{
  public abstract void Visit(File file);
  public abstract void Visit(Directory directory);
}

public class ConcreteVisitor : Visitor
{
  private int _level = 0;
  public override void Visit(File file)
  {
    Console.WriteLine("".PadLeft(_level) + $"{file.Name} ({file.Size} bytes)");
  }
  public override void Visit(Directory directory)
  {
    Console.WriteLine("".PadLeft(_level) + $"{directory.Name} ({directory.Size} files)");
    _level++;
    foreach (var entry in directory.Entries)
    {
      entry.Accept(this);
    }
    _level--;
  }
}