using System.Runtime.CompilerServices;

namespace TemplateMethod
{
  public class EmptyBulletException : Exception
  {

  }
  /// <summary>
  /// 銃の抽象クラス
  /// 
  /// </summary>
  public abstract class Gun
  {
    /// <summary>
    /// 弾丸をセットする
    /// </summary>
    protected abstract void LoadBullet();
    /// <summary>
    /// 弾丸が弾倉に入っているかどうか
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsEmptyBullet();
    /// <summary>
    /// 引き金を引く
    /// </summary>
    protected abstract void PullTrigger();
    public void Fire()
    {
      LoadBullet();
      // 抽象クラス側で空撃ちを防ぐ
      if (IsEmptyBullet())
      {
        throw new EmptyBulletException();
      }
      else
      {
        PullTrigger();
      }
    }
  }
  public class MatchlockGun : Gun
  {
    protected bool isLoaded { get; set; } = false;
    protected override void LoadBullet()
    {
      Console.WriteLine("火縄に着火する");
      Console.WriteLine("火薬を詰める");
      Console.WriteLine("突き棒で固める");
      Console.WriteLine("鉄砲玉を詰める");
      Console.WriteLine("突き棒で固める");
      Console.WriteLine("火皿に口薬を引く");
      isLoaded = true;
    }
    protected override bool IsEmptyBullet()
    {
      return !isLoaded;
    }
    protected override void PullTrigger()
    {
      Console.WriteLine("ボーン!");
      isLoaded = false;
    }
  }
  public abstract class ModernGunBase : Gun
  {
    protected int bullets { get; set; } = 0;
    protected abstract int maxBullets { get; }
    protected override bool IsEmptyBullet()
    {
      return bullets <= 0;
    }
    protected override void LoadBullet()
    {
      Console.WriteLine("弾丸を装填する");
      if (bullets == 0)
      {
        bullets = maxBullets;
      }
    }
  }
  public class HandGun : ModernGunBase
  {
    protected override int maxBullets => 6;
    protected override void PullTrigger()
    {
      Console.WriteLine("Bang!");
      bullets--;
    }
  }

  /// <summary>
  /// アサルトライフル
  /// 三点バースト式
  /// </summary>
  public class AssultRifle : ModernGunBase
  {
    protected override int maxBullets => 30;
    protected override void PullTrigger()
    {
      int i = 3;
      while (i > 0 && bullets > 0)
      {
        Console.Write("パッ");
        bullets--;
        i--;
      }
      Console.WriteLine();
    }
  }

  public class Runner
  {
    public static void Run()
    {
      Gun hg = new HandGun();
      hg.Fire();
      Gun ar = new AssultRifle();
      ar.Fire();
      Gun[] guns = { hg, ar, new MatchlockGun(), new RayGun() };
      foreach (var gun in guns)
      {
        gun.Fire();
      }
    }
  }
  /// <summary>
  /// 光線銃
  /// 光子力の力で無限に発射できる。兜甲児もびっくり!
  /// </summary>
  public class RayGun : Gun
  {
    protected override void LoadBullet()
    {
      // 光線銃は自動的にエネルギーを蓄えられるため何もしなくていい
    }
    protected override bool IsEmptyBullet()
    {
      // 光線銃はエネルギーが自動的に蓄えられているので常にfalse
      return false;
    }
    protected override void PullTrigger()
    {
      Console.WriteLine("ピュー");
    }
  }
}