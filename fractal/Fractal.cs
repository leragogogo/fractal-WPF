using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace fractal
{
    /// <summary>
    /// Родительский класс для всех видов фракталов.
    /// </summary>
    class Fractal
    {
        /// <summary>
        /// Глубина рекурсии.
        /// </summary>
        protected int depthRecursion;
        public int DepthRecursion
        {
            set
            {
                depthRecursion = value;
            }
            get
            {
                return depthRecursion;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        public Fractal(int depth)
        {
            DepthRecursion = depth;
        }

        /// <summary>
        /// Вычисление координат элементов фрактала.
        /// </summary>
        public virtual void MathFractal()
        {

        }

        /// <summary>
        /// Отрисовка фрактала.
        /// </summary>
        /// <param name="canvas">Canvas, на котором фрактал будет нарисован.</param>
        public virtual void Render(Canvas canvas)
        {

        }
    }
}
