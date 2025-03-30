using System;
using System.Collections.Generic;

namespace BookNookProject
{
    public abstract class Person
    {
        public string Name { get; set; }
        public Person(string name)
        {
            Name = name;
        }
    }

    public class Librarian : Person
    {
        public string EmployeeID { get; set; }

        public Librarian(string name, string employeeID) : base(name)
        {
            EmployeeID = employeeID;
        }

        public void ManageInventory(Book book, string action)
        {
            // Gotta make inventory management happen here
        }

        public void RegisterMember(Member member)
        {
            // Need code to actually register the member
        }

        public void ProcessReturn(Book book, Member member)
        {
            // This needs to process when someone returns a book
        }
    }

    public class Member : Person
    {
        public string MemberID { get; set; }
        public List<Loan> Loans { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Member(string name, string memberID) : base(name)
        {
            MemberID = memberID;
            Loans = new List<Loan>();
            Reservations = new List<Reservation>();
        }

        public void BorrowBook(Book book)
        {
            // Gotta code borrowing a book here
        }

        public void ReturnBook(Book book)
        {
            // Need to figure out how returning books actually works
        }

        public void PayFine(Fine fine)
        {
            // I'll add fine payments later
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true;
        }

        public void Checkout()
        {
            // I need to make it so they can actually check out
            IsAvailable = false;
        }

        public void ReturnBook()
        {
            // Gotta make returning books change availability
            IsAvailable = true;
        }

        public void Reserve()
        {
            // Need to let members reserve books
        }
    }

    public class Loan
    {
        public Book BorrowedBook { get; set; }
        public Member BorrowingMember { get; set; }
        public DateTime DueDate { get; set; }

        public Loan(Book book, Member member, DateTime dueDate)
        {
            BorrowedBook = book;
            BorrowingMember = member;
            DueDate = dueDate;
        }

        public bool IsOverdue()
        {
            return DateTime.Now > DueDate;
        }

        public void ExtendDueDate()
        {
            DueDate = DueDate.AddDays(7);
        }
    }

    public class Reservation
    {
        public Book ReservedBook { get; set; }
        public Member ReservingMember { get; set; }
        public DateTime ReservationDate { get; set; }

        public Reservation(Book book, Member member)
        {
            ReservedBook = book;
            ReservingMember = member;
            ReservationDate = DateTime.Now;
        }

        public void CancelReservation()
        {
            // Need to add cancellation functionality
        }
    }

    public class Fine
    {
        public Member MemberWithFine { get; set; }
        public double Amount { get; set; }

        public Fine(Member member, double amount)
        {
            MemberWithFine = member;
            Amount = amount;
        }

        public void CalculateFine(Loan loan)
        {
            // Gotta figure out how fines are calculated
        }

        public void PayFine(Member member)
        {
            Amount = 0;
        }
    }

    public class Library
    {
        public List<Book> Books { get; set; }
        public List<Member> Members { get; set; }

        public Library()
        {
            Books = new List<Book>();
            Members = new List<Member>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public Member FindMember(string memberId)
        {
            return Members.Find(m => m.MemberID == memberId);
        }

        public Book FindBook(string isbn)
        {
            return Books.Find(b => b.ISBN == isbn);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            Book book1 = new Book("1984", "George Orwell", "1234567890");
            library.AddBook(book1);

            Member member1 = new Member("John Doe", "M001");
            library.Members.Add(member1);

            Librarian librarian = new Librarian("Jane Smith", "E001");

            member1.BorrowBook(book1);
            Loan loan1 = new Loan(book1, member1, DateTime.Now.AddDays(14));
            member1.Loans.Add(loan1);

            Console.WriteLine("Library setup complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
