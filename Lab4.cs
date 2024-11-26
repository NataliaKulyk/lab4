using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PCComponentsApp
{
    // Клас для представлення комплектуючих ПК
    public class Component
    {
        // Поля для зберігання даних про комплектуюче
        private string name;
        private string serialNumber;
        private string manufacturer;
        private string country;
        private decimal price;

        // Конструктор за замовчуванням
        public Component() { }

        // Параметризований конструктор з обробкою виключень
        public Component(string name, string serialNumber, string manufacturer, string country, decimal price)
        {
            Name = name; // Виклик властивості для ініціалізації назви
            SerialNumber = serialNumber; // Виклик властивості для ініціалізації серійного номера
            Manufacturer = manufacturer; // Виклик властивості для ініціалізації виробника
            Country = country; // Виклик властивості для ініціалізації країни
            Price = price; // Виклик властивості для ініціалізації ціни
        }

        // Властивість для назви комплектуючого з обробкою виключень
        public string Name
        {
            get => name; // Повертає значення поля name
            set
            {
                // Перевірка, щоб назва не була пустою або нульовою
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty."); // Викидає виключення при некоректному значенні
                name = value; // Присвоює значення поля name
            }
        }

        // Властивість для серійного номера з обробкою виключень
        public string SerialNumber
        {
            get => serialNumber; // Повертає значення поля serialNumber
            set
            {
                // Перевірка, щоб серійний номер не був пустим або нульовим
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Serial number cannot be null or empty."); // Викидає виключення при некоректному значенні
                serialNumber = value; // Присвоює значення поля serialNumber
            }
        }

        // Властивість для виробника з обробкою виключень
        public string Manufacturer
        {
            get => manufacturer; // Повертає значення поля manufacturer
            set
            {
                // Перевірка, щоб виробник не був пустим або нульовим
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Manufacturer cannot be null or empty."); // Викидає виключення при некоректному значенні
                manufacturer = value; // Присвоює значення поля manufacturer
            }
        }

        // Властивість для країни з обробкою виключень
        public string Country
        {
            get => country; // Повертає значення поля country
            set
            {
                // Перевірка, щоб країна не була пустою або нульовою
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Country cannot be null or empty."); // Викидає виключення при некоректному значенні
                country = value; // Присвоює значення поля country
            }
        }

        // Властивість для ціни з обробкою виключень
        public decimal Price
        {
            get => price; // Повертає значення поля price
            set
            {
                // Перевірка, щоб ціна не була від'ємною
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative."); // Викидає виключення при некоректному значенні
                price = value; // Присвоює значення поля price
            }
        }

        // Метод для відображення інформації про комплектуюче у форматованому вигляді
        public override string ToString()
        {
            return $"Name: {Name}, Serial Number: {SerialNumber}, Manufacturer: {Manufacturer}, Country: {Country}, Price: {Price:C}";
        }
    }

    // Клас-обгортка для узагальненої колекції комплектуючих
    public class ComponentCollection
    {
        // Поле для зберігання списку комплектуючих
        private List<Component> components;

        // Конструктор
        public ComponentCollection()
        {
            components = new List<Component>(); // Ініціалізація порожнього списку комплектуючих
        }

        // Метод для додавання комплектуючого до колекції
        public void AddComponent(Component component)
        {
            // Перевірка на унікальність серійного номера
            if (components.Any(c => c.SerialNumber == component.SerialNumber))
            {
                throw new InvalidOperationException("Component with this serial number already exists."); // Викидає виключення, якщо серійний номер вже існує
            }
            components.Add(component); // Додає комплектуюче до списку
        }

        // Метод для відображення всіх комплектуючих
        public void DisplayComponents()
        {
            Console.WriteLine("Components List:"); // Заголовок для списку
            foreach (var component in components)
            {
                Console.WriteLine(component); // Виводить інформацію про кожне комплектуюче
            }
        }

        // Метод для сортування комплектуючих за серійним номером
        public void SortBySerialNumber()
        {
            components = components.OrderBy(c => c.SerialNumber).ToList(); // Сортує колекцію за серійним номером
        }

        // Метод для вибірки комплектуючих за назвою
        public List<Component> FilterByName(string name)
        {
            // Фільтрує комплектуючі, що мають задану назву, і повертає їх список
            return components.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Метод для вибірки комплектуючих за країною-виробником
        public List<Component> FilterByCountry(string country)
        {
            // Фільтрує комплектуючі, що мають задану країну, і повертає їх список
            return components.Where(c => c.Country.Equals(country, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Метод для запису колекції у файл
        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath)) // Відкриває файл для запису
            {
                foreach (var component in components)
                {
                    // Записує інформацію про комплектуючі у файл, розділяючи поля комами
                    writer.WriteLine($"{component.Name},{component.SerialNumber},{component.Manufacturer},{component.Country},{component.Price}");
                }
            }
        }

        // Метод для зчитування колекції з файлу
        public void LoadFromFile(string filePath)
        {
            components.Clear(); // Очищає колекцію перед завантаженням
            using (StreamReader reader = new StreamReader(filePath)) // Відкриває файл для читання
            {
                string line;
                // Читає файл рядок за рядком
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(','); // Розділяє рядок на частини за комою
                    if (parts.Length == 5) // Перевірка, чи є всі частини
                    {
                        try
                        {
                            // Створює новий об'єкт Component та додає його до колекції
                            AddComponent(new Component(parts[0], parts[1], parts[2], parts[3], decimal.Parse(parts[4])));
                        }
                        catch (Exception ex)
                        {
                            // Виводить повідомлення про помилку при завантаженні
                            Console.WriteLine($"Error loading component: {ex.Message}");
                        }
                    }
                }
            }
        }
    }

    // Головний клас програми
    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкту колекції компонентів
            ComponentCollection componentCollection = new ComponentCollection();

            // Додавання комплектуючих до колекції з обробкою виключень
            try
            {
                componentCollection.AddComponent(new Component("Graphics Card", "SN123", "NVIDIA", "USA", 499.99m));
                componentCollection.AddComponent(new Component("Motherboard", "SN456", "ASUS", "Taiwan", 199.99m));
                componentCollection.AddComponent(new Component("CPU", "SN789", "Intel", "USA", 299.99m));
            }
            catch (Exception ex)
            {
                // Виводить повідомлення про помилку при додаванні компонентів
                Console.WriteLine($"Error adding component: {ex.Message}");
            }

            // Відображення всіх комплектуючих
            componentCollection.DisplayComponents();

            // Сортування комплектуючих за серійним номером
            Console.WriteLine("\nSorting components by Serial Number...");
            componentCollection.SortBySerialNumber();
            componentCollection.DisplayComponents(); // Виводимо відсортований список

            // Вибірка комплектуючих за назвою
            string searchName = "CPU";
            var filteredByName = componentCollection.FilterByName(searchName);
            Console.WriteLine($"\nFiltering components by name: {searchName}");
            foreach (var component in filteredByName)
            {
                Console.WriteLine(component); // Виводить результати фільтрації
            }

            // Вибірка комплектуючих за країною-виробником
            string searchCountry = "USA";
            var filteredByCountry = componentCollection.FilterByCountry(searchCountry);
            Console.WriteLine($"\nFiltering components by country: {searchCountry}");
            foreach (var component in filteredByCountry)
            {
                Console.WriteLine(component); // Виводить результати фільтрації
            }

            // Запис колекції у файл
            string filePath = "components.txt";
            componentCollection.SaveToFile(filePath);
            Console.WriteLine($"\nComponent collection saved to {filePath}.");

            // Завантаження колекції з файлу
            Console.WriteLine("\nLoading components from file...");
            componentCollection.LoadFromFile(filePath);
            componentCollection.DisplayComponents(); // Виводимо завантажений список

            // Додавання комплектуючих з унікальним серійним номером
            try
            {
                componentCollection.AddComponent(new Component("RAM", "SN123", "Corsair", "USA", 89.99m)); // Цей компонент не буде додано
            }
            catch (Exception ex)
            {
                // Виводить повідомлення про помилку при додаванні компонентів
                Console.WriteLine($"Error adding component: {ex.Message}");
            }

            // Завершення програми
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}



