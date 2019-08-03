using System;
using System.Windows.Forms;

namespace codeBank
    {
    public partial class frmAyar : Form
        {
        public frmAyar()
            {
            InitializeComponent();
            }

        Properties.Settings Ayar = Properties.Settings.Default;
        private void frmAyar_Load(object sender, EventArgs e)
            {

            cbKategoriAcik.Checked = Ayar.KategorilerAcik;

            }

        private void btnKaydet_Click(object sender, EventArgs e)
            {
            try
                {

                //if (errorProvider1.HataKontrol(tp1) || errorProvider1.HataKontrol(tp3))
                //    return;


                Ayar.KategorilerAcik = cbKategoriAcik.Checked;

                Ayar.Save();

                this.Close();

                }
            catch (Exception ex)
                {
                MessageBox.Show("Hata Oluştu  : " + ex.ToString());
                }
            }


        }
    }
