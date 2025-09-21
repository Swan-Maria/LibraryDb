using LibraryApp.Data;
using Microsoft.EntityFrameworkCore;

using var context = new LibraryDbContext();

// 0. Сброс базы данных (понадобилось в следствии частого перезапуска программы)
context.Books.RemoveRange(context.Books.ToList());
context.SaveChanges();
context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"Books_Id_seq\" RESTART WITH 1");

// 1. Добавить одну книгу
var book1 = new Book { Title = "Преступление и наказание", Author = "Ф.M. Достоевский", YearPublished = 1866 };
context.Books.Add(book1);
context.SaveChanges();

// 2. Добавить несколько книг
var books = new[]
{
    new Book { Title = "Война и мир", Author = "Толстой", YearPublished = 1869 },
    new Book { Title = "Братья Карамазовы", Author = "Ф.M. Достоевский", YearPublished = 1880},
    new Book { Title = "Анна Каренина", Author = "Толстой", YearPublished = 1875 },
    new Book { Title = "Убить пересмешника", Author = "Харпер Ли", YearPublished = 1960},
    new Book { Title = "Ночь в Лиссабоне", Author = "Э.М.Ремарк", YearPublished = 1962},
    new Book { Title = "Маленький принц", Author = "A. де Сент-Экзюпери", YearPublished = 1943},
    new Book { Title = "Три товарища", Author = "Э.М.Ремарк", YearPublished = 1932},
    new Book { Title = "Сто лет одиночества", Author = "Г.Г. Маркес", YearPublished = 1967},
    new Book { Title = "Тени в раю", Author = "Э.М.Ремарк", YearPublished = 1971},
    new Book { Title = "Триумфальная арка", Author = "Э.М.Ремарк", YearPublished = 1946}
};
context.Books.AddRange(books);
context.SaveChanges();

// 3. Обновить одну книгу
var anna = context.Books.First(b => b.Title == "Анна Каренина");
anna.YearPublished = 1878;
context.SaveChanges();

// 4. Обновить несколько книг
var tolstoyBooks = context.Books.Where(b => b.Author.Contains("Толстой")).ToList();
tolstoyBooks.ForEach(b => b.Author = "Л.Н. Толстой");
context.SaveChanges();

// 5. Удалить одну книгу
var rmBook = context.Books.First(b => b.Title == "Сто лет одиночества");
context.Books.Remove(rmBook);
context.SaveChanges();

// 6. Удалить несколько книг
var rmBooks = context.Books.Where(b => b.YearPublished >= 1965).ToList();
context.Books.RemoveRange(rmBooks);
context.SaveChanges();

// 7. Получить количество всех книг
Console.WriteLine($"Всего книг в базе: {context.Books.Count()}");

// 8. Получить одну книгу по условию
var warPeace = context.Books.FirstOrDefault(b => b.Title == "Война и мир");
Console.WriteLine(warPeace != null ? $"Найдена книга: {warPeace.Title}" : "\'Война и мир\' не найдена");

// 9. Получить список книг по условию
var sovietBooks = context.Books.Where(b => b.YearPublished>=1800 && b.YearPublished < 1900).ToList();
Console.WriteLine($"Книг 19-го века найдено: {sovietBooks.Count}");
