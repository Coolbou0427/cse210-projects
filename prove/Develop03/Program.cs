using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Scripture[] scriptures = new Scripture[]
        {
            // For creativity, the program uses a library of scriptures below and randomly picks one.
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        Random rnd = new Random();
        Scripture scripture = scriptures[rnd.Next(scriptures.Length)];

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit:");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;
            scripture.HideRandomWords(3);
        }
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Press any key to exit.");
        Console.ReadKey();
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        _words = new List<Word>();
        string[] splitWords = text.Split(' ');
        foreach (var word in splitWords)
            _words.Add(new Word(word));
    }

    public string GetDisplayText()
    {
        string display = Reference.ToString() + "\n";
        foreach (var word in _words)
            display += word.GetDisplay() + " ";
        return display.Trim();
    }

    public void HideRandomWords(int count)
    {
        Random rnd = new Random();
        List<Word> visibleWords = _words.FindAll(w => !w.IsHidden);
        if (visibleWords.Count < count)
            count = visibleWords.Count;
        for (int i = 0; i < count; i++)
        {
            int index = rnd.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        foreach (var word in _words)
            if (!word.IsHidden)
                return false;
        return true;
    }
}

class Word
{
    private string _text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        _text = text;
        IsHidden = false;
    }

    public void Hide() => IsHidden = true;

    public string GetDisplay() => IsHidden ? new string('_', _text.Length) : _text;
}

class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        return _startVerse == _endVerse
            ? $"{_book} {_chapter}:{_startVerse}"
            : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}
