using System;
using System.Windows.Forms;

namespace Practica_8
{
    public partial class frmPrincipal : Form
    {
        ConexionBD Conectado = new ConexionBD();
        public frmPrincipal()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RbInsertar.Checked = true;
            Conectado.cargarPersonas(DgvPersonas);
        }

        private void CmdAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Conectado.Insertar(TxtNombre.Text, TxtApellidos.Text, DtpFecha.Text));
            Conectado.cargarPersonas(DgvPersonas);
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
        }

        private void CmdModificar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Conectado.atualizar(Convert.ToInt32(TxtId.Text), TxtNombre.Text, TxtApellidos.Text, DtpFecha.Text));
            Conectado.cargarPersonas(DgvPersonas);
            TxtId.Text = "";
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
        }

        private void RbModificar_CheckedChanged(object sender, EventArgs e)
        {
            CmdModificar.Enabled = true;
            CmdEliminar.Enabled = false;
            CmdAgregar.Enabled = false;
            TxtId.Enabled = true;
            TxtNombre.Enabled = true;
            TxtApellidos.Enabled = true;
            DtpFecha.Enabled = true;
        }

        private void RbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            TxtId.Enabled = true;
            TxtNombre.Enabled = false;
            TxtApellidos.Enabled = false;
            DtpFecha.Enabled = false;
            CmdEliminar.Enabled = true;
            CmdAgregar.Enabled = false;
            CmdModificar.Enabled = false;
        }

        private void RbInsertar_CheckedChanged(object sender, EventArgs e)
        {
            TxtId.Enabled = true;
            TxtNombre.Enabled = true;
            TxtApellidos.Enabled = true;
            DtpFecha.Enabled = true;
            CmdAgregar.Enabled = true;
            CmdEliminar.Enabled = false;
            CmdModificar.Enabled = false;
        }

        private void CmdEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Conectado.Eliminar(Convert.ToInt32(TxtId.Text)));
            Conectado.cargarPersonas(DgvPersonas);
        }
    }
}
