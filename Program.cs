using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            // Create an instance of the StarbucksApp
            StarbucksApp app = new StarbucksApp();

            // Run the Starbucks app
            app.Run();

            Console.WriteLine("Do you want to continue? (Y/N)");
            string answer = Console.ReadLine().Trim().ToUpper();
            if (answer != "Y")
            {
                isRunning = false;
            }
        }
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
}

class Menu
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
}

class StarbucksApp
{
    private List<User> users;
    private List<Menu> menuList;

    public StarbucksApp()
    {
        // Initialize users and menuList
        InitializeUsers();
        InitializeMenu();
    }

    private void InitializeUsers()
    {
        // Initialize users
        users = new List<User>
        {
            new User { Username = "admin", Password = "1234" }
        };
    }

    private void InitializeMenu()
    {
        // Initialize menuList
        menuList = new List<Menu>
        {
            new Menu { Name = "Cappuccino", Price = 10, Stock = 7 },
            new Menu { Name = "Macchiato", Price = 11, Stock = 5 },
            new Menu { Name = "Latte Macchiato", Price = 15, Stock = 4 }
        };
    }

    public void Run()
    {
        Console.WriteLine("Input username and password");
        Console.Write("Username: ");
        string inputUser = Console.ReadLine();
        Console.Write("Password: ");
        string inputPass = Console.ReadLine();

        User currentUser = users.FirstOrDefault(u => u.Username == inputUser && u.Password == inputPass);

        if (currentUser == null)
        {
            Console.WriteLine("Incorrect username or password");
            return;
        }

        Console.WriteLine("Welcome");
        Console.WriteLine("Selamat datang di Starbucks");
        Console.WriteLine("===========================");

        DisplayMenu();

        Console.WriteLine("What are you gonna do?");
        Console.WriteLine("1. Search menu with name-based");
        Console.WriteLine("2. Search menu by min-max price");
        Console.WriteLine("3. Sort menu based on its availability");
        Console.WriteLine("4. View menu (adding menu or even delete it)");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                SearchMenuByName();
                break;
            case 2:
                SearchMenuByPrice();
                break;
            case 3:
                SortMenuByAvailability();
                break;
            case 4:
                ViewAndUpdateMenu();
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Menu " + "\t\t" + "Price " + "\t" + "Stock ");
        foreach (var item in menuList)
        {
            Console.WriteLine(item.Name + "\t" + item.Price + "\t" + item.Stock);
        }
    }

    private void SearchMenuByName()
    {
        Console.WriteLine("What menu you would like to search?");
        string userSearch = Console.ReadLine();
        var searchResults = menuList.Where(x => x.Name.Contains(userSearch));
        Console.WriteLine("\nSearch results:");
        foreach (var item in searchResults)
        {
            Console.WriteLine(item.Name + "\t" + item.Price + "\t" + item.Stock);
        }
    }

    private void SearchMenuByPrice()
    {
        Console.WriteLine("What price you would like to search?");
        Console.WriteLine("1. Max");
        Console.WriteLine("2. Min");
        int userChoice = int.Parse(Console.ReadLine());

        if (userChoice == 1)
        {
            int maxPrice = menuList.Max(x => x.Price);
            var searchResults = menuList.Where(x => x.Price == maxPrice);
            Console.WriteLine("\nSearch results:");
            foreach (var item in searchResults)
            {
                Console.WriteLine(item.Name + "\t" + item.Price + "\t" + item.Stock);
            }
        }
        else if (userChoice == 2)
        {
            int minPrice = menuList.Min(x => x.Price);
            var searchResults = menuList.Where(x => x.Price == minPrice);
            Console.WriteLine("\nSearch results:");
            foreach (var item in searchResults)
            {
                Console.WriteLine(item.Name + "\t" + item.Price + "\t" + item.Stock);
            }
        }
    }

    private void SortMenuByAvailability()
    {
        var sortedMenu = menuList.OrderByDescending(x => x.Stock);
        Console.WriteLine("\nSorted menus:");
        foreach (var item in sortedMenu)
        {
            Console.WriteLine(item.Name + "\t" + item.Price + "\t" + item.Stock);
        }
    }

    private void ViewAndUpdateMenu()
    {
        DisplayMenu();
        Console.WriteLine("What you're gonna do?");
        Console.WriteLine("1. Add menu");
        Console.WriteLine("2. Delete menu");
        Console.WriteLine("3. Let it go");
        int userChoice = int.Parse(Console.ReadLine());

        switch (userChoice)
        {
            case 1:
                AddMenu();
                break;
            case 2:
                DeleteMenu();
                break;
            case 3:
                Console.WriteLine("Ok");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    private void AddMenu()
    {
        Console.Write("Menu: ");
        string itemName = Console.ReadLine();
        Console.Write("Price: ");
        int itemPrice = int.Parse(Console.ReadLine());
        Console.Write("Stock: ");
        int itemStock = int.Parse(Console.ReadLine());

        menuList.Add(new Menu { Name = itemName, Price = itemPrice, Stock = itemStock });
        Console.WriteLine("Menu item added successfully.");
    }

    private void DeleteMenu()
    {
        Console.WriteLine("What menu you would like to delete?");
        string itemToDelete = Console.ReadLine();
        Menu menuToDelete = menuList.FirstOrDefault(x => x.Name == itemToDelete);

        if (menuToDelete != null)
        {
            menuList.Remove(menuToDelete);
            Console.WriteLine("Menu item deleted successfully.");
        }
        else
        {
            Console.WriteLine("Menu item not found.");
        }
    }
}
