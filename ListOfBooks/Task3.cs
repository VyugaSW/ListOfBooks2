using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfBooks
{

    internal class Book
    {
        string _name;
        string _author;
        string _description;
        public string Name
        {
            get { return _name; }
            set
            {
                try
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    _name = value;
                }
                catch (FormatException) { throw new FormatException(); }
                catch (ArgumentNullException) { throw new ArgumentNullException(); }

            }
        }
        public string Author 
        {
            get { return _author; }
            set 
            {
                try
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    _author = value;
                }
                catch (FormatException) { throw new FormatException(); }
                catch (ArgumentNullException) { throw new ArgumentNullException(); }
            }
        }
        public string Description 
        {
            get { return _description; } 
            set 
            {
                try
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    _description = value;
                }
                catch (FormatException) { throw new FormatException(); }
                catch (ArgumentNullException) { throw new ArgumentNullException(); }
            }
        }

        public Book(string name, string author, string description)
        {
            try
            {
                Name = name;
                Author = author;
                Description = description;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public override string ToString()
        {
            return $"Name - {Name}\nAuthor - {_author}\nDescription - {Description}";
        }
    }

    internal class ListOfBooks
    {
        Book[] bookList = new Book[0];
        public int Lenght { get; private set; } = 0;
        static public ListOfBooks operator +(ListOfBooks list, Book book)
        {
            list.Add(book);
            return list;
        }
        static public ListOfBooks operator +(Book book, ListOfBooks list)
        {
            return list + book;
        }

        static public ListOfBooks operator -(ListOfBooks list, Book book)
        {
            list.Remove(book);
            return list;
        }
        static public ListOfBooks operator -(Book book, ListOfBooks list)
        {
            return list - book;
        }

        public Book this[int index]
        {
            get => bookList.ElementAt(index);
            set 
            {
                try
                {
                    if (index < 0 || index >= bookList.Length)
                        throw new FormatException();
                    bookList[index] = value;
                }
                catch (FormatException)
                {
                    throw new FormatException();
                }
            }
        }

        public void Add(Book book)
        {
            try
            {
                Array.Resize(ref bookList, bookList.Length + 1);
                bookList[Lenght] = book;
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            Lenght = bookList.Length;
        }

        public void RemoveAt(int index)
        {
            try
            {
                if(index < 0 || index >= bookList.Length)
                    throw new FormatException();
                Book[] temp = new Book[bookList.Length - 1];
                Array.Copy(bookList, 0, temp, 0, index);
                Array.Copy(bookList, index + 1, temp, index, bookList.Length - index - 1);
                bookList = temp;
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            Lenght = bookList.Length;
        }

        public void Remove(Book book)
        {
            try
            {
                bookList[bookList.Length-1] = null;
                Array.Resize(ref bookList, bookList.Length - 1);
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            Lenght = bookList.Length;
        }

        public int IndexOf(Book book)
        {
            int i = 0;
            try
            {
                for(int j = 0; j < bookList.Length; j++)
                {
                    if (bookList[j] == book)
                    {
                        i = j;
                        break;
                    }
                }
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            return i;
        }

        public bool Contains(Book book)
        {         
            return bookList.Contains(book) ? true : false;
        }

        public Book FindName(string name)
        {
            foreach(Book b in bookList)
            {
                if (b.Name == name)
                    return b;
            }
            return null;
        }

        public Book FindAuthor(string author)
        {
            foreach (Book b in bookList)
            {
                if (b.Author == author)
                    return b;
            }
            return null;
        }

    }


    internal class MainListOfBooks
    {
        ListOfBooks bookList = new ListOfBooks();
        
        static string Menu = "1| - Add new book\n" +
                             "2| - Remove book\n" +
                             "3| - Show all books\n" +
                             "4| - Find book\n" +
                             "0| - Exit\n";

        static string FindMenu = "1| - Find by name\n" +
                                 "2| - Find by author\n"+
                                 "0| - Exit\n";

        public void Main()
        {
            char k = '1';
            while(k != '0')
            {
                Console.Clear();
                Console.WriteLine(Menu);
                k = Console.ReadKey().KeyChar;
                try
                {
                    Console.Clear();
                    switch (k)
                    {
                        case '1':
                            Add();
                            Console.WriteLine("\nBook have added!");
                            break;
                        case '2':
                            RemoveAt();
                            Console.WriteLine("\nBook have removed!");
                            break;
                        case '3':
                            Console.WriteLine(Show());
                            Console.WriteLine("Enter any key to continue...");
                            break;
                        case '4':
                            Console.WriteLine(Find());
                            Console.WriteLine("\nEnter any key to continue...");
                            break;
                        case '0':
                            k = '0';
                            break;
                        default:
                            Console.WriteLine("Not key\nEnter any key to continue...");
                            break;
                    }
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }

        private Book NewBook()
        {
            Book book;
            try
            {
                Console.WriteLine("Enter a book name - ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter an author of book - ");
                string author = Console.ReadLine();

                Console.WriteLine("Enter a description - ");
                string description = Console.ReadLine();

                book = new Book(name, author, description);
            }
            catch(FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            return book;
        }

        private void Add() 
        {
            try
            {
                bookList.Add(NewBook());
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        private void RemoveAt()
        {
            try
            {
                Console.WriteLine("Enter number of book to remove - ");
                int i = int.Parse(Console.ReadLine());
                bookList.RemoveAt(i-1);
            }
            catch(FormatException ex)
            {
                throw ex;
            }
        }

        private string Show()
        {
            if (bookList.Lenght == 0)
                return "There aren't any books";
            string booksStr = "";
            for (int i = 0; i < bookList.Lenght; i++)
            {
                booksStr += $"Number book in List - {i+1}\n";
                booksStr += bookList[i];
                booksStr += '\n';
            }
            return booksStr;
        }

        private string Find()
        {
            Book temp = new Book("1","1","1");
            char k = ' ';
            Console.WriteLine(FindMenu);
            try
            {
                k = Console.ReadKey().KeyChar;
            }
            catch(FormatException ex)
            {
                throw ex;
            }

            Console.Clear();
            switch (k)
            {
                case '1':
                    temp = FindByName();
                    break;
                case '2':
                    temp = FindByAuthor();
                    break;
                default:
                    Console.WriteLine("Not key");
                    break;
            }

            if (temp == null)
                return "Book with this name wasn't found";
            return (bookList.IndexOf(temp)+1) + "\n" + temp.ToString();

        }

        private Book FindByName()
        {
            string bookName;
            Console.WriteLine("Enter a book name");
            try
            {
                bookName = Console.ReadLine();
            }
            catch(FormatException ex)
            {
                throw ex;
            }
            return bookList.FindName(bookName);
        }

        private Book FindByAuthor()
        {
            string bookAuthor;
            Console.WriteLine("Enter a book name");
            try
            {
                bookAuthor = Console.ReadLine();
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            return bookList.FindAuthor(bookAuthor);
        }
    }
}
