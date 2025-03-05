namespace C09_Bridge {
  public struct Product {
    public string Name;
    public int Price;
  }
  public interface VendorImpl {
    void RawOpen();
    void RawSelectProduct();
    void RowOutputProduct();
    void RawClose();
  }
  public class Vendor {
    protected VendorImpl impl;

    public Vendor(VendorImpl impl) {
      this.impl = impl;
    }
    /// <summary>
    /// 自販機購入シーケンスの開始
    /// </summary>
    public void Open() {
      impl.RawOpen();
    }
    /// <summary>
    /// 商品選択
    /// </summary>
    public void SelectProduct() {
      impl.RawSelectProduct();
    }
    /// <summary>
    /// 商品排出
    /// </summary>
    public void OutputProduct() {
      impl.RowOutputProduct();
    }
    /// <summary>
    /// 自販機購入シーケンスの終了
    /// </summary>
    public void Close() {
      impl.RawClose();
    }
    /// <summary>
    /// 一連の販売動作
    /// </summary>
    public void Sell() {
      Open();
      SelectProduct();
      OutputProduct();
      Close();
    }
  }
  public interface LotteryImpl {
    public bool RawLottery();
  }
  public class LotteryVendor : Vendor {
    protected LotteryImpl lotteryImpl;
    public LotteryVendor(VendorImpl impl1, LotteryImpl impl2) : base(impl1) {
      lotteryImpl = impl2;
    }
    /// <summary>
    /// 当たりが出たらもう一本商品を選択できる場合の一連の販売動作
    /// </summary>
    public void SellWithLottery() {
      Open();
      SelectProduct();
      Sell();
      // 当たりが出たらもう一本
      if(lotteryImpl.RawLottery()) {
        SelectProduct();
        OutputProduct();
      }
      Close();
    }
  }

  public class CanVendorImpl : VendorImpl {
    public void RawOpen() {
      System.Console.WriteLine("商品販売を開始します");
    }
    public void RawSelectProduct() {
      System.Console.WriteLine("商品を選択してください");
      // TODO: 商品選択プロンプト
    }
    public void RowOutputProduct() {
      System.Console.WriteLine("商品を排出します");
      // TODO: 選択した商品をストックから出して排出する
    }
    public void RawClose() {
      System.Console.WriteLine("商品販売を終了します");
    }
  }

  public class BottleVendorImpl : VendorImpl {
    public void RawOpen() {
      System.Console.WriteLine("商品販売を開始します");
    }
    public void RawSelectProduct() {
      System.Console.WriteLine("商品を選択してください");
      // TODO: 番号で商品選択させる
    }
    public void RowOutputProduct() {
      System.Console.WriteLine("商品を排出します");
      // TODO: 選択した商品の床をネジネジして商品を出す
    }
    public void RawClose() {
      System.Console.WriteLine("商品販売を終了します");
    }
  }

  /// <summary>
  /// 4桁の数字が全て同じ場合に当たりとする実装
  /// </summary>
  public class DecimalLotteryImpl : LotteryImpl {
    public bool RawLottery() {
      var d =  new System.Random().Next(1000);
      return JudgeDecimal(d);
    }
    /// <summary>
    /// 4桁の数字が全て同じかどうか判定
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    protected bool JudgeDecimal(int d) {
      var cs = d.ToString("%04d").ToCharArray();
      var c = cs.First();
      return cs.All(x => x == c);
    }
  }

  public class SlotLotteryImpl : LotteryImpl {
    public bool RawLottery() {
      var p1 = Picture.CreatePictureRandom();
      var p2 = Picture.CreatePictureRandom();
      var p3 = Picture.CreatePictureRandom();
      return JudgeSlot(new Picture[]{p1, p2, p3});
    }

    /// <summary>
    /// 絵柄が全て同じかどうか判定
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    protected bool JudgeSlot(Picture[] ps) {
      var n = ps.First().Name;
      return ps.All(p => p.Name == n);
    }

    /// <summary>
    /// 絵柄クラス
    /// SlotLotteryImpl内でのみ使用する
    /// </summary>
    protected class Picture {
      public string Name;

      private static string[] Pictures = new string[] { "BAR", "7", "CHERRY" };

      private Picture(string name) {
        Name = name;
      }

      public static Picture CreatePictureRandom() {
        var r = new System.Random().Next(Pictures.Length);
        string? name;
        if(r < 10) {
          name = "BAR";
        } else if(r < 30) {
          name = "7";
        } else {
          name = "CHERRY";
        }
        return new Picture(name);
      }
    }
  }
}