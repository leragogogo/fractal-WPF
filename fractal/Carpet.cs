using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractal
{
    /// <summary>
    /// Фрактал - Ковер Серпинского.
    /// </summary>
    class Carpet : Fractal
    {
        /// <summary>
        /// Лист со всеми квадратами.
        /// </summary>
        private List<double[]> listOfAllSquares;
        
        /// <summary>
        /// Лист с квадратами, которые породят еще квадраты.
        /// </summary>
        private List<double[]> listOfParentSquares;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depthRecursion">Глубина рекурсии.</param>
        public Carpet(int depthRecursion) : base(depthRecursion)
        {
            listOfAllSquares = new List<double[]>(2000);
            double[] t = new double[3] { 0, 0, 9 };
            double[] t2 = new double[3] { 3, 3, 3 };
            if (depthRecursion == 0)
            {
                listOfAllSquares.Add(t);
            }
            else
            {
                listOfAllSquares.Add(t);
                listOfAllSquares.Add(t2);
            }
            listOfParentSquares = new List<double[]>((int)Math.Pow(8, depthRecursion));
            listOfParentSquares.Add(t2);
        }

        /// <summary>
        /// Вычисление координат новых квадратов
        /// </summary>
        /// <param name="x1">X левого нижнего угла родительского квадрата.</param>
        /// <param name="y1">Y левого нижнего угла родительского квадрата.</param>
        /// <param name="x2">X правого нижнего угла родительского квадрата.</param>
        /// <param name="y2">Y правого нижнего угла родительского квадрата.</param>
        /// <param name="x3">X правого верхнего угла родительского квадрата.</param>
        /// <param name="y3">Y правого верхнего угла родительского квадрата.</param>
        /// <param name="x4">X левого верхнего угла родительского квадратa.</param>
        /// <param name="y4">Y левого верхнего угла родительского квадратa.</param>
        /// <param name="delta">Длинна стороны нового квадрата.</param>
        /// <returns>Лист с координатами левого нижнего угла и длинной квадратов. </returns>
        private List<double[]> GetNewSquares(double x1,double y1, double x2,double y2,double x3,double y3,double x4,double y4, double delta)
        {
            List<double[]> newSquares = new List<double[]>(8);
            
            double[] newSquare1 = new double[] { x1 - 2 * delta, y1 - 2 * delta, delta };
            double[] newSquare2 = new double[] { x1 + delta, y1 - 2 * delta, delta };
            double[] newSquare3 = new double[] { x1 - 2 * delta, y1 + delta, delta };
            double[] newSquare4 = new double[] { x2 + delta, y2 - 2 * delta, delta };
            double[] newSquare5 = new double[] { x2 + delta, y2 + delta, delta };
            double[] newSquare6 = new double[] { x3 + delta, y3 + delta, delta };
            double[] newSquare7 = new double[] { x3 - 2 * delta, y3 + delta, delta };
            double[] newSquare8 = new double[] { x4 - 2 * delta, y4 + delta, delta };
            newSquares.Add(newSquare1);
            newSquares.Add(newSquare2);
            newSquares.Add(newSquare3);
            newSquares.Add(newSquare4);
            newSquares.Add(newSquare5);
            newSquares.Add(newSquare6);
            newSquares.Add(newSquare7);
            newSquares.Add(newSquare8);
            return newSquares;
        }

        public override void MathFractal()
        {
            List<double[]> tempList;
            for (int i = 1; i < depthRecursion; i++)
            {
                tempList = new List<double[]>(2000);
                for (int j = 0; j < listOfParentSquares.Count; j++)
                {
                    double x1 = listOfParentSquares[j][0];
                    double y1 = listOfParentSquares[j][1];
                    double x2 = x1 + listOfParentSquares[j][2];
                    double y2 = y1;
                    double x3 = x1 + listOfParentSquares[j][2];
                    double y3 = y1 + listOfParentSquares[j][2];
                    double x4 = x1;
                    double y4 = y1 + listOfParentSquares[j][2];
                    double delta = listOfParentSquares[j][2] / 3;
                    var newSquares = GetNewSquares(x1, y1, x2, y2, x3, y3, x4, y4, delta);
                    for(int k = 0; k < newSquares.Count; k++)
                    {
                        tempList.Add(newSquares[k]);
                        listOfAllSquares.Add(newSquares[k]);
                    }
                }
                listOfParentSquares.Clear();
                foreach (var a in tempList)
                {
                    listOfParentSquares.Add(a);
                }
            }
        }

        public override void Render(Canvas canvas)
        {
            if (canvas != null)
            {
                canvas.Children.Clear();
                SolidColorBrush color;
                double mathWidth = 9;
                double mathHeight = 9;
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

                for (int i = 0; i < listOfAllSquares.Count; i++)
                {
                    if (i == 0)
                    {
                        color = Brushes.DarkBlue;
                    }
                    else
                    {
                        color = Brushes.White;
                    }
                    var rect = new Rectangle
                    {
                        Width = listOfAllSquares[i][2]*coef,
                        Height = listOfAllSquares[i][2]*coef,
                        Fill = color,
                    };
                    Canvas.SetLeft(rect, listOfAllSquares[i][0] * coef  + deltaX / 2);
                    Canvas.SetTop(rect, 9 * coef - (listOfAllSquares[i][2] + listOfAllSquares[i][1]) * coef + deltaY / 2);
                    canvas.Children.Add(rect);
                }

            }
        }
    }
}
