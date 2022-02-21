using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractal
{
    /// <summary>
    /// Фрактал - Кривая Коха.
    /// </summary>
    class KochCurve : Fractal
    {
        /// <summary>
        /// Лист всех линий.
        /// </summary>
        private List<double[,]> listOfLines;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depthRecursion">Глубина рекурсии.</param>
        public KochCurve(int depthRecursion) : base(depthRecursion)
        {
            listOfLines = new List<double[,]>((int)Math.Pow(4, depthRecursion));
            var t = new double[2, 2];
            t[0, 0] = 0;
            t[0, 1] = 0;
            t[1, 0] = 9;
            t[1, 1] = 0;
            listOfLines.Add(t);

        }

        /// <summary>
        /// Вычисление координат новой точки.
        /// </summary>
        /// <param name="x1_">X начала первой родительской линии. </param>
        /// <param name="y1_">Y начала первой первой родительской линии.</param>
        /// <param name="x2_">X конца первой родительской линии.</param>
        /// <param name="y2_">Y конца первой родительской линии.</param>
        /// <param name="x1">X начала второй родительской линии.</param>
        /// <param name="y1">Y начала второй родительской линии.</param>
        /// <param name="x2">X конца второй родительской линии.</param>
        /// <param name="y2">Y конца второй родительской линии.</param>
        /// <returns>Координаты новой точки</returns>
        private double[] GetNewPoint(double x1_,double y1_,double x2_,double y2_,double x1, double y1, double x2,double y2)
        {
            double[] point = new double[2];
            double len = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double y = Math.Sqrt(Math.Pow(len, 2) - Math.Pow(len / 2, 2));
            double x3 = 0;
            double y3 = 0;
            if ((Math.Abs(y2_ - y1_) < 0.1) && (x2_ > x1_))
            {
                x3 = x1 + (len / 2);
                y3 = y1 + y;
            }
            else if ((Math.Abs(y2_ - y1_) < 0.1) && (x2_ < x1_))
            {
                x3 = x1 - (len / 2);
                y3 = y1 - y;
            }
            else if ((y2_ > y1_) && (x2_ > x1_))
            {
                x3 = x1 - (len / 2);
                y3 = y1 + y;
            }
            else if ((y2_ < y1_) && (x2_ < x1_))
            {
                x3 = x1 + (len / 2);
                y3 = y1 - y;
            }
            else if ((y2_ < y1_) && (x2_ > x1_))
            {
                x3 = x2 + (len / 2);
                y3 = y2 + y;
            }
            else if ((y2_ > y1_) && (x2_ < x1_))
            {
                x3 = x2 - (len / 2);
                y3 = y2 - y;
            }
            point[0] = x3;
            point[1] = y3;
            return point;
        }

        public override void MathFractal()
        {
            List<double[,]> tempList;
            for (int i = 0; i < depthRecursion; i++)
            {
                tempList = new List<double[,]>((int)Math.Pow(4, depthRecursion));
                for (int j = 0; j < listOfLines.Count; j++)
                {
                    double x1_ = listOfLines[j][0, 0];
                    double y1_ = listOfLines[j][0, 1];
                    double x2_ = listOfLines[j][1, 0];
                    double y2_ = listOfLines[j][1, 1];
                    double deltaX = (x2_ - x1_) / 3;
                    double deltaY = (y2_ - y1_) / 3;
                    double x1 = x1_ + deltaX;
                    double x2 = x2_ - deltaX;
                    double y1 = y1_ + deltaY;
                    double y2 = y2_ - deltaY;
                    double[] point  = GetNewPoint(x1_,y1_,x2_,y2_,x1,y1,x2,y2);
                    double x3 = point[0];
                    double y3 = point[1];
                    double[,] line1 = { { x1_, y1_ }, { x1, y1 } };
                    double[,] line2 = { { x1, y1 }, { x3, y3 } };
                    double[,] line3 = { { x3, y3 }, { x2, y2 } };
                    double[,] line4 = { { x2, y2 }, { x2_, y2_ } };
                    tempList.Add(line1);
                    tempList.Add(line2);
                    tempList.Add(line3);
                    tempList.Add(line4);
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
            for (int i = 0; i < listOfLines.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (listOfLines[i][j, 0] < minX)
                    {
                        minX = listOfLines[i][j, 0];
                    }
                    if (listOfLines[i][j, 0] > maxX)
                    {
                        maxX = listOfLines[i][j, 0];
                    }
                    if (listOfLines[i][j, 1] < minY)
                    {
                        minY = listOfLines[i][j, 1];
                    }
                    if (listOfLines[i][j, 1] > maxY)
                    {
                        maxY = listOfLines[i][j, 1];
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
                double[] minMaxValues = GetMimMaxValues();
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

                for (int i = 0; i < listOfLines.Count; i++)
                {
                    double x1 = listOfLines[i][0, 0];
                    double y1 = listOfLines[i][0, 1];
                    double x2 = listOfLines[i][1, 0];
                    double y2 = listOfLines[i][1, 1];
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
