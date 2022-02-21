using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fractal
{
    /// <summary>
    /// Главное окно.
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flag = true;

        private Fractal fractal;

        /// <summary>
        /// Флаг, показывающий был ли создан фрактал.
        /// </summary>
        private bool isFractalCreated = false;

        /// <summary>
        /// Флаг, показывающий был ли выбран тип фрактала.
        /// </summary>
        private bool isTypeFractalChoosen = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание фрактального дерева.
        /// </summary>
        private void CreateTree(int depth, double coef, int angle1, int angle2)
        {
            fractal = new Tree(depth, coef, angle1, angle2);
            isFractalCreated = true;
            fractal.MathFractal();
            fractal.Render(canvas);
        }

        /// <summary>
        /// Создание кривой Коха.
        /// </summary>
        private void CreateKoch(int depth)
        {
            fractal = new KochCurve(depth);
            isFractalCreated = true;
            fractal.MathFractal();
            fractal.Render(canvas);
        }
        /// <summary>
        /// Создание ковра Серпинского.
        /// </summary>
        private void CreateCarpet(int depth)
        {
            fractal = new Carpet(depth);
            isFractalCreated = true;
            fractal.MathFractal();
            fractal.Render(canvas);
        }
        /// <summary>
        /// Создание треугольника Серпинского.
        /// </summary>
        private void CreateTriangle(int depth)
        {
            fractal = new Triangle(depth);
            isFractalCreated = true;
            fractal.MathFractal();
            fractal.Render(canvas);
        }
        /// <summary>
        /// Создание множества Кантора.
        /// </summary>
        private void CreateKanter(int depth, int distance)
        {
            fractal = new Kanter(depth, distance);
            isFractalCreated = true;
            fractal.MathFractal();
            fractal.Render(canvas);
        }

        /// <summary>
        /// Обработчик события выбора в comboBox.
        /// </summary>
        /// <param name="sender">comboBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            flag = !comboBox.IsDropDownOpen;
            DoVisibility();
            if (comboBox.SelectedItem == Tree)
            {
                CreateTree(0, 0.8, 135, 225);
            }
            else if (comboBox.SelectedItem == Koсh)
            {
                CreateKoch(0);
            }
            else if (comboBox.SelectedItem == Carpet)
            {
                CreateCarpet(0);
            }
            else if (comboBox.SelectedItem == Triangle)
            {
                CreateTriangle(0);
            }
            else if (comboBox.SelectedItem == Kanter)
            {
                CreateKanter(0, 1);
            }
        }


        /// <summary>
        /// Обработчик события потери фокуса.
        /// </summary>
        /// <param name="sender">textBoxes связанные с глубинной рекурсии</param>
        /// <param name="e">Информация о событии.</param>
        private void LostFocusDepth(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = "0";
            }
        }
        
        /// <summary>
        /// Обработчик события изменения текста в textBox, куда вводится глубина рекурсии дерева.
        /// </summary>
        /// <param name="sender">depthRecTextBoxTree.</param>
        /// <param name="e">Информация о событии.</param>
        private void TextChangedDepthTree(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string depth = textBox.Text;
            if (int.TryParse(depth, out int d) && d is >=0 and <=9)
            {
                if ((coefficientTextBox != null) && (angle1TextBox != null) && (angle2TextBox != null))
                {
                    if (isTypeFractalChoosen)
                    {
                        CreateTree(d, double.Parse(coefficientTextBox.Text), int.Parse(angle1TextBox.Text), int.Parse(angle2TextBox.Text));
                    }
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    textBox.Text = "0";
                    MessageBox.Show("Глубина рекурсии должна быть целым положительным числом в диапазоне [0,9]");
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текста в textBox, куда вводится глубина рекурсии кривой Коха.
        /// </summary>
        /// <param name="sender">depthRecTextBoxKoch.</param>
        /// <param name="e">Информация о событии.</param>
        private void TextChangedDepthKoch(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string depth = textBox.Text;
            if (int.TryParse(depth, out int d) && (d >= 0) && (d <= 4))
            {
                if (isTypeFractalChoosen)
                {
                    CreateKoch(d);
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    textBox.Text = "0";
                    MessageBox.Show("Глубина рекурсии должна быть целым числом в диапазоне [0,4]");
                }
            }
        }
        /// <summary>
        /// Обработчик события изменения текста в textBox, куда вводится глубина рекурсии ковра Серпинского .
        /// </summary>
        /// <param name="sender">depthRecTextBoxCarpet.</param>
        /// <param name="e">Информация о событии.</param>
        private void TextChangedDepthCarpet(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string depth = textBox.Text;
            if (int.TryParse(depth, out int d) && (d >= 0) && (d <= 4))
            {
                if (isTypeFractalChoosen)
                {
                    CreateCarpet(d);
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    textBox.Text = "0";
                    MessageBox.Show("Глубина рекурсии должна быть целым числом в диапазоне [0,4]");
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текста в textBox, куда вводится глубина рекурсии треугольника Серпинского.
        /// </summary>
        /// <param name="sender">depthRecTextBoxTriangle.</param>
        /// <param name="e">Информация о событии.</param>
        private void TextChangedDepthTriangle(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string depth = textBox.Text;
            if (int.TryParse(depth, out int d) && (d >= 0) && (d <= 6))
            {
                if (isTypeFractalChoosen)
                {
                    CreateTriangle(d);
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    textBox.Text = "0";
                    MessageBox.Show("Глубина рекурсии должна быть целым числом в диапазоне [0,6]!");
                }
            }
        }


        /// <summary>
        /// Обработчик события изменения текста в textBox, куда вводится глубина рекурсии множества Кантера.
        /// </summary>
        /// <param name="sender">depthRecTextBoxKanter.</param>
        /// <param name="e">Информация о событии.</param>
        private void TextChangedDepthKanter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string depth = textBox.Text;
            if (int.TryParse(depth, out int d) && (d >= 0) && (d <= 9))
            {
                if (distanceTextBox != null)
                {
                    if (isTypeFractalChoosen)
                    {
                        CreateKanter(d, int.Parse(distanceTextBox.Text));
                    }
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    textBox.Text = "0";
                    MessageBox.Show("Глубина рекурсии должна быть целым числом в диапазоне [0,9]");
                }
            }
        }

        /// <summary>
        /// Обработчик события потери фокуса.
        /// </summary>
        /// <param name="sender">coefficentTextBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void LostFocusCoefficent(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string coef = textBox.Text;
            if (double.TryParse(coef, out double c) && (c <= 1) && (c > 0) && textBox.Text.Length <= 3)
            {
                if ((depthRecTextBoxTree != null) && (angle1TextBox != null) && (angle2TextBox != null) && (c > 0) && (c <= 1) && isTypeFractalChoosen)
                {
                    CreateTree(int.Parse(depthRecTextBoxTree.Text), c, int.Parse(angle1TextBox.Text), int.Parse(angle2TextBox.Text));
                }
            }
            else
            {
                textBox.Text = "0,8";
                MessageBox.Show("Коэффицент должен быть целым положительным числом в диапазоне (0,1]!\nМаксимальное количество знаков после запятой - 1.");
                CreateTree(int.Parse(depthRecTextBoxTree.Text), 0.8, int.Parse(angle1TextBox.Text), int.Parse(angle2TextBox.Text));
            }
        }

        /// <summary>
        /// Обработчик события потери фокуса.
        /// </summary>
        /// <param name="sender">distanceTextBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void LostFocusDistance(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string distance = textBox.Text;
            if (int.TryParse(distance, out int d) && (d >= 1) && (d <= 10))
            {
                if (depthRecTextBoxKanter != null && isTypeFractalChoosen)
                {
                    CreateKanter(int.Parse(depthRecTextBoxKanter.Text), d);
                }
            }
            else
            {
                textBox.Text = "1";
                MessageBox.Show("Расстояние между отрезками может быть целым числом в диапозоне [1,10]!");
                CreateKanter(int.Parse(depthRecTextBoxKanter.Text), 1);
            }
        }

        /// <summary>
        /// Обработчик события потери фокуса.
        /// </summary>
        /// <param name="sender">angle1TextBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void LostFocusAngle1(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string angle1 = textBox.Text;
            if (int.TryParse(angle1, out int a1) && (a1 <= 180))
            {
                if ((depthRecTextBoxTree != null) && (coefficientTextBox != null) && (angle2TextBox != null) && (a1 >= 90) && (a1 <= 180) && isTypeFractalChoosen)
                {
                    CreateTree(int.Parse(depthRecTextBoxTree.Text), double.Parse(coefficientTextBox.Text), a1, int.Parse(angle2TextBox.Text));
                }
            }
            else
            {
                textBox.Text = "135";
                MessageBox.Show("Угол первого отрезка должен быть целым положительным числом в диапазоне [90,180]!");
                CreateTree(int.Parse(depthRecTextBoxTree.Text), double.Parse(coefficientTextBox.Text), 135, int.Parse(angle2TextBox.Text));
            }
        }


        /// <summary>
        /// Обработчик события потери фокуса.
        /// </summary>
        /// <param name="sender">angle2TextBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void LostFocusAngle2(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string angle2 = textBox.Text;
            if (int.TryParse(angle2, out int a2) && (a2 <= 270) && (a2 > 180))
            {
                if ((depthRecTextBoxTree != null) && (coefficientTextBox != null) && (angle1TextBox != null) && (a2 > 180) && (a2 <= 270) && isTypeFractalChoosen)
                {
                    CreateTree(int.Parse(depthRecTextBoxTree.Text), double.Parse(coefficientTextBox.Text), int.Parse(angle1TextBox.Text), a2);
                }
            }
            else
            {
                textBox.Text = "225";
                MessageBox.Show("Угол второго отрезка должен быть целым положительным числом в диапазоне (180,270]!");
                CreateTree(int.Parse(depthRecTextBoxTree.Text), double.Parse(coefficientTextBox.Text), int.Parse(angle1TextBox.Text), 225);
            }
        }

        /// <summary>
        /// Обработчик события закрытия раскрывающейся части поля со списком.
        /// </summary>
        /// <param name="sender">comboBox.</param>
        /// <param name="e">Информация о событии.</param>
        private void ComboBoxDropDownClosed(object sender, EventArgs e)
        {
            isTypeFractalChoosen = true;
            if (flag)
            {
                DoVisibility();
            }
            flag = true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Сохранить фрактал".
        /// </summary>
        /// <param name="sender">buttonSave.</param>
        /// <param name="e">Информация о событии.</param>
        private void ButtonSaveClick(object sender, EventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveImg = new Microsoft.Win32.SaveFileDialog();
            saveImg.DefaultExt = ".PNG";
            saveImg.Filter = "Image (.PNG)|*.PNG";
            if (saveImg.ShowDialog() == true)
            {
                SaveImage(saveImg.FileName);
            }
        }

        /// <summary>
        /// Сохранение содержимого canvas как png картинку. 
        /// </summary>
        /// <param name="fileName">Выбранный пользователем путь сохранения для картинки.</param>
        private void SaveImage(string fileName)
        {
            double dpi = 300;
            double scale = dpi / 96;
            Size size = canvas.RenderSize;
            RenderTargetBitmap image = new RenderTargetBitmap((int)(size.Width * scale), (int)(size.Height * scale), dpi, dpi, PixelFormats.Pbgra32);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            image.Render(canvas);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (FileStream file = File.Create(fileName))
            {
                encoder.Save(file);
            }
        }

        /// <summary>
        /// Делает обьекты на окне, связанные с деревом, видимыми.
        /// </summary>
        private void TreeIsSelected()
        {
            depthRec.Visibility = Visibility.Visible;
            depthRecTextBoxTree.Visibility = Visibility.Visible;
            depthRecTextBoxKoch.Visibility = Visibility.Hidden;
            depthRecTextBoxCarpet.Visibility = Visibility.Hidden;
            depthRecTextBoxTriangle.Visibility = Visibility.Hidden;
            depthRecTextBoxKanter.Visibility = Visibility.Hidden;
            coefficient.Visibility = Visibility.Visible;
            coefficientTextBox.Visibility = Visibility.Visible;
            distance.Visibility = Visibility.Hidden;
            distanceTextBox.Visibility = Visibility.Hidden;
            angle1.Visibility = Visibility.Visible;
            angle1TextBox.Visibility = Visibility.Visible;
            angle2.Visibility = Visibility.Visible;
            angle2TextBox.Visibility = Visibility.Visible;
            buttonSave.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Делает обьекты на окне, связанные с кривой Коха, видимыми.
        /// </summary>
        private void KochIsSelected()
        {
            depthRec.Visibility = Visibility.Visible;
            depthRecTextBoxTree.Visibility = Visibility.Hidden;
            depthRecTextBoxKoch.Visibility = Visibility.Visible;
            depthRecTextBoxCarpet.Visibility = Visibility.Hidden;
            depthRecTextBoxTriangle.Visibility = Visibility.Hidden;
            depthRecTextBoxKanter.Visibility = Visibility.Hidden;
            coefficient.Visibility = Visibility.Hidden;
            coefficientTextBox.Visibility = Visibility.Hidden;
            distance.Visibility = Visibility.Hidden;
            distanceTextBox.Visibility = Visibility.Hidden;
            angle1.Visibility = Visibility.Hidden;
            angle1TextBox.Visibility = Visibility.Hidden;
            angle2.Visibility = Visibility.Hidden;
            angle2TextBox.Visibility = Visibility.Hidden;
            buttonSave.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Делает обьекты на окне, связанные с ковром Серпинского, видимыми.
        /// </summary>
        private void CarpetIsSelected()
        {
            depthRec.Visibility = Visibility.Visible;
            depthRecTextBoxTree.Visibility = Visibility.Hidden;
            depthRecTextBoxKoch.Visibility = Visibility.Hidden;
            depthRecTextBoxCarpet.Visibility = Visibility.Visible;
            depthRecTextBoxTriangle.Visibility = Visibility.Hidden;
            depthRecTextBoxKanter.Visibility = Visibility.Hidden;
            coefficient.Visibility = Visibility.Hidden;
            coefficientTextBox.Visibility = Visibility.Hidden;
            distance.Visibility = Visibility.Hidden;
            distanceTextBox.Visibility = Visibility.Hidden;
            angle1.Visibility = Visibility.Hidden;
            angle1TextBox.Visibility = Visibility.Hidden;
            angle2.Visibility = Visibility.Hidden;
            angle2TextBox.Visibility = Visibility.Hidden;
            buttonSave.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Делает обьекты на окне, связанные с треугольником Серпинского, видимыми.
        /// </summary>
        private void TriangleIsSelected()
        {
            depthRec.Visibility = Visibility.Visible;
            depthRecTextBoxTree.Visibility = Visibility.Hidden;
            depthRecTextBoxKoch.Visibility = Visibility.Hidden;
            depthRecTextBoxCarpet.Visibility = Visibility.Hidden;
            depthRecTextBoxTriangle.Visibility = Visibility.Visible;
            depthRecTextBoxKanter.Visibility = Visibility.Hidden;
            coefficient.Visibility = Visibility.Hidden;
            coefficientTextBox.Visibility = Visibility.Hidden;
            distance.Visibility = Visibility.Hidden;
            distanceTextBox.Visibility = Visibility.Hidden;
            angle1.Visibility = Visibility.Hidden;
            angle1TextBox.Visibility = Visibility.Hidden;
            angle2.Visibility = Visibility.Hidden;
            angle2TextBox.Visibility = Visibility.Hidden;
            buttonSave.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Делает обьекты на окне, связанные с множеством Кантера, видимыми.
        /// </summary>
        private void KanterIsSelected()
        {
            depthRec.Visibility = Visibility.Visible;
            depthRecTextBoxTree.Visibility = Visibility.Hidden;
            depthRecTextBoxKoch.Visibility = Visibility.Hidden;
            depthRecTextBoxCarpet.Visibility = Visibility.Hidden;
            depthRecTextBoxTriangle.Visibility = Visibility.Hidden;
            depthRecTextBoxKanter.Visibility = Visibility.Visible;
            coefficient.Visibility = Visibility.Hidden;
            coefficientTextBox.Visibility = Visibility.Hidden;
            distance.Visibility = Visibility.Visible;
            distanceTextBox.Visibility = Visibility.Visible;
            angle1.Visibility = Visibility.Hidden;
            angle1TextBox.Visibility = Visibility.Hidden;
            angle2.Visibility = Visibility.Hidden;
            angle2TextBox.Visibility = Visibility.Hidden;
            buttonSave.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// В зависимости от выбранного элемента в comboBox делает видимыми обьекты на окне.
        /// </summary>
        private void DoVisibility()
        {
            if (Tree.IsSelected)
            {
                TreeIsSelected();
            }
            else if (Koсh.IsSelected)
            {
                KochIsSelected();
            }
            else if (Carpet.IsSelected)
            {
                CarpetIsSelected();
            }
            else if (Triangle.IsSelected)
            {
                TriangleIsSelected();
            }
            else if (Kanter.IsSelected)
            {
                KanterIsSelected();
            }
        }

        /// <summary>
        /// Обработчик события изменения размеров окна. 
        /// </summary>
        /// <param name="sender">window.</param>
        /// <param name="e">Информация о событии.</param>
        private void SizeChangedWindow(object sender, EventArgs e)
        {
            canvas.Height = window.Height;
            canvas.Width = window.Width / 2;
            buttonSave.Margin = new Thickness(0, 0, window.Width / 2, 0);
            if (WindowState == WindowState.Maximized)
            {
                canvas.Height = window.ActualHeight;
                canvas.Width = window.ActualWidth / 2;
                buttonSave.Margin = new Thickness(0, 0, window.ActualWidth / 2, 0);
            }
            if (isFractalCreated)
            {
                fractal.Render(canvas);
            }
        }
    }
}
