// See https://aka.ms/new-console-template for more information
using Decorator;

// Console.WriteLine("Hello, World!");

var pr = new ProductResponse
{
  Code = "12345",
  SellingPrice = 1000,
  ByingPrice = 800,
  UnitString = "個",
  Name = "商品A"
};
Console.WriteLine(pr.ToJson());

var prv2 = new ProductResopnseV2
{
  Code = "12345",
  SellingPrice = 1000,
  ByingPrice = 800,
  UnitString = "個",
  Name = "商品A",
  Maker = "メーカーA",
  MakerCode = "MA"
};
Console.WriteLine(prv2.ToJson());

var prdv2 = new ProductResponseV1ToV2Decorator
{
  Data = pr,
  Maker = "メーカーB",
  MakerCode = "MB"
};
Console.WriteLine(prdv2.ToJson());

var prdv3 = new ProductResponseV2ToV3Decorator
{
  Data = prv2,
  VatRate = 0.05
};
Console.WriteLine(prdv3.ToJson());