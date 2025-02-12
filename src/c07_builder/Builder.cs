namespace C07_Builder {

  public class Main {
    public static void Run() {
      var excelBuilder = new ExcelEstimateBuilder();
      var director = new Director(excelBuilder);
      director.Construct();
      excelBuilder.WriteExcelFile("estimate.xlsx");

      var pdfBuilder = new PdfEstimateBuilder();
      director = new Director(pdfBuilder);
      director.Construct();
      pdfBuilder.WritePDFFile("estimate.pdf");
    }
  }

  public class Director {
    private IEstimateBuilder builder;
    public Director(IEstimateBuilder builder) {
      this.builder = builder;
    }
    public IEstimateBuilder Construct() {
      // ここでbuilderを使ってEstimateを構築する
      var customer = new Organizaion {
        Name = "顧客A",
        ZipCode = "123-4567",
        Address = "東京都千代田区1-1-1",
        Tel = "03-1234-5678",
        Email = "customer@example.com"
      };
      var vendor = new Organizaion {
        Name = "発注先B",
        ZipCode = "987-6543",
        Address = "東京都渋谷区2-2-2",
        Tel = "03-9876-5432",
        Email = "vendor@example.com"
      };
      builder.SetStamp(new Image());

      builder.SetCustomerInfo(customer);
      builder.SetVenderInfo(vendor);
      builder.AddCaption("見積もり");
      builder.AddDetal(new EstimateDetail {
        ProductName = "商品A",
        UnitPrice = 1000,
        Quantity = 2,
        Price = 2000
      });
      builder.AddDetal(new EstimateDetail {
        ProductName = "商品B",
        UnitPrice = 2000,
        Quantity = 3,
        Price = 6000
      });
      builder.AddVerticalLine();
      builder.AddSubTotal();
      builder.AddTotal();

      return this.builder;
    }
  }

  public interface IEstimateBuilder {
    public void SetCustomerInfo(Organizaion org);
    public void SetvendorInfo(Organizaion org);
    public void SetStamp(Image img);
    public void AddCaption(string caption);
    public void AddDetal(EstimateDetail detail));
    public void AddVerticalLine();
    public void AddSubTotal();
    public void AddTotal();
  }

  public class ExcelEstimateBuilder : IEstimateBuilder {
    public void SetCustomerInfo(Organizaion org) {
      // 顧客情報をExcelの特定セルに設定する
    }
    public void SetvendorInfo(Organizaion org) {
      // 発注先情報をExcelの特定セルに設定する
    }
    public void SetStamp(Image img) {
      // スタンプ画像をExcelの特定セル位置に設定する
    }
    public void AddCaption(string caption) {
      // キャプションをExcelに追加する
    }
    public void AddDetal(EstimateDetail detail) {
      // 明細をExcelに追加する
    }
    public void AddVerticalLine() {
      // 縦線をExcelに追加する
    }
    public void AddSubTotal() {
      // 小計をExcelに追加する
    }
    public void AddTotal() {
      // 合計をExcelに追加する
    }

    public void WriteExcelFile(string path) {
      // Excelファイルを出力する
    }
  }

  public class PdfEstimateBuilder : IEstimateBuilder {
    public void SetCustomerInfo(Organizaion org) {
      // 顧客情報をPDFに設定する
    }
    public void SetvendorInfo(Organizaion org) {
      // 発注先情報をPDFに設定する
    }
    public void SetStamp(Image img) {
      // スタンプをPDFの特定位置に設定する
    }
    public void AddCaption(string caption) {
      // キャプションをPDFに追加する
    }
    public void AddDetal(EstimateDetail detail) {
      // 明細をPDFに追加する
    }
    public void AddVerticalLine() {
      // 縦線をPDFに追加する
    }
    public void AddSubTotal() {
      // 小計をPDFに追加する
    }
    public void AddTotal() {
      // 合計をPDFに追加する
    }

    public void WritePDFFile(string path) {
      // PDFファイルを出力する
    }
  }

  /// <summary>
  /// 組織情報。顧客情報や発注先情報などに使う
  /// </summary>
  public class Organizaion {
    public string Name {get; set;}
    public string ZipCode {get; set;}
    public string Address {get; set;}
    public string Tel {get; set;}
    public string Email {get; set;}
    public string ContactStaffName {get; set;}
  }

  public class Image {
    // 何等かの画像データを保持するクラス
  }

  public class EstimateDetail {
    public string ProductName {get; set;}
    public int UnitPrice {get; set;}
    public int Price {get; set;}
    public int Quantity {get; set;}
  }
}