using System.Collections;
using System.Collections.Generic;
using System;

public class Program
{
    public static void Main(string[] args)
    {
    Vector v1 = new Vector("Червоний", 3, 4);
    Vector v2 = new Vector("Синій", 1, 1);
    Vector v3 = new Vector("Зелений", 5, 12);
    Vector v4 = new Vector("Жовтий", 8, 6);

        Console.WriteLine("--- Завдання 2: Дослідження Колекцій ---");

        Console.WriteLine("\n=== 1. Масив (Array) ===");
        
        // Для завдання 2: використовуємо 3 об'єкти (v1, v2, v3) у масиві
        Vector?[] vectorArray = new Vector?[3];
        vectorArray[0] = v1;
        vectorArray[1] = v2;
        vectorArray[2] = v3;

        Console.WriteLine("Оновлення: v2 (індекс 1) замінено на v4.");
        vectorArray[1] = v4; 

        Console.WriteLine("Ітерація по масиву:");
        foreach (Vector? v in vectorArray)
        {
            if (v is null) continue;
            Console.WriteLine(v);
        }

        int index = Array.IndexOf(vectorArray, v3);
        Console.WriteLine($"Пошук: Елемент v3 знаходиться за індексом: {index}");

        vectorArray[index] = null;
        Console.WriteLine("Видалення: Елементу v3 (індекс 2) присвоєно null.");
        
        Console.WriteLine("\n=== 2. Звичайна колекція (ArrayList) ===");
        
    ArrayList arrayList = new ArrayList();
        
        arrayList.Add(v1);
        arrayList.Add(v2);
        arrayList.Add(v3);
        Console.WriteLine($"Додано 3 елементи. Розмір: {arrayList.Count}");

        arrayList[1] = v4;
        Console.WriteLine("Оновлення: v2 (індекс 1) замінено на v4.");

        Console.WriteLine("Ітерація по ArrayList:");
        foreach (object obj in arrayList)
        {
            if (obj is Vector v)
            {
                Console.WriteLine(v);
            }
        }

        int listIndex = arrayList.IndexOf(v3);
        Console.WriteLine($"Пошук: Елемент v3 знаходиться за індексом: {listIndex}");

        arrayList.Remove(v3);
        Console.WriteLine($"Видалення: v3 видалено. Новий розмір: {arrayList.Count}");

        Console.WriteLine("\n=== 3. Узагальнена колекція (List<Vector>) ===");
        
    List<Vector> vectorList = new List<Vector>();
        
        vectorList.Add(v1);
        vectorList.Add(v2);
        vectorList.Add(v3);
        Console.WriteLine($"Додано 3 елементи. Розмір: {vectorList.Count}");

        vectorList[1] = v4;
        Console.WriteLine("Оновлення: v2 (індекс 1) замінено на v4.");

        Console.WriteLine("Ітерація по List<Vector>:");
        foreach (Vector v in vectorList)
        {
            Console.WriteLine(v);
        }

        int genericIndex = vectorList.IndexOf(v3);
        Console.WriteLine($"Пошук: Елемент v3 знаходиться за індексом: {genericIndex}");

        vectorList.Remove(v3);
        Console.WriteLine($"Видалення: v3 видалено. Новий розмір: {vectorList.Count}");

        // Демонстрація сортування з використанням IComparer / Comparison
        Console.WriteLine("\nСортування списку за довжиною (взростання):");
        vectorList.Sort((a, b) => a.CompareTo(b));
        foreach (var v in vectorList)
            Console.WriteLine(v);

        Console.WriteLine("\nСортування списку за кольором (лексично):");
        vectorList.Sort((a, b) => string.Compare(a.Color, b.Color, StringComparison.Ordinal));
        foreach (var v in vectorList)
            Console.WriteLine(v);

        Console.WriteLine("\n--- Порівняння Колекцій ---");
        Console.WriteLine(
            "1. Масив (Array): \n" +
            "   - Плюси: Найшвидший доступ за індексом. \n" +
            "   - Мінуси: Фіксований розмір. Складні операції додавання/видалення (потребують створення нового масиву). \n"
        );
        Console.WriteLine(
            "2. ArrayList (Non-Generic): \n" +
            "   - Плюси: Динамічний розмір. \n" +
            "   - Мінуси: Небезпечний для типів. Зберігає 'object', що вимагає 'упаковки' (boxing) для типів-значень та 'розпакування' (unboxing/casting) для доступу до методів. Це повільно і може призвести до помилок під час виконання. (Зараз вважається застарілим). \n"
        );
        Console.WriteLine(
            "3. List<T> (Generic): \n" +
            "   - Плюси: Найкращий вибір. Динамічний розмір. Типобезпечний (ви знаєте, що там лежать лише Vector). Немає потреби в кастингу. Висока продуктивність (немає boxing/unboxing). \n" +
            "   - Мінуси: Трохи повільніший за масив при доступі за індексом, але набагато гнучкіший."
        );

        Console.WriteLine("\n\n=== Завдання 3 і 4: Бінарне Дерево ===");

        Vector v5 = new Vector("Фіолетовий", 2, 2);
        
        BinaryTree<Vector> vectorTree = new BinaryTree<Vector>();

        Console.WriteLine("Додавання елементів у дерево (v1, v2, v3, v4, v5)...");
        vectorTree.Add(v1);
        vectorTree.Add(v2);
        vectorTree.Add(v3);
        vectorTree.Add(v4);
        vectorTree.Add(v5);

        Console.WriteLine("\nДемонстрація ітератора (Прямий обхід - Preorder):");
        Console.WriteLine("(Порядок: Корінь -> Ліво -> Право)");
        
        foreach (Vector v in vectorTree)
        {
            Console.WriteLine(v);
        }

        // Пример с пользовательским компаратором (за кольором)
        Console.WriteLine("\n--- Дерево з користувацьким компаратором (за кольором) ---");
        var colorComparer = new Comparison<Vector>((a, b) => string.Compare(a.Color, b.Color, StringComparison.Ordinal));
        var comparerObj = Comparer<Vector>.Create(colorComparer);

        BinaryTree<Vector> colorTree = new BinaryTree<Vector>(comparerObj);
        colorTree.Add(v1);
        colorTree.Add(v2);
        colorTree.Add(v3);
        colorTree.Add(v4);
        colorTree.Add(v5);

        Console.WriteLine("Preorder (по кольору):");
        foreach (Vector v in colorTree)
        {
            Console.WriteLine(v);
        }

        Console.WriteLine($"\nПеревірка наявності v3 у дереві за довжиною: {vectorTree.Contains(v3)}");
        Console.WriteLine($"Перевірка наявності v3 у дереві за кольором: {colorTree.Contains(v3)}");
    }
}