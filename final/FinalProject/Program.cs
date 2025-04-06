using System;
using System.Collections.Generic;

namespace BookNookProject
{
    public abstract class Person
    {
        private string _name;
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; } 
        }

        public Person(string name)
        {
            _name = name;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("Name: " + _name);
        }
    }

    public class Librarian : Person
    {
        private string _employeeID;
        public string EmployeeID 
        { 
            get { return _employeeID; } 
            set { _employeeID = value; } 
        }

        public Librarian(string name, string employeeID) : base(name)
        {
            _employeeID = employeeID;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("Librarian: " + Name + ", Employee ID: " + _employeeID);
        }

        public void ManageInventory(Book book, string action)
        {
        }

        public void RegisterMember(Member member)
        {
        }

        public void ProcessReturn(Book book, Member member)
        {
        }
    }

    public class Member : Person
    {
        private string _memberID;
        private List<Loan> _loans;
        private List<Reservation> _reservations;

        public string MemberID 
        { 
            get { return _memberID; } 
            set { _memberID = value; } 
        }
        public List<Loan> Loans 
        { 
            get { return _loans; } 
        }
        public List<Reservation> Reservations 
        { 
            get { return _reservations; } 
        }

        public Member(string name, string memberID) : base(name)
        {
            _memberID = memberID;
            _loans = new List<Loan>();
            _reservations = new List<Reservation>();
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("Member: " + Name + ", Member ID: " + _memberID);
        }

        public void PayFine(Fine fine)
        {
        }
    }

    public class Book
    {
        private string _title;
        private string _author;
        private string _isbn;
        private bool _isAvailable;

        public string Title 
        { 
            get { return _title; } 
        }
        public string Author 
        { 
            get { return _author; } 
        }
        public string ISBN 
        { 
            get { return _isbn; } 
        }
        public bool IsAvailable 
        { 
            get { return _isAvailable; } 
        }

        public Book(string title, string author, string isbn)
        {
            _title = title;
            _author = author;
            _isbn = isbn;
            _isAvailable = true;
        }

        public void Checkout()
        {
            _isAvailable = false;
        }

        public void ReturnBook()
        {
            _isAvailable = true;
        }
    }

    public class Loan
    {
        private Book _borrowedBook;
        private Member _borrowingMember;
        private DateTime _dueDate;

        public Book BorrowedBook 
        { 
            get { return _borrowedBook; } 
        }
        public Member BorrowingMember 
        { 
            get { return _borrowingMember; } 
        }
        public DateTime DueDate 
        { 
            get { return _dueDate; } 
        }

        public Loan(Book book, Member member, DateTime dueDate)
        {
            _borrowedBook = book;
            _borrowingMember = member;
            _dueDate = dueDate;
        }

        public bool IsOverdue()
        {
            return DateTime.Now > _dueDate;
        }

        public void ExtendDueDate()
        {
            _dueDate = _dueDate.AddDays(7);
        }
    }

    public class Reservation
    {
        private Book _reservedBook;
        private Member _reservingMember;
        private DateTime _reservationDate;

        public Book ReservedBook 
        { 
            get { return _reservedBook; } 
        }
        public Member ReservingMember 
        { 
            get { return _reservingMember; } 
        }
        public DateTime ReservationDate 
        { 
            get { return _reservationDate; } 
        }

        public Reservation(Book book, Member member)
        {
            _reservedBook = book;
            _reservingMember = member;
            _reservationDate = DateTime.Now;
        }

        public void CancelReservation()
        {
        }
    }

    public class Fine
    {
        private Member _memberWithFine;
        private double _amount;

        public Member MemberWithFine 
        { 
            get { return _memberWithFine; } 
        }
        public double Amount 
        { 
            get { return _amount; } 
        }

        public Fine(Member member, double amount)
        {
            _memberWithFine = member;
            _amount = amount;
        }

        public void CalculateFine(Loan loan)
        {
            if (loan.IsOverdue())
            {
                TimeSpan overdueTime = DateTime.Now - loan.DueDate;
                _amount = overdueTime.Days * 1.0;
            }
            else
            {
                _amount = 0;
            }
        }

        public void PayFine()
        {
            _amount = 0;
        }
    }

    public class Library
    {
        private List<Book> _books;
        private List<Member> _members;
        private List<Loan> _loans;
        private List<Reservation> _reservations;

        public Library()
        {
            _books = new List<Book>();
            _members = new List<Member>();
            _loans = new List<Loan>();
            _reservations = new List<Reservation>();
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _books.Remove(book);
        }

        public void RegisterMember(Member member)
        {
            _members.Add(member);
        }

        public Book FindBook(string isbn)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].ISBN == isbn)
                {
                    return _books[i];
                }
            }
            return null;
        }

        public Member FindMember(string memberID)
        {
            for (int i = 0; i < _members.Count; i++)
            {
                if (_members[i].MemberID == memberID)
                {
                    return _members[i];
                }
            }
            return null;
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public List<Member> GetAllMembers()
        {
            return _members;
        }

        public void CheckoutBook(string isbn, string memberID)
        {
            Book book = FindBook(isbn);
            Member member = FindMember(memberID);

            if (book != null && member != null && book.IsAvailable)
            {
                book.Checkout();
                Loan newLoan = new Loan(book, member, DateTime.Now.AddDays(14));
                _loans.Add(newLoan);
                member.Loans.Add(newLoan);
                Console.WriteLine("Book checked out successfully!");
            }
            else
            {
                Console.WriteLine("Checkout failed. Book might not be available or member not found.");
            }
        }

        public void ReturnBook(string isbn, string memberID)
        {
            Book book = FindBook(isbn);
            Member member = FindMember(memberID);

            if (book != null && member != null && !book.IsAvailable)
            {
                Loan foundLoan = null;
                for (int i = 0; i < _loans.Count; i++)
                {
                    if (_loans[i].BorrowedBook.ISBN == isbn && _loans[i].BorrowingMember.MemberID == memberID)
                    {
                        foundLoan = _loans[i];
                        break;
                    }
                }

                if (foundLoan != null)
                {
                    Fine fine = new Fine(member, 0);
                    fine.CalculateFine(foundLoan);
                    if (fine.Amount > 0)
                    {
                        Console.WriteLine("Book is overdue. Fine: $" + fine.Amount);
                    }
                    book.ReturnBook();
                    _loans.Remove(foundLoan);
                    member.Loans.Remove(foundLoan);
                    Console.WriteLine("Book returned successfully!");
                }
                else
                {
                    Console.WriteLine("Loan not found.");
                }
            }
            else
            {
                Console.WriteLine("Return failed. Check book or member info.");
            }
        }

        public void ReserveBook(string isbn, string memberID)
        {
            Book book = FindBook(isbn);
            Member member = FindMember(memberID);

            if (book != null && member != null)
            {
                Reservation newReservation = new Reservation(book, member);
                _reservations.Add(newReservation);
                member.Reservations.Add(newReservation);
                Console.WriteLine("Book reserved successfully!");
            }
            else
            {
                Console.WriteLine("Reservation failed. Book or member not found.");
            }
        }

        public void ListBooks()
        {
            for (int i = 0; i < _books.Count; i++)
            {
                Book book = _books[i];
                Console.WriteLine("Title: " + book.Title + ", Author: " + book.Author + ", ISBN: " + book.ISBN + ", Available: " + book.IsAvailable);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book("1984", "George Orwell", "1234567890"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "2345678901"));
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "3456789012"));

            library.RegisterMember(new Member("John Doe", "M001"));
            library.RegisterMember(new Member("Jane Roe", "M002"));

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n----- BookNook Library Menu -----");
                Console.WriteLine("1. List all books");
                Console.WriteLine("2. Check out a book");
                Console.WriteLine("3. Return a book");
                Console.WriteLine("4. Reserve a book");
                Console.WriteLine("5. List all members");
                Console.WriteLine("6. Register new member");
                Console.WriteLine("7. Quit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    library.ListBooks();
                }
                else if (choice == "2")
                {
                    Console.Write("Enter book ISBN: ");
                    string isbnCheckout = Console.ReadLine();
                    Console.Write("Enter member ID: ");
                    string memberIDCheckout = Console.ReadLine();
                    library.CheckoutBook(isbnCheckout, memberIDCheckout);
                }
                else if (choice == "3")
                {
                    Console.Write("Enter book ISBN: ");
                    string isbnReturn = Console.ReadLine();
                    Console.Write("Enter member ID: ");
                    string memberIDReturn = Console.ReadLine();
                    library.ReturnBook(isbnReturn, memberIDReturn);
                }
                else if (choice == "4")
                {
                    Console.Write("Enter book ISBN: ");
                    string isbnReserve = Console.ReadLine();
                    Console.Write("Enter member ID: ");
                    string memberIDReserve = Console.ReadLine();
                    library.ReserveBook(isbnReserve, memberIDReserve);
                }
                else if (choice == "5")
                {
                    List<Member> members = library.GetAllMembers();
                    for (int i = 0; i < members.Count; i++)
                    {
                        members[i].DisplayInfo();
                    }
                }
                else if (choice == "6")
                {
                    Console.Write("Enter new member name: ");
                    string newMemberName = Console.ReadLine();
                    Console.Write("Enter new member ID: ");
                    string newMemberID = Console.ReadLine();
                    Member newMember = new Member(newMemberName, newMemberID);
                    library.RegisterMember(newMember);
                    Console.WriteLine("Member registered successfully!");
                }
                else if (choice == "7")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }
            Console.WriteLine("Exiting BookNook. Thanks!");
        }
    }
}
