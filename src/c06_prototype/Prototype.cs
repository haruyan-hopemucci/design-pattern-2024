namespace C06_Prototype {
  public class GoodsManager {
    private IDictionary<string, Goods> prototypes = new Dictionary<string, Goods>();
    public void Register(Goods goods) {
      // Identifierがちゃんとセットされていないと登録させない
      if(goods.Identifier == null) {
        throw new ArgumentException("Goods must have an identifier.");
      }
      this.prototypes[goods.Identifier] = goods;
    }
    public Goods Create(string identifier) {
      return this.prototypes[identifier].CreateClone();
    }
  }

  public interface Goods {
    public string? Identifier{get; set;}
    public Image? PrintingImage{get; set;}
    public Goods CreateClone();
    public Recipe GetProductRecipe();
  }

  public sealed class Image {
    // 何等かの画像データを保持するクラス
  }
  
  public class Recipe {
    // 商品の製造方法を保持するクラス
  }

  public class AcrylGoods : Goods {
    public string? Identifier{get; set;}
    public Image? PrintingImage{get; set;}
    public Size3D? Size{get; set;}
    public Shape? Shape{get; set;}
    public Recipe GetProductRecipe() {
      var rp = new Recipe();
      // TODO: アクリル製品の製造方法をrpに設定して返す
      return rp;
    }
    public Goods CreateClone() {
      return new AcrylGoods {
        Identifier = this.Identifier,
        PrintingImage = this.PrintingImage,
        Size = this.Size,
        Shape = this.Shape
      };
    }
  }

  public class Sticker : Goods {
    public string? Identifier{get; set;}
    public Image? PrintingImage{get; set;}
    public Material? Material{get; set;}
    public List<Shape> CutLines{get; set;} = new List<Shape>();
    public Recipe GetProductRecipe() {
      var rp = new Recipe();
      // TODO: ステッカーの製造方法をrpに設定して返す
      return rp;
    }
    public Goods CreateClone() {
      // return new Sticker {
      //   Identifier = this.Identifier,
      //   PrintingImage = this.PrintingImage,
      //   Material = this.Material,
      //   CutLines = this.CutLines
      // };
      return (Goods)this.MemberwiseClone();
    }
  }
  public class Size3D {
    public double Width{get; set;}
    public double Height{get; set;}
    public double Depth{get; set;}
  }

  public sealed class Shape {
    // 何等かの形状データを保持するクラス
  }

  public sealed class Material {
    // シールの材質等のデータを保持するクラス
  }

  public class Main {
    public static void Run() {
      var manager = new GoodsManager();
      var acrylGoods = new AcrylGoods {
        Identifier = "acryl001",
        PrintingImage = new Image(),
        Size = new Size3D { Width = 10, Height = 20, Depth = 5 },
        Shape = new Shape()
      };
      var acrylGoods2 = new AcrylGoods {
        Identifier = "acryl002",
        PrintingImage = new Image(),
        Size = new Size3D { Width = 30, Height = 30, Depth = 30 },
        Shape = new Shape()
      };
      var sticker = new Sticker {
        Identifier = "sticker001",
        PrintingImage = new Image(),
        Material = new Material(),
        CutLines = new List<Shape> { new Shape(), new Shape() }
      };
      // 実際はsticker1とは形状の違うものだと思ってください
      var sticker2 = new Sticker {
        Identifier = "sticker002",
        PrintingImage = new Image(),
        Material = new Material(),
        CutLines = new List<Shape> { new Shape(), new Shape() }
      };
      // 同じ種類のプロダクトでも寸歩や形状が異なるものをそれぞれプロトタイプとして登録する
      manager.Register(acrylGoods);
      manager.Register(acrylGoods2);
      manager.Register(sticker);
      manager.Register(sticker2);

      // プロトタイプを使って新しい商品を作る
      var acrylClone = manager.Create("acryl001");
      // acrylClone をカスタマイズして新しい商品を作る
      var stickerClone = manager.Create("sticker001");
      // stickerClone をカスタマイズして新しい商品を作る
    }
  }
}