using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class EditorTiles : Form
    {
        public EditorTiles()
        {
            InitializeComponent();
        }

        private void EditorTiles_Load(object sender, EventArgs e)
        {
            //load in tiles
            foreach (KeyValuePair<string, Graphics.CTextureAtlas> kvp in Graphics.CTextures.textures)
            {
                if (kvp.Value.isTileSet)
                    comboBox1.Items.Add(kvp.Key);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemoryStream imageStream = new MemoryStream();
            Texture2D source = Graphics.CTextures.textures[comboBox1.Text].sourceImage;

            if (source.Width > 240)
            {
                hScrollBar1.Enabled = true;
                hScrollBar1.Maximum = source.Width;
                hScrollBar1.Minimum = 240;
            }

            if (source.Height > 200)
            {
                vScrollBar1.Enabled = true;
                vScrollBar1.Maximum = source.Height - 200;
                vScrollBar1.Minimum = 0;
            }

            source.SaveAsPng(imageStream, source.Width, source.Height);

            pictureBox1.Image = Image.FromStream(imageStream);

            imageStream.Close();
            source = null;
            imageStream = null;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Padding temp = pictureBox1.Padding;
            temp.Top =  vScrollBar1.Value;
            pictureBox1.Padding = temp;

            pictureBox1.Invalidate();
        }
    }
}
