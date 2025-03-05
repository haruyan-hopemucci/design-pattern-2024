# Bridgeパターン

機能(API)は機能側のレイヤーに、実装(impl)は実装側のレイヤーに分ける

機能拡張や機能追加をしなければならない場合に、実装と機能定義を分けることで見通しの向上と修正箇所を小さくできる。

## 実装

自動販売機

今回は二種類の自動販売機を想定する。
- 一般的に見られる、缶飲料やペットボトル飲料を販売する自動販売機
- 銭湯の牛乳瓶自販機のような、商品を排出する際に底をネジネジする自販機

また、当たり機能付きの自販機をアドオンできるよう多段ブリッジしてみる
当たりの抽選方法も、7セグLEDの方法と絵柄スロットの方法の二種類を用意する。

```mermaid
classDiagram
namespace 機能のクラス階層 {
  class Vendor {
    <<基本の自動販売機機能>>
    impl
    Open()
    SelectProduct()
    OutputProduct()
    Close()
    Sell()
  }
  class LotteryVendor {
    <<抽選当たり機能>>
    SellWithLottery()
  }
}
namespace 実装のクラス階層 {
  class VendorImpl {
    <<intarface>>
    RawOpen()*
    RawSelectProduct()*
    RawSell()*
    RawClose()*
  }
  class CanVenderImpl {
    <<缶で販売する自販機実装>>
    RawOpen()
    RawSelectProduct()
    RawSell()
    RawClose()
  }
  class BottleVenderImpl {
    <<ビンで販売する自販機実装>>
    RawOpen()
    RawSelectProduct()
    RawSell()
    RawClose()
  }
  class LotteryImpl {
    <<interface>>
    RawLottery()*
  }
  class DecimalLotteryImpl {
    <<よくある数字が揃ったら当たりになる自販機実装>>
    RawLottery()
  }
  class SlotLotteryImpl {
    <<絵柄が揃ったら当たりになる自販機実装>>
    RawLottery()
  }
}

Vendor <|-- LotteryVendor : extends
Vendor o--> VendorImpl : uses
VendorImpl <|-- CanVenderImpl
VendorImpl <|-- BottleVenderImpl
LotteryVendor o--> LotteryImpl : uses
LotteryImpl <|-- DecimalLotteryImpl
LotteryImpl <|-- SlotLotteryImpl
```