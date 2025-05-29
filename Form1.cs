using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMember nm = new NewMember();
            nm.Show();
        }

        private void newStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff ns = new Staff();
            ns.Show();
        }

        private void equipmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Equipments equipments = new Equipments();
            equipments.Show();
        }

        private void searchMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSuplement aS = new addSuplement();
            aS.Show();
        }

        private void viewMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewMembers vM = new viewMembers();
            vM.Show();
        }

        private void viewStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewStaff vS = new viewStaff();
            vS.Show();
        }

        private void viewEquipmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewEquipmentscs vE = new viewEquipmentscs();
            vE.Show();
        }

        private void viewSupplementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewSupplements vS = new viewSupplements();
            vS.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                loginForm lg = new loginForm();
                lg.Show();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application?", "Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
