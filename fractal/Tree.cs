using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace fractal
{
    /// <summary>
    /// Фрактал - Фрактальное дерево.
    /// </summary>
    class Tree : Fractal
    {
        /// <summary>
        /// Коэффицент отношения длин предыдущей и следующей ветки.
        /// </summary>
        private double coefficient;
        public double Coefficient
        {
            set
            {
                coefficient = value;
            }
        }

        /// <summary>
        /// Угол наклона первой ветки.
        /// </summary>
        private double angle1;
        public double Angle1
        {
            set
            {
                angle1 = value;
            }
        }

        /// <summary>
        /// Угол наклона второй ветки.
        /// </summary>
        private double angle2;
        public double Angle2
        {
            set
            {
                angle2 = value;
            }
        }

        /// <summary>
        /// Лист с ветками из которых будут расти еще ветки.
        /// </summary>
        private List<double[,]> listOfLines;
        
        /// <summary>
        /// Массив со всеми ветками дерева.
        /// </summary>
        private double[,,] arrayOfLines;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depthRecursion">Глубина рекурсии</param>
        /// <param name="coef">Коэффицент отношения длин предыдущей и следующей ветки. </param>
        /// <param name="ang1">Угол наклона первой ветки.</param>
        /// <param name="ang2">Угол наклона второй ветки.</param>
        public Tree(int depthRecursion, double coef, int ang1, int ang2) : base(depthRecursion)
        {
            Coefficient = coef;
            Angle1 = ang1 * Math.PI / 180;
            Angle2 = ang2 * Math.PI / 180;
            listOfLines = new List<double[,]>(depthRecursion * 2);
            var t = new double[2, 2];
            t[0, 0] = 0;
            t[0, 1] = 0;
            t[1, 0] = 0;
            t[1, 1] = 10;
            listOfLines.Add(t);
            arrayOfLines = new double[(int)Math.Pow(2, depthRecursion + 1) - 1, 2, 2];
            arrayOfLines[0, 0, 0] = 0;
            arrayOfLines[0, 0, 1] = 0;
            arrayOfLines[0, 1, 0] = 0;
            arrayOfLines[0, 1, 1] = 10;

        }

        /// <summary>
        /// Вычисление координат точек конца двух новых веток.
        /// </summary>
        /// <param name="x1">X начала родительской ветки.</param>
        /// <param name="x2">X конца родительской ветки.</param>
        /// <param name="y1">Y начала родительской ветки.</param>
        /// <param name="y2">Y конца родительской ветки.</param>
        /// <param name="len">Длина новой ветки.</param>
        /// <param name="alfa">Вспомогательный угол.</param>
        /// <param name="j">Индекс родителя.</param>
        /// <returns>Массив с координатами точек конца двух новых веток.</returns>
        private double[] GetNewPoints(double x1, double x2, double y1, double y2, double len, double alfa, int j)
        {
            double[] points = new double[4];
            if ((y2 > y1) && (x2 >= x1))
            {
                points[0] = listOfLines[j][1, 0] + Math.Cos(angle1 - alfa - Math.PI / 2) * len;
                points[1] = listOfLines[j][1, 1] + Math.Sin(angle1 - alfa - Math.PI / 2) * len;
                points[2] = listOfLines[j][1, 0] + Math.Cos(angle2 - alfa - Math.PI / 2) * len;
                points[3] = listOfLines[j][1, 1] + Math.Sin(angle2 - alfa - Math.PI / 2) * len;
            }
            else if ((y2 >= y1) && (x2 < x1))
            {
                points[0] = listOfLines[j][1, 0] - Math.Cos(angle1 - alfa - Math.PI / 2) * len;
                points[1] = listOfLines[j][1, 1] + Math.Sin(angle1 - alfa - Math.PI / 2) * len;
                points[2] = listOfLines[j][1, 0] - Math.Cos(angle2 - alfa - Math.PI / 2) * len;
                points[3] = listOfLines[j][1, 1] + Math.Sin(angle2 - alfa - Math.PI / 2) * len;
            }
            else if ((y2 < y1) && (x2 < x1))
            {
                points[0] = listOfLines[j][1, 0] - Math.Cos(angle1 - alfa - Math.PI / 2) * len;
                points[1] = listOfLines[j][1, 1] - Math.Sin(angle1 - alfa - Math.PI / 2) * len;
                points[2] = listOfLines[j][1, 0] - Math.Cos(angle2 - alfa - Math.PI / 2) * len;
                points[3] = listOfLines[j][1, 1] - Math.Sin(angle2 - alfa - Math.PI / 2) * len;
            }
            else
            {
                points[0] = listOfLines[j][1, 0] + Math.Cos(angle1 - alfa - Math.PI / 2) * len;
                points[1] = listOfLines[j][1, 1] - Math.Sin(angle1 - alfa - Math.PI / 2) * len;
                points[2] = listOfLines[j][1, 0] + Math.Cos(angle2 - alfa - Math.PI / 2) * len;
                points[3] = listOfLines[j][1, 1] - Math.Sin(angle2 - alfa - Math.PI / 2) * len;
            }
            return points;
        }
        public override void MathFractal()
        {
            List<double[,]> tempList;
            double len = 10;
            int countOfLines = 1;
            for (int i = 0; i < depthRecursion; i++)
            {
                tempList = new List<double[,]>(depthRecursion * 2);
                len *= coefficient;
                for (int j = 0; j < listOfLines.Count; j++)
                {
                    double alfa = Math.Atan(Math.Abs(listOfLines[j][1, 0] - listOfLines[j][0, 0]) / Math.Abs(listOfLines[j][1, 1] - listOfLines[j][0, 1]));
                    var points = GetNewPoints(listOfLines[j][0, 0], listOfLines[j][1, 0], listOfLines[j][0, 1], listOfLines[j][1, 1], len, alfa, j);
                    double[,] t1 = new double[2, 2];
                    t1[0, 0] = listOfLines[j][1, 0];
                    t1[0, 1] = listOfLines[j][1, 1];
                    t1[1, 0] = points[0];
                    t1[1, 1] = points[1];
                    double[,] t2 = new double[2, 2];
                    t2[0, 0] = listOfLines[j][1, 0];
                    t2[0, 1] = listOfLines[j][1, 1];
                    t2[1, 0] = points[2];
                    t2[1, 1] = points[3];
                    tempList.Add(t1);
                    tempList.Add(t2);
                    arrayOfLines[countOfLines, 0, 0] = listOfLines[j][1, 0];
                    arrayOfLines[countOfLines, 0, 1] = listOfLines[j][1, 1];
                    arrayOfLines[countOfLines, 1, 0] = points[0];
                    arrayOfLines[countOfLines, 1, 1] = points[1];
                    arrayOfLines[countOfLines + 1, 0, 0] = listOfLines[j][1, 0];
                    arrayOfLines[countOfLines + 1, 0, 1] = listOfLines[j][1, 1];
                    arrayOfLines[countOfLines + 1, 1, 0] = points[2];
                    arrayOfLines[countOfLines + 1, 1, 1] = points[3];
                    countOfLines += 2;

                }
                listOfLines.Clear();
                foreach (var a in tempList)
                {
                    listOfLines.Add(a);
                }
            }
        }

        /// <summary>
        /// Вычисление минимального и максимального x,y фрактала.
        /// Нужно для маштабирования.
        /// </summary>
        /// <returns>Массив с минимальным и максимальным x,y.</returns>
        private double[] GetMimMaxValues()
        {
            double[] arr = new double[4];
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;
            for (int i = 0; i < arrayOfLines.GetLength(0); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (arrayOfLines[i, j, 0] < minX)
                    {
                        minX = arrayOfLines[i, j, 0];
                    }
                    if (arrayOfLines[i, j, 0] > maxX)
                    {
                        maxX = arrayOfLines[i, j, 0];
                    }
                    if (arrayOfLines[i, j, 1] < minY)
                    {
                        minY = arrayOfLines[i, j, 1];
                    }
                    if (arrayOfLines[i, j, 1] > maxY)
                    {
                        maxY = arrayOfLines[i, j, 1];
                    }
                }
            }
            arr[0] = minX;
            arr[1] = minY;
            arr[2] = maxX;
            arr[3] = maxY;
            return arr;
        }
        public override void Render(Canvas canvas)
        {
            if (canvas != null)
            {
                canvas.Children.Clear();
                var minMaxValues = GetMimMaxValues();
                double mathWidth = minMaxValues[2] - minMaxValues[0];
                double mathHeight = minMaxValues[3] - minMaxValues[1];
                double coef;
                double deltaX = 0;
                double deltaY = 0;
                if (mathWidth / mathHeight < canvas.Width / (canvas.Height - 40))
                {
                    coef = (canvas.Height - 40) / mathHeight;
                    deltaX = canvas.Width - mathWidth * coef;
                }
                else
                {
                    coef = canvas.Width / mathWidth;
                    deltaY = canvas.Height - 40 - mathHeight * coef;
                }

                for (int i = 0; i < arrayOfLines.GetLength(0); i++)
                {
                    double x1 = arrayOfLines[i, 0, 0];
                    double y1 = arrayOfLines[i, 0, 1];
                    double x2 = arrayOfLines[i, 1, 0];
                    double y2 = arrayOfLines[i, 1, 1];
                    canvas.Children.Add(new Line
                    {
                        X1 = x1 * coef - minMaxValues[0] * coef + deltaX / 2,
                        X2 = x2 * coef - minMaxValues[0] * coef + deltaX / 2,
                        Y1 = minMaxValues[3] * coef - y1 * coef + deltaY / 2,
                        Y2 = minMaxValues[3] * coef - y2 * coef + deltaY / 2,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    });
                }
            }
        }
    }
}


