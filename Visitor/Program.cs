// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var f1 = new File("file1", 100);
Console.WriteLine("--- File ---");
f1.Accept(new ConcreteVisitor());

var d1 = new Directory("dir1");
d1.Add(f1);
d1.Add(new File("file2", 200));
d1.Add(new File("file3", 300));
Console.WriteLine("--- Dir1 ---");
d1.Accept(new ConcreteVisitor());

var d2 = new Directory("dir2");
d1.Add(d2);
d2.Add(new File("file4", 400));
d2.Add(new File("file5", 500));
Console.WriteLine("--- Dir1 added Dir2 ---");
d1.Accept(new ConcreteVisitor());