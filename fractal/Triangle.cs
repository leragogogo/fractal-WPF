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
    /// Фрактал - Треугольник Серпинского.
    /// </summary>
    class Triangle : Fractal
    {
        /// <summary>
        /// Лист со всеми треугольниками.
        /// </summary>
        private List<double[,]> listOfAllTriangles;
        
        /// <summary>
        /// Лист с треугольниками, которые породят еще треугольников.
        /// </summary>
        private List<double[,]> listOfParentTriangles;
        
        /// <summary>
        /// Высота текущего треугольника. 
        /// </summary>
        double h;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depthRecursion">Глубина рекурсии.</param>
        public Triangle(int depthRecursion) : base(depthRecursion)
        {
            listOfAllTriangles = new List<double[,]>(2000);
            listOfParentTriangles = new List<double[,]>((int)Math.Pow(3, depthRecursion));
            double[,] t = new double[,] { { 0, 0 }, { 10, 0 }, { 5, Math.Sqrt(75) } };
            double[,] t2 = new double[,] { { 5, 0 }, { 7.5, Math.Sqrt(75) / 2 }, { 2.5, Math.Sqrt(75) / 2 } };
            h = Math.Sqrt(75) / 2;
            if (depthRecursion == 0)
            {
                listOfAllTriangles.Add(t);
            }
            else
            {
                listOfAllTriangles.Add(t);
                listOfAllTriangles.Add(t2);
            }
            listOfParentTriangles.Add(t2);

        }

        public override void MathFractal()
        {
            List<double[,]> tempList;
            for (int i = 1; i < depthRecursion; i++)
            {
                tempList = new List<double[,]>((int)Math.Pow(3, depthRecursion));
                for (int j = 0; j < listOfParentTriangles.Count; j++)
                {
                    // с 46 по 63 считаются координаты вершин новых треугольников.
                    double triangle1X1 = (listOfParentTriangles[j][1, 0] + listOfParentTriangles[j][2, 0]) / 2;
                    double triangle1Y1 = (listOfParentTriangles[j][1, 1] + listOfParentTriangles[j][2, 1]) / 2;
                    double triangle2X1 = (listOfParentTriangles[j][0, 0] + listOfParentTriangles[j][1, 0]) / 2;
                    double triangle2Y1 = (listOfParentTriangles[j][0, 1] + listOfParentTriangles[j][1, 1]) / 2;
                    double triangle3X1 = (listOfParentTriangles[j][0, 0] + listOfParentTriangles[j][2, 0]) / 2;
                    double triangle3Y1 = (listOfParentTriangles[j][0, 1] + listOfParentTriangles[j][2, 1]) / 2;
                    double triangle1X2 = (triangle1X1 + listOfParentTriangles[j][1, 0]) / 2;
                    double triangle1Y2 = triangle1Y1 + h / 2;
                    double triangle1X3 = (triangle1X1 + listOfParentTriangles[j][2, 0]) / 2;
                    double triangle1Y3 = triangle1Y1 + h / 2;
                    double triangle2X2 = listOfParentTriangles[j][1, 0];
                    double triangle2Y2 = listOfParentTriangles[j][1, 1] - h;
                    double triangle3X2 = listOfParentTriangles[j][2, 0];
                    double triangle3Y2 = listOfParentTriangles[j][2, 1] - h;
                    double triangle2X3 = triangle2X2 + (triangle2X2 - triangle2X1);
                    double triangle2Y3 = triangle2Y1;
                    double triangle3X3 = triangle3X2 - (triangle3X1 - triangle3X2);
                    double triangle3Y3 = triangle3Y1;

                    double[,] triangle1 = new double[,] { { triangle1X1, triangle1Y1 }, { triangle1X2, triangle1Y2 }, { triangle1X3, triangle1Y3 } };
                    double[,] triangle2 = new double[,] { { triangle2X2, triangle2Y2 }, { triangle2X3, triangle2Y3 }, { triangle2X1, triangle2Y1 } };
                    double[,] triangle3 = new double[,] { { triangle3X2, triangle3Y2 }, { triangle3X1, triangle3Y1 }, { triangle3X3, triangle3Y3 } };

                    tempList.Add(triangle1);
                    tempList.Add(triangle2);
                    tempList.Add(triangle3);

                    listOfAllTriangles.Add(triangle1);
                    listOfAllTriangles.Add(triangle2);
                    listOfAllTriangles.Add(triangle3);

                }
                listOfParentTriangles.Clear();
                foreach (var a in tempList)
                {
                    listOfParentTriangles.Add(a);
                }
                h = h / 2;
            }
        }

        /// <summary>
        /// Добавление линии на canvas.
        /// </summary>
        /// <param name="canvas">Сanvas</param>
        /// <param name="x1">X начала линии.</param>
        /// <param name="y1">Y начала линии.</param>
        /// <param name="x2">X конца линии.</param>
        /// <param name="y2">Y конца линии.</param>
        /// <param name="coef">Коэффицент, нужный для маштабирования.</param>
        /// <param name="deltaX">Разница между шириной canvas и фрактала.</param>
        /// <param name="deltaY">Разница между высотой canvas и фрактала.</param>
        private void AddLineToCanvas(Canvas canvas,double x1,double y1,double x2,double y2, double coef,double deltaX,double deltaY)
        {
            canvas.Children.Add(new Line
            {
                X1 = x1 * coef + deltaX / 2,
                X2 = x2 * coef + deltaX / 2,
                Y1 = Math.Sqrt(75) * coef - y1 * coef + deltaY / 2,
                Y2 = Math.Sqrt(75) * coef - y2 * coef + deltaY / 2,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });
        }

        public override void Render(Canvas canvas)
        {
            if (canvas != null)
            {
                canvas.Children.Clear();
                double mathWidth = 10;
                double mathHeight = Math.Sqrt(75);
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

                for (int i = 0; i < listOfAllTriangles.Count; i++)
                {
                    AddLineToCanvas(canvas, listOfAllTriangles[i][0, 0], listOfAllTriangles[i][0, 1], listOfAllTriangles[i][1, 0], listOfAllTriangles[i][1, 1], coef, deltaX, deltaY);
                    AddLineToCanvas(canvas, listOfAllTriangles[i][1, 0], listOfAllTriangles[i][1, 1], listOfAllTriangles[i][2, 0], listOfAllTriangles[i][2, 1], coef, deltaX, deltaY);
                    AddLineToCanvas(canvas, listOfAllTriangles[i][0, 0], listOfAllTriangles[i][0, 1], listOfAllTriangles[i][2, 0], listOfAllTriangles[i][2, 1], coef, deltaX, deltaY);
                }
            }
        }
    }
}
