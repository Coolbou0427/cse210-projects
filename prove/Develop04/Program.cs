using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MindfulnessActivity breathing = new BreathingActivity();
                    breathing.Run();
                    break;
                case "2":
                    MindfulnessActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    break;
                case "3":
                    MindfulnessActivity listing = new ListingActivity();
                    listing.Run();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nah, invalid choice. Try again.");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}

abstract class MindfulnessActivity
{
    protected string _activityName;
    protected string _description;
    protected int _duration;

    public void Run()
    {
        DisplayStartingMessage();
        SetDuration();
        Prepare();
        ExecuteActivity();
        DisplayEndingMessage();
    }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_activityName}.");
        Console.WriteLine(_description);
    }

    protected void SetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        if (int.TryParse(Console.ReadLine(), out int seconds))
            _duration = seconds;
        else
        {
            Console.WriteLine("Invalid input, defaulting to 30 seconds.");
            _duration = 30;
        }
    }

    protected void Prepare()
    {
        Console.WriteLine("Get ready...");
        ShowCountdownAnimation(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Good job!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed the {_activityName} for {_duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowCountdownAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    protected void ShowSpinner(int seconds)
    {
        int spinnerCounter = 0;
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        while (DateTime.Now < endTime)
        {
            spinnerCounter++;
            switch (spinnerCounter % 4)
            {
                case 0: Console.Write("/ "); break;
                case 1: Console.Write("- "); break;
                case 2: Console.Write("\\ "); break;
                case 3: Console.Write("| "); break;
            }
            Thread.Sleep(250);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    protected abstract void ExecuteActivity();
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        _activityName = "Breathing Activity";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in... ");
            ShowCountdownAnimation(4);
            if (DateTime.Now >= endTime) break;
            Console.Write("Breathe out... ");
            ShowCountdownAnimation(6);
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        _activityName = "Reflection Activity";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. Recognize your power and how you can use it in other aspects of your life.";
    }

    protected override void ExecuteActivity()
    {
        Random random = new Random();
        int promptIndex = random.Next(_prompts.Count);
        Console.WriteLine(_prompts[promptIndex]);
        ShowSpinner(3);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            int questionIndex = random.Next(_questions.Count);
            Console.WriteLine(_questions[questionIndex]);
            ShowSpinner(3);
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        _activityName = "Listing Activity";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    protected override void ExecuteActivity()
    {
        Random rand = new Random();
        int promptIndex = rand.Next(_prompts.Count);
        Console.WriteLine(_prompts[promptIndex]);
        ShowCountdownAnimation(3);
        Console.WriteLine("Start listing items. Press Enter after each item:");

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    items.Add(input);
            }
            Thread.Sleep(100);
        }
        Console.WriteLine($"You listed {items.Count} items.");
    }
}
