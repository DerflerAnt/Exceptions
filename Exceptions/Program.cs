using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        /*
        // Використання фіксованого seed для генератора випадкових чисел
        Random random = new Random(1234);

        // Створення файлів від 10.txt до 29.txt
        for (int i = 10; i <= 29; i++)
        {
            string fileName = $"{i}.txt"; // Назва файлу

            // Генерація фіксованих випадкових чисел
            int number1 = random.Next(-100, 100); // Випадкове число в діапазоні
            int number2 = random.Next(-100, 100);

            // Запис чисел у файл
            File.WriteAllLines(fileName, new string[] { number1.ToString(), number2.ToString() });

            Console.WriteLine($"Файл {fileName} створено з числами: {number1}, {number2}");
        }

        Console.WriteLine("Усі файли створені.");
        */
        // Списки для збереження результатів обробки
        List<int> validProducts = new List<int>(); // Список коректних добутків
        List<string> noFiles = new List<string>(); // Список відсутніх файлів
        List<string> badDataFiles = new List<string>(); // Список файлів із некоректними даними
        List<string> overflowFiles = new List<string>();// Список файлів із переповненням при множенні

        try
        {
            // Перебір файлів від 10.txt до 29.txt (20 файлів)
            foreach (int i in Enumerable.Range(10, 20))// Генерує числа від 10 до 29
            {
                string fileName = $"{i}.txt";// Формування назви файлу
                if (!File.Exists(fileName))
                {
                    // Якщо файл не існує, додаємо його до списку noFiles і пропускаємо обробку
                    noFiles.Add(fileName);
                    continue;
                }
                try
                {
                    // Зчитування рядків із файлу
                    string[] lines = File.ReadAllLines(fileName);
                    // Парсинг чисел із файлу
                    int firstNumber = int.Parse(lines[0]);
                    int secondNumber = int.Parse(lines[1]);
                    // Обчислення добутку з перевіркою на переповнення
                    checked
                    {
                        int product = firstNumber * secondNumber;
                        validProducts.Add(product);// Додавання результату до списку
                    }
                }
                catch (FileNotFoundException)
                {
                    // Якщо файл не знайдено, додаємо його до списку noFiles
                    noFiles.Add(fileName);
                }
                catch (FormatException)
                {
                    // Якщо дані у файлі некоректні (не числа), додаємо до badDataFiles
                    badDataFiles.Add(fileName);
                }
                catch (OverflowException)
                {
                    // Якщо відбулося переповнення при множенні, додаємо до overflowFiles
                    overflowFiles.Add(fileName);
                }
                catch (Exception ex)
                {
                    // Обробка будь-яких інших неочікуваних винятків
                    Console.WriteLine($"Unexpected error in {fileName}: {ex.Message}");
                }
            }
            // Запис результатів у відповідні файли
            if (noFiles.Count > 0)
            {
                Console.WriteLine("Файли, яких не існує:");
                noFiles.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Усі файли знайдено.");
            }
            // Файли, які не знайдено
            if (badDataFiles.Count > 0)
            {
                Console.WriteLine("Файли з некоректними даними:");
                badDataFiles.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Некоректних даних не знайдено.");
            } // Файли з некоректними даними
            if (overflowFiles.Count > 0)
            {
                Console.WriteLine("Файли з переповненням при множенні:");
                overflowFiles.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Переповнень не виявлено.");
            }
            // Файли з переповненням
            // Якщо є коректні добутки, обчислюємо їх середнє арифметичне
            if (validProducts.Count > 0)
            {
                // Обчислення середнього
                double average = validProducts.Average();
                Console.WriteLine($"Середнє арифметичне: {average}");
            }
            else
            {
                // Якщо немає коректних добутків
                Console.WriteLine("Жодного коректного добутку не знайдено.");
            }
        }
        catch (IOException ioEx)
        {
            // Обробка помилок при записі результатів у файли
            Console.WriteLine($"Помилка створення/запису файлу: {ioEx.Message}");
        }
    }
}
