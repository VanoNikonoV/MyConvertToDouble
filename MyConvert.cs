
public static class MyConvert
{
    /// <summary>
    /// Преобразует заданное строковое представление числа в эквивалентное число с плавающей запятой двойной точности
    /// </summary>
    /// <param name="value">Строка, содержащая преобразуемое число</param>
    /// <returns>Число с плавающей запятой двойной точности, эквивалентное числу value, или 0.0(нуль), если value имеет значение null</returns>
    public static double ToDouble(string? value)
    {
        //если входное значение null
        if (value == null) { throw new ArgumentNullException("value");}
    
        bool isNegative;

        var (numerator, denominator) = PreparationValue(value, out isNegative);

        double integer = CharsToDouble(numerator);

        double fraction = CharsToDouble(denominator, true);

        double number = integer + fraction;

        if (isNegative)
            number *= -1;

        return number;
    }

    /// <summary>
    /// Метод подготовки строки к преобразованию, разделяет строку на две части
    /// разделитель определяется как первая точка или запитая, 
    /// последующие символы разделителей игнорируются
    /// </summary>
    /// <param name="input">Строковое представление числа</param>
    /// <param name="isNegative"> Значение true, если число отрицательное; в противном случае — значение false</param>
    /// <returns>Целую и(или) дробную части числа</returns>
    private static (char[] numerator, char[] denominator) PreparationValue(string input, out bool isNegative)
    {
        List <char> Numerator = new();
        List<char> Denominator = new();

        bool flag = false; // начало разделителся дроби

        isNegative = false;

        input = input.Trim();

        for (int i = 0; i < input.Length; i++)
        {
            if (!isNegative && input[0] == '-') //если знак минус в входной строке
            {
                isNegative = true;
            }

            if (!flag && (input[i] == '.' || input[i] == ','))
            {
                flag = true;
                continue;
            }

            if (char.IsDigit(input[i]))
            {
                if (flag) Denominator.Add(input[i]);
                else        Numerator.Add(input[i]);
            }
            else 
            {
                if (input[i] == '.' || input[i] == ',') { continue; }
                
                else throw new FormatException();
            }
                
        }
        return (Numerator.ToArray(), Denominator.ToArray());
    }

    /// <summary>
    /// Перевод массива символов с цифрами в эквивалентное число
    /// </summary>
    /// <param name="str">Строка содержащая только числа</param>
    /// <returns>Число</returns>
    /// <exception cref="FormatException"></exception>
    private static double CharsToDouble(char[] numbers, bool revers = false)
    {
        double result = default;

        char[] reversed = numbers.Reverse().ToArray();

        for (int i = 0; i < numbers.Length; i++)
        {
            double s = Math.Pow(10, i);

            double number;

            if (CharInDouble(reversed[i], out number))
            {
                double temp = number * s;

                result += temp;

                /* О неточночности вычислений
                Выполнение дополнительных математических операций с исходным значением 
                с плавающей запятой часто приводит к увеличению его точности. 
                Например, если сравнить результат умножения 0,1 на 10 и сложения 0,0,1 к 0,1 девять раз, 
                мы увидим, что сложение, поскольку оно включало еще восемь операций, привело к менее точному результату.
                https://learn.microsoft.com/ru-ru/dotnet/api/system.double?view=net-8.0
                */
            }

            else throw new FormatException();
        }
        
        if (double.MaxValue <= result && result <= double.MinValue) { throw new OverflowException(); } //выход за диапазон значений

        return result = !revers ? result  : result / Math.Pow(10, numbers.Length);  
    }

    /// <summary>
    /// Конвертация строкового символа цифры в double
    /// </summary>
    /// <param name="input">Cтроковое представление символа цифры</param>
    /// <param name="result">Цифра в double</param>
    /// <returns>Значение true, если параметр input успешно преобразован; в противном случае — значение false.</returns>
    private static bool CharInDouble(char? input, out double result)
    {
        if (input == '0') { result = 0; return true; }
        if (input == '1') { result = 1; return true; }
        if (input == '2') { result = 2; return true; }
        if (input == '3') { result = 3; return true; }
        if (input == '4') { result = 4; return true; }
        if (input == '5') { result = 5; return true; }
        if (input == '6') { result = 6; return true; }
        if (input == '7') { result = 7; return true; }
        if (input == '8') { result = 8; return true; }
        if (input == '9') { result = 9; return true; }
        else result = 0; return false;

    }
}