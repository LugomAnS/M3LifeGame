using System;

namespace LifeGame
{
    internal class Program
    {
        static void Main()
        {
            //
            //https://ru.wikipedia.org/wiki/%D0%98%D0%B3%D1%80%D0%B0_%C2%AB%D0%96%D0%B8%D0%B7%D0%BD%D1%8C%C2%BB
            //
            //Сгенерировать первое поколение жизни
            //Отрисовать поколение
            //Проверка "правил жизни" и генерация следующего поколения
            //Отрисовать поколение
            //зациклить на n поколений
            //задержка отрисовки
            const int HEIGHT = 15;
            const int WIDTH = 25;
            const int LIFE_CIRCLE = 50;
            int lifeCount = 0;
            Console.CursorVisible = false;
            //генерация первого поколения
            bool[,] currentLife = LifeMechanics.LifeGeneration(HEIGHT, WIDTH);
           
            //отрисовка первого поколения
            LifeMechanics.DrawGeneration(currentLife);
            
            bool[,] nextGeneration;
            do
            {
                //сгенерировать новое поколение
                nextGeneration = LifeMechanics.NewLifeGeneration(currentLife);
                Console.Clear();
                //отрисовать поколение
                LifeMechanics.DrawGeneration(nextGeneration);
                // сделать сгенерированное поколение текущим
                currentLife = nextGeneration;
                //задержка отрисовки
                Thread.Sleep(100);
                
                lifeCount++;
            } while (lifeCount< LIFE_CIRCLE);

        }
    }
}