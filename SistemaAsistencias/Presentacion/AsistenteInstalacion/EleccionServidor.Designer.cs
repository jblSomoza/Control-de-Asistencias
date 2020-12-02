namespace SistemaAsistencias.Logica.AsistenteInstalacion
{
    partial class EleccionServidor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EleccionServidor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Label4 = new System.Windows.Forms.Label();
            this.BtnRemoto = new System.Windows.Forms.Button();
            this.BtnPrincipal = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1346, 100);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::SistemaAsistencias.Properties.Resources.strix_owl;
            this.pictureBox1.Location = new System.Drawing.Point(226, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "SISTEMA DE ASISTENCIA STRIX OWL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.Label4);
            this.Panel4.Controls.Add(this.BtnRemoto);
            this.Panel4.Controls.Add(this.BtnPrincipal);
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.label2);
            this.Panel4.Controls.Add(this.panel2);
            this.Panel4.Controls.Add(this.panel3);
            this.Panel4.Controls.Add(this.pictureBox2);
            this.Panel4.Location = new System.Drawing.Point(-58, 96);
            this.Panel4.Margin = new System.Windows.Forms.Padding(4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1463, 539);
            this.Panel4.TabIndex = 622;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Label4.ForeColor = System.Drawing.Color.White;
            this.Label4.Location = new System.Drawing.Point(1036, 390);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(424, 127);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Se Conecta a la Computadora Principal siempre y cuando la Principal este Encendid" +
    "a";
            // 
            // BtnRemoto
            // 
            this.BtnRemoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.BtnRemoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRemoto.FlatAppearance.BorderSize = 0;
            this.BtnRemoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRemoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.BtnRemoto.ForeColor = System.Drawing.Color.White;
            this.BtnRemoto.Location = new System.Drawing.Point(71, 390);
            this.BtnRemoto.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRemoto.Name = "BtnRemoto";
            this.BtnRemoto.Size = new System.Drawing.Size(463, 103);
            this.BtnRemoto.TabIndex = 609;
            this.BtnRemoto.Text = "Punto de Control";
            this.BtnRemoto.UseVisualStyleBackColor = false;
            this.BtnRemoto.Click += new System.EventHandler(this.BtnRemoto_Click);
            // 
            // BtnPrincipal
            // 
            this.BtnPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.BtnPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrincipal.FlatAppearance.BorderSize = 0;
            this.BtnPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.BtnPrincipal.ForeColor = System.Drawing.Color.White;
            this.BtnPrincipal.Location = new System.Drawing.Point(71, 139);
            this.BtnPrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPrincipal.Name = "BtnPrincipal";
            this.BtnPrincipal.Size = new System.Drawing.Size(463, 103);
            this.BtnPrincipal.TabIndex = 608;
            this.BtnPrincipal.Text = "Principal";
            this.BtnPrincipal.UseVisualStyleBackColor = false;
            this.BtnPrincipal.Click += new System.EventHandler(this.BtnPrincipal_Click);
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Label9.ForeColor = System.Drawing.Color.White;
            this.Label9.Location = new System.Drawing.Point(905, 135);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(516, 127);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "Esta Computadora debe estar Encendida para que las Computadoras\r\nSecundarias se C" +
    "onecten. Si se apaga no podran conectarse.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(304, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(586, 58);
            this.label2.TabIndex = 605;
            this.label2.Text = "¿Esta Computadora es?";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Location = new System.Drawing.Point(893, 112);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(4, 166);
            this.panel2.TabIndex = 606;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Location = new System.Drawing.Point(1015, 358);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(4, 171);
            this.panel3.TabIndex = 607;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(459, 112);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(629, 417);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 604;
            this.pictureBox2.TabStop = false;
            // 
            // EleccionServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1346, 731);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.panel1);
            this.Name = "EleccionServidor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EleccionServidor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);


        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button BtnRemoto;
        internal System.Windows.Forms.Button BtnPrincipal;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.PictureBox pictureBox2;
    }
}