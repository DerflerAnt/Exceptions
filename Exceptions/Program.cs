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
            foreach (int i in Enumerable.Range(10, 20)) // Генеруємо числа від 10 до 29
            {
                string fileName = $"{i}.txt"; // Формування назви файлу

                try
                {
                    string[] lines = File.ReadAllLines(fileName); // Спроба читання файлу

                    try
                    {
                        int firstNumber = int.Parse(lines[0]);
                        int secondNumber = int.Parse(lines[1]);

                        try
                        {
                            checked // Перевірка на переповнення
                            {
                                int product = firstNumber * secondNumber;
                                validProducts.Add(product);
                            }
                        }
                        catch (OverflowException)
                        {
                            overflowFiles.Add(fileName);
                        }
                    }
                    catch (FormatException)
                    {
                        badDataFiles.Add(fileName);
                    }
                }
                catch (FileNotFoundException)
                {
                    noFiles.Add(fileName);
                }
            }

            try
            {
                File.WriteAllLines("no_file.txt", noFiles);
                File.WriteAllLines("bad_data.txt", badDataFiles);
                File.WriteAllLines("overflow.txt", overflowFiles);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Помилка запису файлу: {ioEx.Message}");
                throw;
            }

            try
            {
                Console.WriteLine(noFiles.Count > 0 ? "Файли, яких не існує:" : "Усі файли знайдено.");
                noFiles.ForEach(Console.WriteLine);

                Console.WriteLine(badDataFiles.Count > 0 ? "Файли з некоректними даними:" : "Некоректних даних не знайдено.");
                badDataFiles.ForEach(Console.WriteLine);

                Console.WriteLine(overflowFiles.Count > 0 ? "Файли з переповненням при множенні:" : "Переповнень не виявлено.");
                overflowFiles.ForEach(Console.WriteLine);

                if (validProducts.Count > 0)
                {
                    double average = validProducts.Average();
                    Console.WriteLine($"Середнє арифметичне коректних добутків: {average}");
                }
                else
                {
                    Console.WriteLine("Жодного коректного добутку не знайдено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непередбачена помилка: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критична помилка виконання програми: {ex.Message}");
        }
    }
}
