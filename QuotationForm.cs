using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNT_quotation.Data;
using UNT_Quotation.Views;
using UNTQuotation.Models;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using UNT_Quotation.Funtion;

namespace UNTQuotation
{

    public partial class QuotationForm : Form
    {
        private int currentRow = 0;
        public QuotationForm()
        {
            InitializeComponent();
        }
        Customer quotations;
        private void Quotation_Load(object sender, EventArgs e)
        {
            // Regular font for data
            dgvQuotation.DefaultCellStyle.Font = new Font("Arial", 14);
            ForeColor = Color.Black; // Text color
            BackColor = Color.LightGray; // Background color
            Database.connetion();
            quotations = new Customer();
            quotations.SetQuotation(cboQuoted);
           quotations.SetService(cboDsSevice);
            cbovalitity.Items.Add("45 Day");
            cbovalitity.Items.Add("75 Day");
            cbovalitity.Items.Add("100 Day");
           //quotations.loadData(dgvQuotation);




        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboQuoted_KeyPress(object sender, KeyPressEventArgs e)
        
        {
            quotations = new Customer();   
          
                quotations.QuotedName = cboQuoted.Text;
                quotations.GetQuotation( txtAddress, txtAttention);
            
        }

        private void addQuotedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerForm quotation = new CustomerForm();
            quotation.ShowDialog();
            cboQuoted.Items.Clear();
            quotations.SetQuotation(cboQuoted);

        }

    

        private void dobValidity_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //printDocument1.Print();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Bitmap pr = new Bitmap(this.dgvQuotation.Width, this.dgvQuotation.Height);
            //dgvQuotation.DrawToBitmap(pr, new Rectangle(0, 0, this.dgvQuotation.Width, this.dgvQuotation.Height));
            //e.Graphics.DrawImage(pr, 10, 10);
            int startX = 10;
            int startY = 10;
            int offsetY = 40;

            // Header printing
            for (int j = 0; j < dgvQuotation.Columns.Count; j++)
            {
                e.Graphics.DrawString(dgvQuotation.Columns[j].HeaderText, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, startX, startY);
                startX += dgvQuotation.Columns[j].Width;
            }

            startX = 10;
            startY += offsetY;

            // Rows printing
            while (currentRow < dgvQuotation.Rows.Count)
            {
                for (int j = 0; j < dgvQuotation.Columns.Count; j++)
                {
                    if (dgvQuotation.Rows[currentRow].Cells[j].Value != null)
                    {
                        e.Graphics.DrawString(dgvQuotation.Rows[currentRow].Cells[j].Value.ToString(), new Font("Arial", 10), Brushes.Black, startX, startY);
                    }
                    startX += dgvQuotation.Columns[j].Width;
                }

                startX = 10;
                startY += offsetY;
                currentRow++;

                if (startY > e.MarginBounds.Height)
                {
                    e.HasMorePages = true; // Indicate that more pages are needed
                    return;
                }
            }

            // No more rows to print
            e.HasMorePages = false;
            currentRow = 0;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CustomerForm quotationForm = new CustomerForm();
            quotationForm.ShowDialog();
                  quotations = new Customer();
            cboQuoted.Items.Clear();
            quotations.SetQuotation(cboQuoted);
            
        }

    
        private void dgvQuotation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            quotations = new Customer();
          //  quotations.TranferData(dgvQuotation);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomerForm quotationForm = new CustomerForm();
            quotationForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            quotations = new Customer();
            quotations.PrintReportSale(dgvQuotation);
        }
    }
}
