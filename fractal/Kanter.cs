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
    /// Фрактал - Множество Кантера.
    /// </summary>
    class Kanter : Fractal
    {
        /// <summary>
        /// Расстояние между линиями, нарисованными на разных итерациях.
        /// </summary>
        private int distance;
        public int Distance
        {
            set
            {
                distance = value;
            }
        }
        /// <summary>
        /// Лист всех линий.
        /// </summary>
        private List<double[,]> listOfAllLines;

        /// <summary>
        /// Лист линий, которые порождают новые.
        /// </summary>
        private List<double[,]> listOfParentLines;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depthRecursion">Глубина рекурсии.</param>
        /// <param name="d">Расстояние между линиями, нарисованными на разных итерациях.</param>
        public Kanter(int depthRecursion, int d) : base(depthRecursion)
        {
            Distance = d;
            listOfAllLines = new List<double[,]>((int)Math.Pow(2, depthRecursion));
            listOfParentLines = new List<double[,]>((int)Math.Pow(2, depthRecursion));
            double[,] t = new double[,] { { 0, 0 }, { 9, 0 } };
            listOfParentLines.Add(t);
            listOfAllLines.Add(t);
        }

        public override void MathFractal()
        {
            List<double[,]> tempList;
            for (int i = 0; i < depthRecursion; i++)
            {
                tempList = new List<double[,]>((int)Math.Pow(2, depthRecursion));
                for (int j = 0; j < listOfParentLines.Count; j++)
                {
                    double delta = (listOfParentLines[j][1, 0] - listOfParentLines[j][0, 0]) / 3;
                    double x1 = listOfParentLines[j][0, 0] + delta;
                    double x2 = x1 + delta;
                    double y = listOfParentLines[j][0, 1] - distance;
                    double[,] line1 = new double[,] { { listOfParentLines[j][0, 0], y }, { x1, y } };
                    double[,] line2 = new double[,] { { x2, y }, { listOfParentLines[j][1, 0], y } };
                    tempList.Add(line1);
                    tempList.Add(line2);
                    listOfAllLines.Add(line1);
                    listOfAllLines.Add(line2);
                }
                listOfParentLines.Clear();
                foreach (var a in tempList)
                {
                    listOfParentLines.Add(a);
                }
            }
        }
        public override void Render(Canvas canvas)
        {
            if (canvas != null)
            {
                canvas.Children.Clear();
                double mathWidth = 9;
                double mathHeight = ((double)distance/4) - listOfAllLines[listOfAllLines.Count - 1][0, 1];
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

                for (int i = 0; i < listOfAllLines.Count; i++)
                {
                    double x1 = listOfAllLines[i][0, 0];
                    double y1 = listOfAllLines[i][0, 1];
                    double x2 = listOfAllLines[i][1, 0];
                    double y2 = listOfAllLines[i][1, 1];
                    canvas.Children.Add(new Line
                    {
                        X1 = x1 * coef + deltaX / 2,
                        X2 = x2 * coef + deltaX / 2,
                        Y1 = (double)distance / 4 * coef - y1 * coef + deltaY / 2,
                        Y2 = (double)distance / 4 * coef - y2 * coef + deltaY / 2,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5 * coef
                    }) ;
                }
            }
        }
    }
}
