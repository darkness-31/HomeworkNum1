namespace Homework;

internal class Programm
{

    public static void Main(String[] args)
    {
        string[] Name = new string[] { "Филлиал №1", "Филлиал №2", "Филлиал №3", "Филлиал №4", "Филлиал №5", "Филлиал №6", "Филлиал №7", "Филлиал №8" };
        int[] ReleasePlan = new int[] { 3465, 4201, 3490, 1364, 2795, 5486, 35187, 2577 };
        int[] ReleaseExecution = new int[] { 3270, 4587, 2708, 1480, 3270, 4587, 2708, 1480};
        double[] ExecutionPlanToProcent = new double[] { };
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────┐");
            Console.WriteLine("│           Меню            │");
            Console.WriteLine("╞═══════════════════════════╡");
            Console.WriteLine("│ 1. Вывод таблицы на экран │");
            Console.WriteLine("│ 2. Ввод данных в таблицу  │");
            Console.WriteLine("│ 3. Выход                  │");
            Console.WriteLine("└───────────────────────────┘");
            Console.Write("Введите цифру пункта меню:");
            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());
            } 
            catch 
            {
                continue;
            }

            Console.Clear();
            switch (input)
            {
                case 1:
                    CalculatPercentComplation(ReleasePlan, ReleaseExecution, ref ExecutionPlanToProcent);
                    ViewTable(Name, ReleasePlan, ReleaseExecution, ExecutionPlanToProcent);
                    break;
                case 2:
                    WriteData(ref Name, ref ReleasePlan, ref ReleaseExecution);
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("нет такого в меню");
                    Thread.Sleep(1000);
                    break;
            }

            Console.WriteLine();
            Console.Write("Для продолжение нажмите любую клавишу... ");
            Console.ReadKey();

        }

    }

    public static void WriteData(ref string[] NameArr, ref int[] PlanArr, ref int[] PlanExeArr)
    {
        NameInput:
        Console.Write("Введите название филиала: ");
        string? name = Console.ReadLine();
        if (name == "")
        {
            Console.WriteLine("Пустое значение");
            Console.WriteLine();
            goto NameInput;
        }
        else if (NameArr.Contains(name))
        {
            Console.WriteLine("Есть похожее название в таблице");
            Console.WriteLine();
            goto NameInput;
        }

        int? plan;
        int? execute;
        
        PlanInput:
        Console.Write("Введите план выпуска: ");
        try
        {
            plan = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Не число");
            Console.WriteLine();
            goto PlanInput;
        }

        PlanExeInput:
        Console.Write("Введите Сколько фактически выпущено: ");
        try
        {
            execute = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Не число");
            Console.WriteLine();
            goto PlanExeInput;
        }

        while (true)
        {
            for (int i=0; i < Console.WindowWidth ;i++ ) Console.Write("─");
            Console.WriteLine();

            Console.Write("Редактировать данные? [д/н]: ");
            bool answer = Console.ReadLine()?.ToLower() == "д" ? true : false;
            if (!answer) break;

            Console.WriteLine();
            
            Console.WriteLine("1. Называние филиала: {0}", name);
            Console.WriteLine("2. план выпуска: {0}", plan);
            Console.WriteLine("3. Сколько фактически выпущено: {0}", execute);
            Console.WriteLine("Любая цифра. выйти");
            Console.Write("Введите цифру данных: ");

            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());
            }
            catch
            {
                continue;
            }
            
            switch (input)
            {
                case 1:
                    Console.WriteLine("Старое: {0}", name);
                    Console.Write("Новое: ");
                    name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Старое: {0}", plan);

                    PlanEdit:
                    Console.Write("Новое: ");
                    try
                    {
                        plan = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Не число");
                        Console.WriteLine();
                        goto PlanEdit;
                    }
                    break;
                case 3:
                    Console.WriteLine("Старое: {0}", execute);
                    PlanExeEdit:
                    Console.Write("Новое: ");
                    try
                    {
                        execute = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Не число");
                        Console.WriteLine();
                        goto PlanExeEdit;
                    }
                    break;
                default:
                    break;
            }

        }

        ReplaceName:
        if (name == "")
        {
            Console.WriteLine();
            Console.Write("Введите имя: ");
            name = Console.ReadLine();
            goto ReplaceName;
        }
        if (plan == null)
        {
            PlanReplace:
            try
            {
                Console.Write("Введите план выпуска");
                plan = int.Parse(Console.ReadLine());
            }
            catch 
            {
                Console.WriteLine("Не число");
                Console.WriteLine();
                goto PlanReplace;
            }
        } 
        if (execute == null)
        {
            PlanExeReplace:
            try
            {
                Console.Write("Введите план выпуска");
                execute = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Не число");
                Console.WriteLine();
                goto PlanExeReplace;
            }
        }


        Array.Resize(ref NameArr, NameArr.Length + 1);
        Array.Resize(ref PlanArr, PlanArr.Length + 1);
        Array.Resize(ref PlanExeArr, PlanExeArr.Length + 1);

        NameArr.SetValue(name, NameArr.Length - 1);

        try
        {
            PlanArr.SetValue(plan, PlanArr.Length - 1);
            PlanExeArr.SetValue(execute, PlanExeArr.Length - 1);
        }
        catch
        {
            Console.WriteLine("Неправильный ввод данных");
            return;
        }
    }

    public static void ViewTable(string[] NameArr, int[] PlanArr, int[] PlanExeArr, double[] Procent)
    {
        int columnLenght_NameArr = (NameArr.Max(x => x.Length) > "Наименования".Length 
                                    ? NameArr.Max(x => x.Length) 
                                    : "Наименования".Length) + 10;
        int columnLenght_PlanArr = (PlanArr.Max(x => x.ToString().Length) > "План выпуска".Length 
                                    ? PlanArr.Max(x => x.ToString().Length) 
                                    : "План выпуска".Length) + 10;
        int columnLenght_PlanExeArr = (PlanExeArr.Max(x => x.ToString().Length) > "Фактически Выпущено".Length 
                                       ? PlanExeArr.Max(x => x.ToString().Length) 
                                       : "Фактически Выпущено".Length) + 10;
        int columnLenght_Percent = (Procent.Max(x => x.ToString().Length) > "% выполнения".Length 
                                    ? Procent.Max(x => x.ToString().Length) 
                                    : "% выполнения".Length) + 10;

        int MaxLenghtTable = columnLenght_NameArr
                                + columnLenght_PlanArr
                                + columnLenght_PlanExeArr
                                + columnLenght_Percent;

        #region Header

        //Верхний слой головы
        for (int i=0; i<=MaxLenghtTable; i++)
        {
            if (i == 0)
                Console.Write("╒");
            else if (i == (columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("╤");
            else if (i == MaxLenghtTable) 
                Console.Write("╕");
            else
                Console.Write("═");
        }

        Console.WriteLine();

        // Внутренний слой головы
        for (int l = 0; l < 2; l++)
        {
            if (l == 0) goto EmptySpace;

            for (int i = 0; i <= MaxLenghtTable; i++)
            {
                if (i == 0 ||
                    i == MaxLenghtTable ||
                    i == (columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                    Console.Write("│");
                else if (i == 6)
                {
                    Console.Write("Наименования ");
                    i += "Наименования".Length;
                }
                else if (i == columnLenght_NameArr + 6)
                {
                    Console.Write("План выпуска ");
                    i += "План выпуска".Length;
                }
                else if (i == columnLenght_PlanArr + columnLenght_NameArr + 6)
                {
                    Console.Write("Фактически Выпущено ");
                    i += "Фактически Выпущено".Length;
                }
                else if (i == columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 6)
                {
                    Console.Write("% выполнения ");
                    i += "% выполнения".Length;
                }
                else
                    Console.Write(" ");
            }
            Console.WriteLine();

            EmptySpace:
            for (int i = 0; i <= MaxLenghtTable; i++)
            {
                if (i == 0 || 
                    i == MaxLenghtTable ||
                    i == (columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                    Console.Write("│");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }

        //Нижний слой головы
        for (int i = 0; i <= MaxLenghtTable; i++)
        {
            if (i == 0)
                Console.Write("╞");
            else if (i == (columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("╪");
            else if (i == MaxLenghtTable)
                Console.Write("╡");
            else
                Console.Write("═");
        }

        #endregion

        Console.WriteLine();

        #region Body

        //Внутренний слой
        for (int l = 0; l < NameArr.Length; l++)
        {
            for (int i = 0; i <= MaxLenghtTable; i++)
            {
                if (i == 0 ||
                    i == MaxLenghtTable ||
                    i == (columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                    Console.Write("│");
                else
                    Console.Write(" ");
            }

            Console.WriteLine();

            for (int i = 0; i <= MaxLenghtTable; i++)
            {

                if (i == 0 ||
                    i == MaxLenghtTable ||
                    i == (columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                    i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                    Console.Write("│");
                else if (i == 6)
                {
                    Console.Write(NameArr[l] + " ");
                    i += NameArr[l].Length;
                }
                else if (i == columnLenght_PlanArr + columnLenght_NameArr - PlanArr[l].ToString().Length - 5)
                {
                    Console.Write(PlanArr[l] + " ");
                    i += PlanArr[l].ToString().Length;
                }
                else if (i == columnLenght_PlanArr + columnLenght_NameArr + columnLenght_PlanExeArr - PlanExeArr[l].ToString().Length - 5)
                {
                    Console.Write(PlanExeArr[l] + " ");
                    i += PlanExeArr[l].ToString().Length;
                }
                else if (i == columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + (int)(columnLenght_Percent/2D))
                {
                    Console.Write(Procent[l] + " ");
                    i += Procent[l].ToString().Length;
                }
                else
                    Console.Write(" ");
            }

            Console.WriteLine();

        }

        // отступ
        for (int i = 0; i <= MaxLenghtTable; i++)
        {
            if (i == 0 ||
                i == MaxLenghtTable ||
                i == (columnLenght_NameArr + 1) ||
                i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("│");
            else
                Console.Write(" ");
        }

        Console.WriteLine();

        //Нижний слой
        for (int i = 0; i <= MaxLenghtTable; i++)
        {
            if (i == 0)
                Console.Write("╞");
            else if (i == (columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("╪");
            else if (i == MaxLenghtTable)
                Console.Write("╡");
            else
                Console.Write("═");
        }

        #endregion

        Console.WriteLine();

        #region Footer

        CalculatTotals(PlanArr, PlanExeArr, out int PlanTotal, out int PlanExeTotal, out double ProcentTotal);
        for (int i = 0; i <= MaxLenghtTable; i++)
        {
            if (i == 0 ||
                i == MaxLenghtTable ||
                i == (columnLenght_NameArr + 1) ||
                i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("│");
            else if (i == 6)
            {
                Console.Write("Итог: ");
                i += "Итог:".Length;
            }
            else if (i == columnLenght_PlanArr + columnLenght_NameArr - PlanTotal.ToString().Length - 5)
            {
                Console.Write(PlanTotal + " ");
                i += PlanTotal.ToString().Length;
            }
            else if (i == columnLenght_PlanArr + columnLenght_NameArr + columnLenght_PlanExeArr - PlanExeTotal.ToString().Length - 5)
            {
                Console.Write(PlanExeTotal + " ");
                i += PlanExeTotal.ToString().Length;
            }
            else if (i == columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + columnLenght_Percent - ProcentTotal.ToString().Length - 5)
            {
                Console.Write(ProcentTotal + " ");
                i += ProcentTotal.ToString().Length;
            }
            else
                Console.Write(" ");
        }

        Console.WriteLine();

        for (int i = 0; i <= MaxLenghtTable; i++)
        {
            if (i == 0)
                Console.Write("╘");
            else if (i == (columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanArr + columnLenght_NameArr + 1) ||
                     i == (columnLenght_PlanExeArr + columnLenght_PlanArr + columnLenght_NameArr + 1))
                Console.Write("╧");
            else if (i == MaxLenghtTable)
                Console.Write("╛");
            else
                Console.Write("═");
        }

        Console.WriteLine();
        #endregion
    }

    public static void CalculatTotals(int[] PlanArr, int[] ExePlan, out int PlanTotal, out int PlanExeTotal, out double ProcentTotal)
    {
        PlanTotal = PlanArr.Sum();
        PlanExeTotal = ExePlan.Sum();
        ProcentTotal = Math.Round(Convert.ToDouble(PlanTotal) / Convert.ToDouble(PlanExeTotal) * 100, 2);
    }

    public static void CalculatPercentComplation(int[] PlanArr, int[] ExePlan, ref double[] procent)
    {
        Array.Resize(ref procent, PlanArr.Length);

        for (int i=0; i<PlanArr.Length; i++)
        {
            procent[i] = Math.Round(Convert.ToDouble(ExePlan[i]) / Convert.ToDouble(PlanArr[i]) * 100, 2);
    }
    }
}