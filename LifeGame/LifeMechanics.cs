using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    /// <summary>
    /// Механики для игры "Жизнь"
    /// </summary>
    internal class LifeMechanics
    {
        //генерация первого поколения
        /// <summary>
        /// Генерация первого поколения жизни 
        /// </summary>
        /// <param name="rows">Высота поля</param>
        /// <param name="colums">Ширина поля</param>
        /// <returns></returns>
        public static bool[,] LifeGeneration (int rows, int colums)
        {
            bool[,] lifeFirst = new bool[rows, colums];
            Random ran = new Random();

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colums; colIndex++)
                {
                    int isLife = ran.Next(0,2);
                    if (isLife == 1)
                    {
                        lifeFirst[rowIndex, colIndex] = true;
                    }
                    else
                    {
                        lifeFirst[rowIndex, colIndex] = false;
                    }
                }

            }

            return lifeFirst;
        }

        //отрисовка поколения
        /// <summary>
        /// Рисует в консоле "жизнь" (0 - нет жизни пустое поле, 1 - есть жизнь, "*")
        /// </summary>
        /// <param name="arrayToDraw">Матрица со значениями жизнь или пусто</param>
        public static void DrawGeneration(bool[,] arrayToDraw)
        {
            for (int rowIndex = 0; rowIndex < arrayToDraw.GetLength(0); rowIndex++)
            {
                // переход и отступ
                Console.WriteLine();
                Console.Write(" ");
                for (int colIndex = 0; colIndex < arrayToDraw.GetLength(1); colIndex++)
                {
                    // есть жизнь рисуем *
                    if (arrayToDraw[rowIndex,colIndex])
                    {
                        Console.Write("*");
                    }
                    // нет жизни рисуем пустое поле
                    else
                    {
                        Console.Write(" ");
                    }

                }
            }
        }

        /// <summary>
        /// Генерация следующего поколения по правилам "жизни"
        /// </summary>
        /// <param name="arrayCompare">Матрица с предыдущим поколением "жизни"</param>
        /// <returns></returns>
        public static bool[,] NewLifeGeneration(bool[,] arrayCompare)
        {
            bool[,] newLife = new bool[arrayCompare.GetLength(0),arrayCompare.GetLength(1)];

            for (int rowIndex = 0; rowIndex < arrayCompare.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrayCompare.GetLength(1); colIndex++)
                {
                    //проверить для каждого элемента следующего поколения
                    //условия "жизни" и присвоить соответствующий статус
                    newLife[rowIndex, colIndex]= LifeTerms(arrayCompare,rowIndex,colIndex);
                    
                }

            }

            return newLife;
        }
        /// <summary>
        /// Проверка условий жизни
        /// </summary>
        /// <param name="life">Матрица "родителей"</param>
        /// <param name="rowCompare">строка потомка</param>
        /// <param name="colCompare">столбец потомка</param>
        /// <returns>Возвращает значение есть жизнь или нет</returns>
        public static bool LifeTerms(bool[,] life, int rowCompare, int colCompare)
        {
            bool isLife;
            int relatives = 0;
            for (int rowIndex = rowCompare-1; rowIndex < rowCompare+2; rowIndex++)
            {
                for (int colIndex = colCompare-1; colIndex < colCompare+2; colIndex++)
                {
                    //Исключаем пограничные значения
                    // Нашел в интернете вариант "бесконечной" матрицы через класс
                    // для реализации тора, пока тяжко
                    if (!((rowIndex < 0 || colIndex < 0) || 
                        (rowIndex>=life.GetLength(0) || colIndex>=life.GetLength(1))))
                    {   // проверка окружающих клеток за исключением самого себя
                        if (life[rowIndex, colIndex]
                            && (rowIndex != rowCompare || colIndex != colCompare) )
                            {
                            relatives++;
                            }
                    }
                 }

            }

            //проверка зарождения жизни, продолжения или умирания клетки
            if (life[rowCompare, colCompare])
            {
                if (2 <= relatives  && relatives <=3)
                {
                    isLife = true;
                }
                else
                {
                    isLife = false;
                }
            }
            else
            {
                if (relatives == 3)
                {
                    isLife = true;
                }
                else
                {
                    isLife = false;
                }
            }

            //возврат результата жизни
            return isLife;
        }

    }
}
