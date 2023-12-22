/*
 Разработайте алгоритм преобразования строки, которая содержит строковое представление числа, 
 в переменную типа double. 

Запрещается использовать уже имеющиеся в .NET алгоритмы преобразования, такие как:

    Convert.ToDouble().
    Double.Parse().
    Double.TryParse().

  Оформите алгоритм в виде метода, не содержащего побочных эффектов.

 */
string max = Double.MinValue.ToString("G17");

string[] values = { "1AFF","  -112,035.77219", "1e-35", "1-2", "100",
                         "1,635,592,999,999,999,999,999,999", "-17.455",
                         "190.34001", "1.29e325", max};

double result;

foreach (string? value in values)
{
    try
    {
        result = MyConvert.ToDouble(value);
      
        Console.WriteLine("Преобразование из '{0}' в {1:R}.", value, result);
    }
    catch (FormatException fe)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Невозможно преобразовать'{0}' в Double. {1:R}", value, fe.Message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    catch (OverflowException oe)
    {
        Console.WriteLine("'{0}' находится за пределами диапазона Double. {1:R}", value, oe.Message);
    }
    catch (ArgumentNullException)
    {
        Console.WriteLine("Невозможно преобразовать null в Double", value);
    }
}


