using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class EditorComponents : Form
    {
        public EditorComponents()
        {
            InitializeComponent();
        }

        private void btnPlayTest_Click(object sender, EventArgs e)
        {
            Gears.Cloud.Master.Push(new usr.local.PlayableState());
        }

        private void EditorComponents_Load(object sender, EventArgs e)
        {

        }
    }
}
