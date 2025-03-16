using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        private string _name;
        private string _description;
        private int _points;
        private bool _completed;

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public int Points { get { return _points; } }
        public bool Completed { get { return _completed; } protected set { _completed = value; } }

        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _completed = false;
        }

        public abstract int RecordEvent();
        public abstract string GetStatus();
        public abstract string Serialize();
    }

    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            if (!Completed)
            {
                Completed = true;
                return Points;
            }
            return 0;
        }

        public override string GetStatus()
        {
            if (Completed)
            {
                return "[X]";
            }
            else
            {
                return "[ ]";
            }
        }

        public override string Serialize()
        {
            return $"SimpleGoal|{Name}|{Description}|{Points}|{Completed}";
        }

        public static SimpleGoal Deserialize(string data)
        {
            string[] parts = data.Split('|');
            string name = parts[1];
            string description = parts[2];
            int points = int.Parse(parts[3]);
            bool isCompleted = bool.Parse(parts[4]);
            SimpleGoal goal = new SimpleGoal(name, description, points);
            goal.Completed = isCompleted;
            return goal;
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            return Points;
        }

        public override string GetStatus()
        {
            return "[∞]";
        }

        public override string Serialize()
        {
            return $"EternalGoal|{Name}|{Description}|{Points}";
        }

        public static EternalGoal Deserialize(string data)
        {
            string[] parts = data.Split('|');
            return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
        }
    }

    class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = 0;
        }

        public override int RecordEvent()
        {
            if (!Completed)
            {
                _currentCount++;
                if (_currentCount >= _targetCount)
                {
                    Completed = true;
                    return Points + _bonusPoints;
                }
                return Points;
            }
            return 0;
        }

        public override string GetStatus()
        {
            return Completed
                ? $"[X] Completed {_currentCount}/{_targetCount}"
                : $"[ ] Completed {_currentCount}/{_targetCount}";
        }

        public override string Serialize()
        {
            return $"ChecklistGoal|{Name}|{Description}|{Points}|{_targetCount}|{_currentCount}|{_bonusPoints}|{Completed}";
        }

        public static ChecklistGoal Deserialize(string data)
        {
            string[] parts = data.Split('|');
            string name = parts[1];
            string description = parts[2];
            int points = int.Parse(parts[3]);
            int targetCount = int.Parse(parts[4]);
            int currentCount = int.Parse(parts[5]);
            int bonusPoints = int.Parse(parts[6]);
            bool completed = bool.Parse(parts[7]);
            ChecklistGoal goal = new ChecklistGoal(name, description, points, targetCount, bonusPoints);
            goal._currentCount = currentCount;
            goal.Completed = completed;
            return goal;
        }
    }

    /*
     * Extra Creativity: I made a leveling system where it levels up every 3 goals and displays the level so you can see what level you are. 
     * This encourages the user to keep going and continue their progress. 
     */
    class Program
    {
        private static int _score = 0;
        private static int _level = 1;
        private static int _completedGoalsCount = 0;
        private static List<Goal> _goals = new List<Goal>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Eternal Quest Menu ---");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Display Score and Level");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Quit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoals();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        Console.WriteLine($"Your current score is: {_score}");
                        Console.WriteLine($"Your current level is: {_level}");
                        break;
                    case "5":
                        SaveGoals();
                        break;
                    case "6":
                        LoadGoals();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        static void CreateGoal()
        {
            Console.WriteLine("\nChoose goal type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choice: ");
            string typeChoice = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();
            Console.Write("Enter points for this goal: ");
            int points = int.Parse(Console.ReadLine());

            switch (typeChoice)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case "3":
                    Console.Write("Enter number of times to complete: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points on completion: ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    break;
            }
        }

        static void ListGoals()
        {
            Console.WriteLine("\n--- Goals List ---");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].Name} ({_goals[i].Description})");
            }
        }

        static void RecordEvent()
        {
            ListGoals();
            Console.Write("Select the goal number to record an event: ");
            string input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int index);
            if (!isValidNumber)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            bool isInRange = (index > 0 && index <= _goals.Count);
            if (!isInRange)
            {
                Console.WriteLine("Invalid selection. Number is out of range.");
                return;
            }

            Goal selectedGoal = _goals[index - 1];
            bool wasCompleted = selectedGoal.Completed;
            int earned = selectedGoal.RecordEvent();
            bool nowCompleted = selectedGoal.Completed;
            _score += earned;
            if (!wasCompleted && nowCompleted)
            {
                _completedGoalsCount++;
                int newLevel = _completedGoalsCount / 3 + 1;
                if (newLevel > _level)
                {
                    _level = newLevel;
                    Console.WriteLine($"Congratulations, you've leveled up to level {_level}!");
                }
            }
            Console.WriteLine($"Event recorded! You earned {earned} points.");
        }

        static void SaveGoals()
        {
            Console.Write("Enter filename: ");
            string filename = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                writer.WriteLine(_level);
                writer.WriteLine(_completedGoalsCount);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.Serialize());
                }
            }
            Console.WriteLine("Goals saved.");
        }

        static void LoadGoals()
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            if (File.Exists(filename))
            {
                string[] lines = File.ReadAllLines(filename);
                _score = int.Parse(lines[0]);
                _level = int.Parse(lines[1]);
                _completedGoalsCount = int.Parse(lines[2]);
                _goals.Clear();
                for (int i = 3; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.StartsWith("SimpleGoal"))
                    {
                        _goals.Add(SimpleGoal.Deserialize(line));
                    }
                    else if (line.StartsWith("EternalGoal"))
                    {
                        _goals.Add(EternalGoal.Deserialize(line));
                    }
                    else if (line.StartsWith("ChecklistGoal"))
                    {
                        _goals.Add(ChecklistGoal.Deserialize(line));
                    }
                }
                Console.WriteLine("Goals loaded.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}
