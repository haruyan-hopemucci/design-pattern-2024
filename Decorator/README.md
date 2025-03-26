# Decorator

同じAPIを持つベースオブジェクトが存在する

そのオブジェクトの派生クラスは、別の派生クラスを内包してベースオブジェクトと同じように振る舞うことができる

飾り付け(Decorator)というより、wrapper(ラッピング)と考えたほうが日本人的にはしっくり来るのかも知れない

## サンプル: APIオブジェクトのバージョンデコレータ

あるAPIのデータを表すオブジェクトがあり、どんどんバージョンアップしている。

バージョンアップするたびに破壊的変更が入ったりするが、旧バージョンのオブジェクトも新バージョンのオブジェクトと同一として扱いたい。

バージョンごとにDecoratorを使って旧バージョンを内包していく。

```mermaid
classDiagram

class AbstractProductsResponse {
  <<abstract>>
  ToJson()*
  PropatiesToDictionary()
}

class ProductResopnse {
  <<初期に作られたレスポンスオブジェクト>>
  string Code
  double SellingPrice
  double ByingPrice
  string UnitString
  string Mame
  ToJson()
}

class ProductResponseDecorator {
  <<abstract>>
  ProductResponse Data
}

class ProductResopnseV2 {
  string version : APIバージョン
  string Maker : 商品のメーカー
  string MakerCode : メーカー側が発番している商品コード
}

class ProductResopnseV3 {
  double VatRate : 消費税率
}

class ProductResponseV4 {
  string[] MakerCodes : メーカーコードが複数存在するProduct対応
}

class ProductResponseV1ToV2Decorator {
  string version : APIバージョン
  string Maker : 商品のメーカー
  string MakerCode : メーカー側が発番している商品コード
  ToJson()
}

class ProductResponseV2ToV3Decorator {
  double VatRate : 消費税率
  ToJson()
}

class ProductResponseV3ToV4Decorator {
  string[] MakerCodes : メーカーコードが複数存在するProduct対応
  ToJson()
}

AbstractProductsResponse <|-- ProductResopnse
AbstractProductsResponse <|-- ProductResponseDecorator
ProductResponseDecorator <|-- ProductResponseV1ToV2Decorator
ProductResponseDecorator <|-- ProductResponseV2ToV3Decorator
ProductResponseDecorator <|-- ProductResponseV3ToV4Decorator
ProductResponse <|-- ProductResponseV2
ProductResponseV2 <|-- ProductResponseV3
ProductResponseV3 <|-- ProductResponseV4
ProductResponseV1ToV2Decorator o--> ProductResponse : uses
ProductResponseV2ToV3Decorator o--> ProductResponseV2 : uses
ProductResponseV2ToV3Decorator o--> ProductResponseV1ToV2Decorator : uses
ProductResponseV3ToV4Decorator o--> ProductResponseV3 : uses
ProductResponseV3ToV4Decorator o--> ProductResponseV2ToV3Decorator : 

```