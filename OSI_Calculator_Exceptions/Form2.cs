using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace OSI_Calculator_Exceptions
{
    public partial class Form_expression : Form
    {
        public Form_expression()
        {
            InitializeComponent();
        }
        //Объявляем приложение       
        Excel.Application ex;
  
        private void button2_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
            }
        }

        List<char> names = new List<char>();
        List<int> i_indx = new List<int> ();
        List<int> j_indx = new List<int> ();
        public string RangeAddress(Excel.Range rng)
        {
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1,
                   Type.Missing, Type.Missing);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try//Excel creation
            {
                ex = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (global::System.Exception)
            {
                throw new Exception("Что-то пошло не так с Excel");
            }

            //Отобразить Excel
            ex.Visible = true;
            //Количество листов в рабочей книге
            ex.SheetsInNewWorkbook = 2;
            //Добавить рабочую книгу
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Вычисление_выражения";


            string[] s = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int i=1;int j=1;//индексы в  EXCEL
            foreach (string item in s)//разделение на строчки
            {
                string[] s2 = item.Split(' ');
                if (item.Contains("PRINT"))
                {
                    s2[0] = "";//удаление PRINT
                    string unity="";
                    foreach (var connect in s2)
                    {
                        if (string.IsNullOrEmpty(connect))
                        {
                            //ничего не делаем
                        }
                        else
                        {
                            string tmp = connect.Trim();
                            unity = unity + tmp + " "; 
                        }
                    }
                    //foreach (var print in s2)
                    //{
                    //string for_print = print;
                    string for_print = unity; 
                    char del = for_print[1];
                    if (for_print[0]=='\"')
                        {
                            for_print = for_print.Remove(0, 1);//удаление первой !"!
                            for_print = for_print.Remove( for_print.IndexOf("\""),for_print.Length - 1 - for_print.IndexOf("\""));//отсечение всего после второй !"!
                            sheet.Cells[1, 3] = for_print; 
                        //}
                    }
                }
                else
                    if (item.Contains("(")|| item.Contains(")") || item.Contains("-") 
                    || item.Contains("+") || item.Contains("*") || item.Contains("/"))//Задание формулы в Excel
                     {
                            //Одна ячейка как диапазон
                            Excel.Range r = sheet.Cells[2, 3] as Excel.Range;
                            //Оформления
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = true;
                            r.Font.Color = ColorTranslator.ToOle(Color.Blue);
                        string only_expression="";
                            if(item.Length>2)
                            {
                             only_expression = item.Remove(0, 2);
                            }
                        string formula_with_adress = only_expression;
                        foreach (var ch in only_expression)
                         {
                                if (ch >= 'A' && ch <= 'Z')
                                {
                                int ind_i = i_indx.ElementAt(names.IndexOf(ch));  
                                int ind_j = j_indx.ElementAt(names.IndexOf(ch));
                                Excel.Range formulaRange = sheet.Cells[ind_i,ind_j];
                                string address = RangeAddress(sheet.Cells[ind_i, ind_j]);//sheet.Cells[1, 1].Adress();  //formulaRange.Range[sheet.Cells[ind_i, ind_j]]; //formulaRange.Cells[ind_i, ind_j];
                                formula_with_adress = formula_with_adress.Replace(ch.ToString(), address);
                                }
                         }
                                //Задаем формулу суммы
                                r.Formula = String.Format("="+formula_with_adress);//поменяли имена на адреса и добавили обозначение формулы - '='
                }
                    else
                        foreach (var item2 in s2)//разделение на отедльные выражения
                        {
                            if (item2[0] >= 'A' && item2[0] <= 'Z' && item2[1] == '=')
                            {
                            string val = item2.Remove(0, 2);//Удаление имени перем-ой и знака равно
                            sheet.Cells[i, j] = String.Format("{0}",val);
                            names.Add(item2[0]);//добавление имен переменных в список
                            i_indx.Add(i); j_indx.Add(j);
                            j++;
                            if (j==3)
                            {
                                j = j-2;//в два столбика записывать будем
                                i++;//переход на след строку
                            }
                            
                        }

                        }
            }

        }

        private void Form_expression_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ex is null)
            {

            }
            else
            {
                ex.Quit();
            }
            
        }
    }
}
