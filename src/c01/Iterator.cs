using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace C01
{
  // Iterator interface
  public interface IIterator<T>
  {
    bool HasNext();
    T Next();
  }

  // Aggregate interface
  public interface IAggregate<T>
  {
    IIterator<T> CreateIterator();
  }

  /**
   * Concrete Aggregateに相当
   * ひらがな50音の文字列を保持するクラス
   */  
  public class HiraganaLetters : IAggregate<string>
  {
    private readonly string[] _letters = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもや＝ゆ＝よらりるれろわ＝＝＝をん".Select(x => x.ToString()).ToArray();

    public IIterator<string> CreateIterator()
    {
      return new HiraganaIterator(_letters);
    }

    public IIterator<string> CreateVerticalIterator()
    {
      return new HiraganaHorizontalIterator(_letters);
    }
  }

  /**
   * Concrete Iteratorに相当
   * ひらがな50音の文字列を順番に返すクラス
   */
  public class HiraganaIterator : IIterator<string>
  {
    private readonly string[] _letters;
    private int _index = 0;

    public HiraganaIterator(string[] letters)
    {
      _letters = letters;
    }

    public bool HasNext()
    {
      return _index < _letters.Length;
    }

    public string Next()
    {
      return _letters[_index++];
    }
  }

  /**
   * Concrete Iteratorに相当
   * ひらがな50音の文字列を横方向に返すクラス
   */
  public class HiraganaHorizontalIterator : IIterator<string>
  {
    private readonly string[] _letters;
    private int _index = 0;
    private int _horizontalIndex = 0;
    public HiraganaHorizontalIterator(string[] letters)
    {
      _letters = letters;
    }

    public bool HasNext()
    {
      return _horizontalIndex < 5;
    }

    public string Next()
    {
      string letter = _letters[_index];
      _index += 5;
      if(_index >= _letters.Length)
      {
        _horizontalIndex++;
        _index = _horizontalIndex;
      }
      return letter;
    }
  }

  public class Runner
  {
    public static void Run()
    {
      var aggregate = new HiraganaLetters();

      var iterator = aggregate.CreateIterator();
      while (iterator.HasNext())
      {
        Console.Write(iterator.Next());
      }
      Console.WriteLine();
      var it2 = aggregate.CreateVerticalIterator();
      while (it2.HasNext())
      {
        Console.Write(it2.Next());
      }
      Console.WriteLine();
    }
  }
}