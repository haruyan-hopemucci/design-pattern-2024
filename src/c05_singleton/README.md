# Singleton

この世界にはインスタンスが1つしか存在できない!
この世界にはインスタンスが1つしか存在できないことを保証する!

というのがSingletonパターン

世界に1つしか存在しないもの、同一インスタンスで処理できなければ困る事象に対して適用するパターン
また、｢インスタンスは1つだけで、すべての処理で1つのインスタンスを使いまわして大丈夫｣という場合にも適用できる。

JavaやASP.NETのコントローラクラス、サービスクラスはこのSingletonで構成されている
(対照的にRailsのコントローラは都度インスタンスをNewしている)
Singleton→インスタンス1つを全体で共有している、であることを考慮しながら実装をすすめないと大きなしっぺ返しが発生する。
(下手にメンバ変数を実装するとすべての処理でメンバ変数の使いまわしができてしまう)

またWebシステムのように複数のユーザーから同時非同期に処理をすることになると、処理の順序や内部状態により不具合が発生する可能性がある。
内部状態は本当に必要なものでない限り持たせないのがSingletonクラスのセオリー(のはずなのだが、一度やらかさないとなかなか理解されない)

### マルチスレッドとマルチプロセス

Singletonパターンを使う際には、常にマルチスレッドへの考慮が必要。
さらに、複数のプロセスでSingletonを共有したい場合や、複数のマシンでSingletonを共有したい場合は単純なSingletonクラスでは要件を充足せず、様々な考慮が必要。

## 具体事例: 神龍システム

神龍は世界に1匹しかいないのでSingletonで表現する。
ドラゴンボールもそれぞれ世界に1つしかないのでSingletonで表現できる
(今回は簡略化のためにしない)

```mermaid
classDiagram
class Shenron {
  -Shenron$
  -bool IsActivate
  -Shenron() コンストラクタ
  -CanGrantWish(string wish)
  +GetInstance()$
  +Activate(string spell, DragonBall[] balls)
  +GrantWish(string wish)
}
class DragonBall {
  -Ball
  -DragonBall()
  +IsNearBy(DragonBall[] balls)$
}
```