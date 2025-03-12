# Strategyパターン

アルゴリズムの差し替えを可能にする

実際の応用実装では、例えばListのsortメソッドで、内包している要素のカーディナリティや要素数によって最適なソートアルゴリズムを切り替えるようなパターンが利用されている。

## 例: リバーシ(オセロ)の戦略

リバーシの戦略をStrategyパターンで表現する

- とにかくたくさんひっくり返せる手を探す戦略(初級者向け)
- 角を取ることを優先する戦略(中級者向け)
- 相手の次の手の選択肢を減らすことを優先する戦略(上級者向け)

```mermaid
classDiagram
class GameField {
  <<global>>
  Field(x,y)
  Put(x,y,color)
  GameField SimulatePut(x,y,color)
}

class Player {
  Strategy
  NextHand(GameField, PlayerColor)
}

class ReversiRateStrategy {
  <<interface>>
  NextHand(GameField, PlayerColor)*
}

class GiveALotPriolityStrategy{
  <<たくさん取れる手を優先する>>
  NextHand(GameField, PlayerColor)
}

class CornerPriolityStrategy {
  <<角を取ることを優先する>>
  NextHand(GameField, PlayerColor)
}

class FewChoiceOpponentStrategy {
  <<相手の置ける手の選択肢を減らすことを優先する>>
  NextHand(GameField, PlayerColor)
}

Player o--> ReversiRateStrategy : uses
GiveALotPriolityStrategy ..|>  ReversiRateStrategy
CornerPriolityStrategy ..|>  ReversiRateStrategy
FewChoiceOpponentStrategy ..|>  ReversiRateStrategy

```