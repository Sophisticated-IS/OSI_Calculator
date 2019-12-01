using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSI_Calculator_Exceptions
{
    public partial class Form_Calc : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);//какая-то winApi ф-ция для скрытия каретки
        public Form_Calc()
        {
            InitializeComponent();
        }
       
        bool cue = false; //*** глобальная перем-ая определяющая,что последнее нажатия было аримфм-ой операцией
        void textbox_figures_processing(char a, TextBox txtBox)//обраб-ка ввода новых цифр 
        {
            if (txtBox.Text == "0" || txtBox.Text == "-0" || cue)
            {
                txtBox.Text = a.ToString();
                cue = false;
            }
            else
            {
                txtBox.Text +=a.ToString();
            }
        }

        bool textbox_contains_symb (char symb,TextBox txtbox)
        {
           int index_of_symb = txtbox.Text.IndexOf(symb);
            
            if (index_of_symb<0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void textbox_del_symb (char symb, TextBox txtbox)
        {

            if (textbox_contains_symb(symb, txtbox))
            {
                int index_of_symb = txtbox.Text.IndexOf(symb);
                txtbox.Text = txtbox.Text.Remove(index_of_symb, 1);
            }
            else;
        }
        
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '1': button_1.PerformClick(); break;
                case '2': button_2.PerformClick(); break;
                case '3': button_3.PerformClick(); break;
                case '4': button_4.PerformClick(); break;
                case '5': button_5.PerformClick(); break;
                case '6': button_6.PerformClick(); break;
                case '7': button_7.PerformClick(); break;
                case '8': button_8.PerformClick(); break;
                case '9': button_9.PerformClick(); break;
                case '0': button_0.PerformClick(); break;
                            case (char)Keys.Back:  button_del.PerformClick(); break;
                            case (char)Keys.Enter: button_equal.PerformClick(); break;
                            case '=':              button_equal.PerformClick(); break;
                            case (char)Keys.Delete: button_ce.PerformClick(); break;
                            case ',': button_comma.PerformClick(); break;
                            case '*': button_multiply.PerformClick(); break;
                            case '/': button_divide.PerformClick(); break;
                            case '+': button_plus.PerformClick(); break;
                            case '-': button_minus.PerformClick(); break;




            }

        }

        private void button_1_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('1', textBox1);
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('2', textBox1);
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('3', textBox1);
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('4', textBox1);
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('5', textBox1);
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('6', textBox1);
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('7', textBox1);
        }

        private void button_8_Click(object sender, EventArgs e)
        {

            textbox_figures_processing('8', textBox1);
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('9', textBox1);
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            textbox_figures_processing('0', textBox1);
        }

        private void button_ce_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            HideCaret(textBox1.Handle);
        }

        private void button_plus_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
            textBox3.Text = "+";
        }

        private void button_minus_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
            textBox3.Text = "-";
        }

        private void button_multiply_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
            textBox3.Text = "×";
        }

        private void button_divide_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
            textBox3.Text = "÷"; 
        }

        private void button_c_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0"; 
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length==2 && textbox_contains_symb('-',textBox1) )
            {
                textBox1.Text = "0";
            }
            else
            {
                         if (textBox1.Text.Length==1)
                                    {
                                        textBox1.Text = "0";
                                    }
                                    else
                                    {
                                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                                    }
            }
        }

        private void button_chnge_sign_Click(object sender, EventArgs e)
        {
            if (textbox_contains_symb('-',textBox1))//удаляем минус
            {
                textbox_del_symb('-', textBox1);
            }
            else //добавляем минус
            {
                textBox1.Text = "-" + textBox1.Text;
            }
            textBox3.Focus();
            HideCaret(textBox3.Handle);
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            HideCaret(textBox2.Handle);
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            HideCaret(textBox3.Handle);
        }

        private void button_comma_Click(object sender, EventArgs e)
        {
            int index_comma = textBox1.Text.IndexOf(",");

            if (index_comma<0)//если запятой нет вообще
            {
                textBox1.Text += ",";
            }
            else//находим запятую и удаляем
            {
                if (textBox1.Text[index_comma-1]=='0')
                {
                    //убрав запятую число будет нач-ся с нуля след-но мы не можем её убрать тут
                }
                else
                textBox1.Text = textBox1.Text.Remove(index_comma, 1);
            }
        }

        void clear_TxtBoxes (params TextBox[] txt)//очищает n-oe кол-во текстбоксов
        {
            foreach (var item in txt)
            {
                item.Clear();
            }
        }

        void divide(TextBox txtBox1, TextBox txtBox2)
        {
            
            try
            {
               double a = Convert.ToDouble(txtBox2.Text);
               double b = Convert.ToDouble(txtBox1.Text);
                
                if (b==0)
                {
                    throw new Arithm_Funcs_Exception("Деление на нуль невозможно", a, b);
                }
                else
                {
                    txtBox1.Text =(a / b).ToString();
                }
            }
            catch (Arithm_Funcs_Exception exc)
            {
                textBox4.Text =$"Ошибка: {exc.Message}  \n Aргумент№1:{exc.Param1Property}  Aргумент№2:{exc.Param2Property}";
            }
            catch (Exception ex)//Ожид-ся переполнение 
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
            }
            
        }

        void multiply(TextBox txtBox1, TextBox txtBox2)
        {
            try
            {
                double a = Convert.ToDouble(txtBox2.Text);
                double b = Convert.ToDouble(txtBox1.Text);
                double c = checked(a * b);
                if (double.IsInfinity(c))
                {
                    throw new ArithmeticException("Произошло переполнение");
                }

                txtBox1.Text = c.ToString();
            }
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
        }

        void add (TextBox txtBox1, TextBox txtBox2)
        {
            try
            {
                double a = Convert.ToDouble(txtBox2.Text);
                double b = Convert.ToDouble(txtBox1.Text);
                txtBox1.Text = checked(a + b).ToString();
            }
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
            
        }

        void subtract(TextBox txtBox1, TextBox txtBox2)
        {
            try
            {
                double a = Convert.ToDouble(txtBox2.Text);
                double b = Convert.ToDouble(txtBox1.Text);
                txtBox1.Text = checked(a - b).ToString();
            }
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
        }
        private void button_equal_Click(object sender, EventArgs e)
        {
            switch (textBox3.Text)
            {
                case "÷": divide(textBox1, textBox2); 
                          clear_TxtBoxes(textBox3, textBox2); break;
                
                case "×": multiply(textBox1, textBox2);
                          clear_TxtBoxes(textBox3, textBox2); break;

                case "+": add(textBox1, textBox2);
                          clear_TxtBoxes(textBox3, textBox2); break;

                case "-": subtract(textBox1, textBox2);
                          clear_TxtBoxes(textBox3, textBox2); break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            cue = true;
        }

        private void button_arccos_Click(object sender, EventArgs e)
        {
            try
            {
             double a = Convert.ToDouble(textBox1.Text);
                if (a>1 || a<-1)
                {
                    throw new Arithm_Funcs_Exception($"Арккосинус не существует от", a);
                }
                else
                {
                    textBox1.Text = Math.Acos(a).ToString();
                    textBox3.Focus();
                    HideCaret(textBox3.Handle);
                }
            }

            catch  (Arithm_Funcs_Exception exc)
            {
               Console.WriteLine( exc.Param1Property);

                textBox4.Text = $"Ошибка: {exc.Message}  \n Aргумент№1:{exc.Param1Property}";
                textBox1.Text = "0";
            }
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
           
        }

        private void button_sqrt_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(textBox1.Text);
                if (a < 0)
                {
                    throw new Arithm_Funcs_Exception("Попытка взять корень из отриц-го числа", a);
                }
                else
                {
                    textBox1.Text = Math.Sqrt(a).ToString();
                    textBox3.Focus();
                    HideCaret(textBox3.Handle);
                }
            }
            catch (Arithm_Funcs_Exception exc)
            {
                textBox4.Text = $"Ошибка: {exc.Message}  \n Aргумент№1:{exc.Param1Property}";
                textBox1.Text = "0";
            }
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
        }
        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }
        private void button_tg_Click(object sender, EventArgs e)
        {
            
           

            try
            {
                double a = ToRadians(Convert.ToDouble(textBox1.Text));//Перевод в градусы 
                
                long discharge = (long)Convert.ToDouble(textBox1.Text) / 180;
                double left_p = Convert.ToDouble(textBox1.Text) / 180.0 - discharge;
                double right_p = 90.0 / 180.0 ;

                if ( (left_p -right_p) < 0.0000000000000000000000000000001)
                {
                    throw new Arithm_Funcs_Exception("Не существует тангенс от ", 90);
                }
                else
                    if (Convert.ToDouble(textBox1.Text)%180.0==0.0)
                    {
                        textBox1.Text = "0";
                    }
                     else
                     {
                        textBox1.Text = Math.Tan(a).ToString();
                        textBox3.Focus();
                        HideCaret(textBox3.Handle);
                     }
            }
                
            
            catch   (Arithm_Funcs_Exception exc)
            {
                textBox4.Text = $"Ошибка: {exc.Message}  \n {exc.Param1Property} градусов";
                textBox1.Text = "0";
            }
            
            catch (Exception ex)
            {
                textBox4.Text = $"Ошибка: {ex.Message}";
                textBox1.Text = "0";
            }
        }

    }
}