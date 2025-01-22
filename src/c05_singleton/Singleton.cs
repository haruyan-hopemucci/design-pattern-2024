using System;
namespace Singleton {
  /// <summary>
  /// 神龍クラス
  /// FIXME: Activateしたあとに他のユーザーがGrantWithすると、他のユーザーの願い事を叶えてしまう。バグチケット参照。
  /// </summary>
  public class Shenron {
    private bool isActivated = false;
    private static Shenron instance = new Shenron();
    private Shenron() { }
    public static Shenron GetInstance() {
      return instance;
    }
    public void Activate(string spell, DragonBall[] dragonBalls) {
      // ドラゴンボールが7つそろっているかチェック
      if (!DragonBall.IsNearBy(dragonBalls)) {
        Console.WriteLine("ドラゴンボールが揃っていません");
        return;
      }
      // 呼び出しの際の呪文が正しいかチェック
      isActivated = true;
    }
    public void GrantWish(string wish) {
      if (!CanGrantWish(wish)) {
        Console.WriteLine($"{wish}は叶えられない願いです");
        return;
      } else {
        Console.WriteLine($"{wish}を叶えよう");
        isActivated = false;
      }
    }
    private bool CanGrantWish(string wish) {
      // 願い事が叶えられるものなのかチェック
      return true;
    }
  }

  public class DragonBall {
    public int Number { get; set; }
    public static bool IsNearBy(DragonBall[] balls){
      if (balls.Length != 7) {
        return false;
      }
      // 7つのドラゴンボールが近傍に固まっているかチェックしてから
      return true;
    }
  }
}