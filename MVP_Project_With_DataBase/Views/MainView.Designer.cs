namespace MVP_Project_With_DataBase.Views
{
    partial class MainView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPersonPets = new System.Windows.Forms.Button();
            this.btnPersons = new System.Windows.Forms.Button();
            this.btnPets = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.btnPersonPets);
            this.panel1.Controls.Add(this.btnPersons);
            this.panel1.Controls.Add(this.btnPets);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 450);
            this.panel1.TabIndex = 0;
            // 
            // btnPersonPets
            // 
            this.btnPersonPets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPersonPets.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPersonPets.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPersonPets.Location = new System.Drawing.Point(0, 210);
            this.btnPersonPets.Name = "btnPersonPets";
            this.btnPersonPets.Size = new System.Drawing.Size(200, 41);
            this.btnPersonPets.TabIndex = 2;
            this.btnPersonPets.Text = "Persons and Pets";
            this.btnPersonPets.UseVisualStyleBackColor = true;
            // 
            // btnPersons
            // 
            this.btnPersons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPersons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPersons.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPersons.Location = new System.Drawing.Point(0, 163);
            this.btnPersons.Name = "btnPersons";
            this.btnPersons.Size = new System.Drawing.Size(200, 41);
            this.btnPersons.TabIndex = 1;
            this.btnPersons.Text = "Persons";
            this.btnPersons.UseVisualStyleBackColor = true;
            // 
            // btnPets
            // 
            this.btnPets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPets.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPets.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPets.Location = new System.Drawing.Point(0, 116);
            this.btnPets.Name = "btnPets";
            this.btnPets.Size = new System.Drawing.Size(200, 41);
            this.btnPets.TabIndex = 0;
            this.btnPets.Text = "Pets";
            this.btnPets.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.Text = "MainView";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPets;
        private System.Windows.Forms.Button btnPersons;
        private System.Windows.Forms.Button btnPersonPets;
    }
}