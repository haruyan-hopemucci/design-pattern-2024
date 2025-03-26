using System.Text.Json;

namespace Decorator
{
  /// <summary>
  /// ProducResponseの基底クラス
  /// </summary>
  public abstract class AbstractProductResponse
  {
    public abstract string ToJson();

    /// <summary>
    /// プロパティをDictionaryに変換する 
    /// </summary>
    /// <returns></returns>
    public virtual IDictionary<string, object> PropatiesToDictionary()
    {
      var dictionary = new Dictionary<string, object>();
      foreach (var propaty in GetType().GetProperties())
      {
        var value = propaty.GetValue(this);
        if (value != null)
        {
          dictionary.Add(propaty.Name, value);
        }
      }
      return dictionary;
    }
  }

  /// <summary>
  /// ProductResponseの具象クラス(初期バージョン)
  /// </summary>
  public class ProductResponse : AbstractProductResponse
  {
    public required string Code { get; set; }
    public double SellingPrice { get; set; }
    public double ByingPrice { get; set; }
    public required string UnitString { get; set; }
    public required string Name { get; set; }

    public override string ToJson()
    {
      return JsonSerializer.Serialize(PropatiesToDictionary());
    }
  }

  public class ProductResopnseV2 : ProductResponse
  {
      public string Version => "V2";
    public required string Maker { get; set; }
    public required string MakerCode { get; set; }
  }

  public abstract class ProductResponseDecorator : AbstractProductResponse
  {
    public required AbstractProductResponse Data { get; set; }
    public override IDictionary<string, object> PropatiesToDictionary()
    {
      var dictionary = Data.PropatiesToDictionary();
      return dictionary;
    }
  }

  /// <summary>
  /// ProductResponseからV2仕様のJSONを生成するDecorator
  /// </summary>
  public class ProductResponseV1ToV2Decorator : ProductResponseDecorator
  {
      public string Version => "V2";
      public required string Maker { get; set; }
      public required string MakerCode { get; set; }
    public override string ToJson()
    {
      // Data が ProductResponse でなければ例外を投げる
      if (!(Data is ProductResponse))
      {
        throw new InvalidOperationException("Data is not ProductResponse");
      }
      var dictionary = PropatiesToDictionary();
      dictionary.Add("Version", Version);
      dictionary.Add("Maker", Maker);
      dictionary.Add("MakerCode", MakerCode);
      return JsonSerializer.Serialize(dictionary);
    }
  }

  /// <summary>
  /// V2仕様のデータオブジェクトかデコレーターからV3仕様のJSONを生成するDecorator
  /// </summary>
  public class ProductResponseV2ToV3Decorator : ProductResponseDecorator
  {
    public string Version => "V3";
    public required double VatRate { get; set; }
    public override string ToJson()
    {
      // Dataを一旦JSONに変換し、Dictionaryにデシリアライズする
      var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(Data.ToJson());

      // Data のバージョンが V2 でなければ例外を投げる
      if (!(dictionary is not null && dictionary.ContainsKey("Version") && dictionary["Version"].ToString() == "V2"))
      {
        throw new InvalidOperationException("Data is not V2");
      }
      dictionary.Add("VatRate", VatRate);
      dictionary["Version"] = Version;
      return JsonSerializer.Serialize(dictionary);
    }
  }
}