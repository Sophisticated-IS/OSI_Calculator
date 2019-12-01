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
    public partial class MSDI_Form : Form
    {  
        Form_Calc f_calc;
        Form_expression f_exp;
        bool first_time = true;
        public MSDI_Form()
        {
            InitializeComponent();

            f_calc = new Form_Calc();
            f_calc.MdiParent = this;
            f_calc.Show();

            f_exp = new Form_expression();
            f_exp.MdiParent = this;
        }
      

        private void калькуляторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_calc.Activate();
           
        }
    
        private void вычислениеВыраженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (first_time)
            {
                first_time = false;

              
                f_exp.Show(); 
                f_exp.Location = new Point(0, 0);

            }
      
            f_exp.Activate();
        }
    }
}
