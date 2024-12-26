using System.Linq;

namespace DesignPatterns.Adapter
{
  public class Main
  {
    public static void Execute()
    {
      var register = new Register();
      register.AddProduct(new TaxesAdaptedProduct { Type = ProductType.Food, Name = "りんご", Price = 100 });
      register.AddProduct(new TaxesAdaptedProduct { Type = ProductType.Food, Name = "ゴルゴ13全巻セット", Price = 10000 });
      register.AddProduct(new TaxesAdaptedProduct { Type = ProductType.NewsPaper, Name = "東スポ", Price = 200 });
      register.PrintReceipt();
    }
  }

  public enum ProductType
  {
    Food = 0,   // 8%
    NewsPaper,  // 8%
    Book = 10000,  // int値で10000以上の商品種類は消費税10%
    Video
  }

  public class Register
  {
    IList<Product> _products = new List<Product>();

    public void AddProduct(Product product)
    {
      _products.Add(product);
    }

    public void PrintReceipt()
    {
      // ヘッダとして店名とかを出力
      foreach (var product in _products)
      {
        Console.WriteLine(product.ToStringWithVat());
      }
      // フッタとして合計金額とかを出力
    }
  }

  /**
    * Targetに相当
    */
  public class Product
  {
    public ProductType Type { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }

    public virtual string ToStringWithVat()
    {
      return $"{Name} : {Price} 円";
    }
  }

  /**
    * Adapterに相当
    */
  public class TaxesAdaptedProduct : Product
  {
    private Vat10Printer<Product>? _vat10Printer;
    private Vat10Printer<Product> Vat10Printer {get{
      _vat10Printer ??= new Vat10Printer<Product>(this);
      return _vat10Printer;
    }}

    public override string ToStringWithVat()
    {
      return $"{Name} : {Vat10Printer} 円";
    }
  }

  /**
    * Adapteeに相当
    * 8%内税方式の金額から税抜き金額や商品の種類に応じた税率、税込金額を計算するクラス
    * 十分にテストされていると仮定
    * 本当はwhere句はインターフェースにしたいが今回は妥協
    */
  public class Vat10Printer<T> where T : Product
  {
    private T product;
    public Vat10Printer(T product)
    {
      this.product = product;
    }
    public decimal InternalTaxPrice()
    {
      // productのPriceは内税方式の金額なので、そのまま帰す
      return product.Price;
    }
    public decimal ExternalTaxPrice()
    {
      // 内税方式の商品金額から税抜き金額を計算して返す
      return product.Price / (1m + 0.08m);
    }
    public decimal VatRate()
    {
      // 商品の種類に応じて消費税率を返す
      return (int)product.Type < 10000 ? 0.08m : 0.1m;
    }
    public decimal Vat()
    {
      // 商品の種類に応じて消費税を計算して返す
      return ExternalTaxPrice() * VatRate();
    }
    public string ToString(Product product)
    {
      return $"{product.Name} : {ExternalTaxPrice()}円 消費税{VatRate()*100}% {Vat()} 円";
    }
  }
}