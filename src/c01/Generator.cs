using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace C01
{
  public enum GachaRareType
  {
    SR = 0,
    SSR = 1,
    UR = 2
  }

  public class GachaCard
  {
    public string Name { get; set; }
    public GachaRareType Rarity { get; set; }
    
    public GachaCard(string name, GachaRareType rarity)
    {
      Name = name;
      Rarity = rarity;
    }
  }

  /**
   * Concrete Aggregateに相当
   * ガチャで排出されるカードセットを保持するクラス
   */
  public class GachaCardSet : IAggregate<GachaCard>
  {
    public List<GachaCard> Cards { get; set; }
    public string Name { get; set; }

    public GachaCardSet(string name)
    {
      Name = name;
      Cards = new List<GachaCard>();
    }
    public GachaCardSet(string name, List<GachaCard> cards)
    {
      Cards = cards;
      Name = name;
    }
    public IIterator<GachaCard> CreateIterator()
    {
      return new GachaIterator(this);
    }
  }
  /**
   * ガチャシステム管理クラス
   */
  public class GachaSystem
  {
    public IDictionary<string, GachaCardSet> CardSets { get; set; }

    public GachaSystem()
    {
      CardSets = new Dictionary<string, GachaCardSet>();
    }

    // TODO: ここにカードセットごとにIteratorを生成するメソッドを追加する
  }

  public class GachaIterator : IIterator<GachaCard>
  {
    protected readonly GachaCardSet _cardSet;
    protected readonly int[] DrawRates;
    protected readonly int[] DrawRatesAccumulated;
    protected readonly int RateSum;

    public GachaIterator(GachaCardSet cardSet, int[] rates)
    {
      DrawRates = rates;
      DrawRatesAccumulated = DrawRates
        .Select((rate, index) => DrawRates.Take(index + 1).Sum())
        .ToArray();
      RateSum = DrawRates.Sum();
      _cardSet = cardSet;
    }

    public GachaIterator(GachaCardSet cardSet) : this(cardSet, new int[] { 80, 15, 5 })
    {
      // SR: 80%, SSR: 15%, UR: 5% が基本確率とする
    }

    public bool HasNext()
    {
      // 無限に回せるようにする
      return true;
    }

    public virtual GachaCard Next()
    {
      // 排出するカードのレアリティを決定
      var random = new Random().Next(RateSum);
      var rarity = (GachaRareType)DrawRatesAccumulated.Aggregate(0, (sum, rate) => rate < random ? ++sum : sum );
      // 指定されたレアリティのカードをランダムに選択
      var cards = _cardSet.Cards.Where(card => card.Rarity == rarity).ToArray();
      return cards[new Random().Next(cards.Length)];
    }
  }

  public class VipGachaIterator : GachaIterator
  {
    public VipGachaIterator(GachaCardSet cardSet) : base(cardSet, new int[] { 85, 14, 1 })
    {
      // 廃課金ユーザーにはレアが出る確率を下げる(実際に某ソシャゲで行われていた)
    }
  }

  /**
    * Concrete Iteratorに相当
    * 一定回数ガチャを引いたらURが排出されるようにする(いわゆる天井)
    */
  public class CeilingedGachaIterator : GachaIterator
  {
    protected int DrawCount = 0;
    protected int Ceiling {get { return 100; } }

    public CeilingedGachaIterator(GachaCardSet cardSet) : base(cardSet)
    {
    }

    public override GachaCard Next()
    {
      DrawCount++;
      if (DrawCount >= Ceiling)
      {
        DrawCount = 0;
        var urCardSet = _cardSet.Cards.Where(card => card.Rarity == GachaRareType.UR).ToArray();
        return urCardSet[new Random().Next(urCardSet.Length)];
      } else {
        return base.Next();
      }
    }
  }
}