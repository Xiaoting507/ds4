namespace Parcial_2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            lblFahrenheit = new Label();
            txtFahrenheit = new TextBox();
            lblFahrenheitResult = new Label();
            btnConvertirF = new Button();
            lblCelsius = new Label();
            txtCelsius = new TextBox();
            lblCelsiusResult = new Label();
            btnConvertirC = new Button();
            lblKelvin = new Label();
            txtKelvin = new TextBox();
            lblKelvinResult = new Label();
            btnConvertirK = new Button();
            lblTituloFahrenheit = new Label();
            lblTituloCelsius = new Label();
            lblTituloKelvin = new Label();
            dgvHistorial = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblFahrenheit
            // 
            lblFahrenheit.AutoSize = true;
            lblFahrenheit.Font = new Font("Arial Narrow", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFahrenheit.Location = new Point(40, 85);
            lblFahrenheit.Margin = new Padding(4, 0, 4, 0);
            lblFahrenheit.Name = "lblFahrenheit";
            lblFahrenheit.Size = new Size(69, 20);
            lblFahrenheit.TabIndex = 0;
            lblFahrenheit.Text = "Fahrenheit";
            // 
            // txtFahrenheit
            // 
            txtFahrenheit.Font = new Font("Microsoft Sans Serif", 10F);
            txtFahrenheit.Location = new Point(173, 77);
            txtFahrenheit.Margin = new Padding(4, 5, 4, 5);
            txtFahrenheit.Name = "txtFahrenheit";
            txtFahrenheit.Size = new Size(265, 26);
            txtFahrenheit.TabIndex = 1;
            // 
            // lblFahrenheitResult
            // 
            lblFahrenheitResult.BackColor = Color.White;
            lblFahrenheitResult.BorderStyle = BorderStyle.FixedSingle;
            lblFahrenheitResult.Font = new Font("Microsoft Sans Serif", 10F);
            lblFahrenheitResult.Location = new Point(562, 77);
            lblFahrenheitResult.Margin = new Padding(4, 0, 4, 0);
            lblFahrenheitResult.Name = "lblFahrenheitResult";
            lblFahrenheitResult.Size = new Size(199, 34);
            lblFahrenheitResult.TabIndex = 2;
            lblFahrenheitResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnConvertirF
            // 
            btnConvertirF.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnConvertirF.Location = new Point(467, 74);
            btnConvertirF.Margin = new Padding(4, 5, 4, 5);
            btnConvertirF.Name = "btnConvertirF";
            btnConvertirF.Size = new Size(80, 42);
            btnConvertirF.TabIndex = 3;
            btnConvertirF.Text = "→";
            btnConvertirF.UseVisualStyleBackColor = true;
            btnConvertirF.Click += btnConvertirF_Click;
            // 
            // lblCelsius
            // 
            lblCelsius.AutoSize = true;
            lblCelsius.Font = new Font("Arial Narrow", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCelsius.Location = new Point(40, 162);
            lblCelsius.Margin = new Padding(4, 0, 4, 0);
            lblCelsius.Name = "lblCelsius";
            lblCelsius.Size = new Size(50, 20);
            lblCelsius.TabIndex = 4;
            lblCelsius.Text = "Celsius";
            // 
            // txtCelsius
            // 
            txtCelsius.Font = new Font("Microsoft Sans Serif", 10F);
            txtCelsius.Location = new Point(173, 154);
            txtCelsius.Margin = new Padding(4, 5, 4, 5);
            txtCelsius.Name = "txtCelsius";
            txtCelsius.Size = new Size(265, 26);
            txtCelsius.TabIndex = 5;
            // 
            // lblCelsiusResult
            // 
            lblCelsiusResult.BackColor = Color.White;
            lblCelsiusResult.BorderStyle = BorderStyle.FixedSingle;
            lblCelsiusResult.Font = new Font("Microsoft Sans Serif", 10F);
            lblCelsiusResult.Location = new Point(770, 78);
            lblCelsiusResult.Margin = new Padding(4, 0, 4, 0);
            lblCelsiusResult.Name = "lblCelsiusResult";
            lblCelsiusResult.Size = new Size(199, 34);
            lblCelsiusResult.TabIndex = 6;
            lblCelsiusResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnConvertirC
            // 
            btnConvertirC.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnConvertirC.Location = new Point(467, 151);
            btnConvertirC.Margin = new Padding(4, 5, 4, 5);
            btnConvertirC.Name = "btnConvertirC";
            btnConvertirC.Size = new Size(80, 42);
            btnConvertirC.TabIndex = 7;
            btnConvertirC.Text = "→";
            btnConvertirC.UseVisualStyleBackColor = true;
            btnConvertirC.Click += btnConvertirC_Click;
            // 
            // lblKelvin
            // 
            lblKelvin.AutoSize = true;
            lblKelvin.Font = new Font("Arial Narrow", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblKelvin.Location = new Point(40, 238);
            lblKelvin.Margin = new Padding(4, 0, 4, 0);
            lblKelvin.Name = "lblKelvin";
            lblKelvin.Size = new Size(43, 20);
            lblKelvin.TabIndex = 8;
            lblKelvin.Text = "Kelvin";
            // 
            // txtKelvin
            // 
            txtKelvin.Font = new Font("Microsoft Sans Serif", 10F);
            txtKelvin.Location = new Point(173, 231);
            txtKelvin.Margin = new Padding(4, 5, 4, 5);
            txtKelvin.Name = "txtKelvin";
            txtKelvin.Size = new Size(265, 26);
            txtKelvin.TabIndex = 9;
            // 
            // lblKelvinResult
            // 
            lblKelvinResult.BackColor = Color.White;
            lblKelvinResult.BorderStyle = BorderStyle.FixedSingle;
            lblKelvinResult.Font = new Font("Microsoft Sans Serif", 10F);
            lblKelvinResult.Location = new Point(977, 78);
            lblKelvinResult.Margin = new Padding(4, 0, 4, 0);
            lblKelvinResult.Name = "lblKelvinResult";
            lblKelvinResult.Size = new Size(199, 34);
            lblKelvinResult.TabIndex = 10;
            lblKelvinResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnConvertirK
            // 
            btnConvertirK.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnConvertirK.Location = new Point(467, 228);
            btnConvertirK.Margin = new Padding(4, 5, 4, 5);
            btnConvertirK.Name = "btnConvertirK";
            btnConvertirK.Size = new Size(80, 42);
            btnConvertirK.TabIndex = 11;
            btnConvertirK.Text = "→";
            btnConvertirK.UseVisualStyleBackColor = true;
            btnConvertirK.Click += btnConvertirK_Click;
            // 
            // lblTituloFahrenheit
            // 
            lblTituloFahrenheit.BackColor = Color.Transparent;
            lblTituloFahrenheit.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloFahrenheit.Location = new Point(561, 23);
            lblTituloFahrenheit.Margin = new Padding(4, 0, 4, 0);
            lblTituloFahrenheit.Name = "lblTituloFahrenheit";
            lblTituloFahrenheit.Size = new Size(200, 38);
            lblTituloFahrenheit.TabIndex = 12;
            lblTituloFahrenheit.Text = "Fahrenheit";
            lblTituloFahrenheit.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTituloCelsius
            // 
            lblTituloCelsius.BackColor = Color.Transparent;
            lblTituloCelsius.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloCelsius.Location = new Point(769, 23);
            lblTituloCelsius.Margin = new Padding(4, 0, 4, 0);
            lblTituloCelsius.Name = "lblTituloCelsius";
            lblTituloCelsius.Size = new Size(200, 38);
            lblTituloCelsius.TabIndex = 13;
            lblTituloCelsius.Text = "Celsius";
            lblTituloCelsius.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTituloKelvin
            // 
            lblTituloKelvin.BackColor = Color.Transparent;
            lblTituloKelvin.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloKelvin.Location = new Point(976, 23);
            lblTituloKelvin.Margin = new Padding(4, 0, 4, 0);
            lblTituloKelvin.Name = "lblTituloKelvin";
            lblTituloKelvin.Size = new Size(200, 38);
            lblTituloKelvin.TabIndex = 14;
            lblTituloKelvin.Text = "Kelvin";
            lblTituloKelvin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.BackgroundColor = Color.White;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Location = new Point(133, 315);
            dgvHistorial.Margin = new Padding(4, 5, 4, 5);
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new Size(915, 194);
            dgvHistorial.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1191, 550);
            Controls.Add(dgvHistorial);
            Controls.Add(lblTituloKelvin);
            Controls.Add(lblTituloCelsius);
            Controls.Add(lblTituloFahrenheit);
            Controls.Add(btnConvertirK);
            Controls.Add(lblKelvinResult);
            Controls.Add(txtKelvin);
            Controls.Add(lblKelvin);
            Controls.Add(btnConvertirC);
            Controls.Add(lblCelsiusResult);
            Controls.Add(txtCelsius);
            Controls.Add(lblCelsius);
            Controls.Add(btnConvertirF);
            Controls.Add(lblFahrenheitResult);
            Controls.Add(txtFahrenheit);
            Controls.Add(lblFahrenheit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Conversor Dinero";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFahrenheit;
        private System.Windows.Forms.TextBox txtFahrenheit;
        private System.Windows.Forms.Label lblFahrenheitResult;
        private System.Windows.Forms.Button btnConvertirF;
        private System.Windows.Forms.Label lblCelsius;
        private System.Windows.Forms.TextBox txtCelsius;
        private System.Windows.Forms.Label lblCelsiusResult;
        private System.Windows.Forms.Button btnConvertirC;
        private System.Windows.Forms.Label lblKelvin;
        private System.Windows.Forms.TextBox txtKelvin;
        private System.Windows.Forms.Label lblKelvinResult;
        private System.Windows.Forms.Button btnConvertirK;
        private System.Windows.Forms.Label lblTituloFahrenheit;
        private System.Windows.Forms.Label lblTituloCelsius;
        private System.Windows.Forms.Label lblTituloKelvin;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}