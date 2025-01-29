using System;
namespace Singleton {
  /// <summary>
  /// 神龍クラス
  /// FIXME: Activateしたあとに他のユーザーがGrantWithすると、他のユーザーの願い事を叶えてしまう。
  /// </summary>
  public class Shenron {
    /// <summary>
    /// 正しく呼び出されたかどうかを判定するフラグ。これがtrueの場合のみ願い事を叶えることができる。
    /// </summary>
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
      // TODO: 呼び出しの際の呪文が正しいかチェック
      isActivated = true;
    }
    public void GrantWish(string wish) {
      if (!isActivated) {
        return;
      }
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
    static private int MAX_NUMBER = 7;
    public int Number { get; set; }
    static private IDictionary<int, DragonBall> balls = new Dictionary<int, DragonBall>();
    private DragonBall(int number) {
      if(number < 1 || number > 8) {
        throw new ArgumentException("DragonBall number must be between 1 and 7");
      }
      Number = number;
      // すでに同じ番号で登録されている場合は、dicに登録しない
      if(!balls.ContainsKey(number)) {
        balls[number] = this;
      }
    }
    public static DragonBall GetDragonBall(int number) {
      if (!balls.ContainsKey(number)) {
        new DragonBall(number);
      }
      return balls[number];
    }
    public static bool IsNearBy(DragonBall[] balls){
      if (balls.Length != MAX_NUMBER) {
        return false;
      }
      // TODO: 7つのドラゴンボールが近傍に固まっているかチェックしてから
      return true;
    }
  }

  public class Main {
    public static void Execute() {
      // ピラフ側
      Shenron shenron = Shenron.GetInstance();
      DragonBall[] balls = new DragonBall[7];
      for (int i = 0; i < 7; i++) {
        balls[i] = DragonBall.GetDragonBall(i + 1);
      }
      shenron.Activate("いでよ神龍、願いを叶え給え", balls);
      shenron.GrantWish("世界征服したい");
    }

    public static void ExecuteUhron() {
      
      Shenron shenron = Shenron.GetInstance();
      shenron.GrantWish("ギャルのパンティをおくれー!");
    }
  }
}