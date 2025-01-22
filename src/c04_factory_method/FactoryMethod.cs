using System.Globalization;
using C01;

namespace FactoryMethod.Framework
{
  public abstract class MapViewer {
    public abstract void show();
  }
    public abstract class MapCreator
    {
      protected MapViewer? MapViewer { get; set; }
        public MapViewer Create(string owner)
        {
          var mv = CreateMap();
          setMapType(mv);
          setMapStyles(mv);
          setOptions(mv);
          return mv;
        }
        public abstract MapViewer CreateMap();
        public abstract void setMapType(MapViewer mv);
        public abstract void setMapStyles(MapViewer mv);
        public abstract void setOptions(MapViewer mv);
   }
   public class MapStyle {
    // スタイル指定クラス(はりぼて)
   }
   public class MapOption {
    // オプション指定クラス(はりぼて)
   }
}

namespace FactoryMethod.googlemap
{
  using System;
  using System.Collections.Generic;
  using FactoryMethod.Framework;
  public class RoadmapViewer : MapViewer
  {
    internal RoadmapViewer()
    {
      // コンストラクタ。パッケージ外からのインスタンス生成を禁止
      // C#ではinternalを使うことで同一アセンブリ内からのみアクセス可能となる
    }
    public override void show()
    {
      Console.WriteLine("通常のGoogleMapを表示");
    }
  }
  public class RoadmapCreator : MapCreator
  {
    public override MapViewer CreateMap()
    {
      return new RoadmapViewer();
    }
    public override void setMapType(MapViewer mv)
    {
      // mapTypeとして"roadmap"を設定
    }
    public override void setMapStyles(MapViewer mv)
    {
      // Style設定は特に何もしなくてOK
    }
    public override void setOptions(MapViewer mv)
    {
      // オプション設定も特に何もしなくてOK
    }
  }
  public class NoLabelSatelliteViewer : MapViewer
  {
    internal NoLabelSatelliteViewer()
    {
      // コンストラクタ。パッケージ外からのインスタンス生成を禁止
      // C#ではinternalを使うことで同一アセンブリ内からのみアクセス可能となる
    }
    public override void show()
    {
      Console.WriteLine("航空写真地図(ラベルなし)を表示");
    }
  }

  public class NoLabelSatelliteCreator : MapCreator
  {
    public override MapViewer CreateMap()
    {
      return new NoLabelSatelliteViewer();
    }
    public override void setMapType(MapViewer mv)
    {
      // mapTypeとして"satellite"を設定
    }
    public override void setMapStyles(MapViewer mv)
    {
      // Style設定は特に何もしなくてOK
    }
    public override void setOptions(MapViewer mv)
    {
      // オプション設定も特に何もしなくてOK
    }
  }

  public class NoLabelRoadmapViewer : MapViewer
  {
    internal NoLabelRoadmapViewer()
    {
      // コンストラクタ。パッケージ外からのインスタンス生成を禁止
      // C#ではinternalを使うことで同一アセンブリ内からのみアクセス可能となる
    }
    public override void show()
    {
      Console.WriteLine("GoogleMap(ラベルなし)を表示");
    }
  }

  public class NoLabelRoadmapCreator : MapCreator
  {
    public override MapViewer CreateMap()
    {
      return new NoLabelRoadmapViewer();
    }
    public override void setMapType(MapViewer mv)
    {
      // mapTypeとして"roadmap"を設定
    }
    public override void setMapStyles(MapViewer mv)
    {
      // all.labels の visibility を off に設定
    }
    public override void setOptions(MapViewer mv)
    {
      // オプション設定も特に何もしなくてOK
    }
  }
}