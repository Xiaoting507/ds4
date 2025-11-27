namespace Proyecto1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txbResultado = new TextBox();
            button1 = new Button();
            btnPotencia = new Button();
            btnRaiz = new Button();
            btnDividir = new Button();
            btnProd = new Button();
            btn9 = new Button();
            btn8 = new Button();
            btn7 = new Button();
            btnRestar = new Button();
            btn6 = new Button();
            btn5 = new Button();
            btn4 = new Button();
            btnSumar = new Button();
            btn3 = new Button();
            btn2 = new Button();
            btn1 = new Button();
            btnResultado = new Button();
            btnDecimal = new Button();
            btn0 = new Button();
            btnSigno = new Button();
            btnEliminar = new Button();
            btnClear = new Button();
            btnClearE = new Button();
            button11 = new Button();
            txbCarry = new TextBox();
            SuspendLayout();
            // 
            // txbResultado
            // 
            txbResultado.Font = new Font("Bahnschrift", 21.9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txbResultado.Location = new Point(13, 76);
            txbResultado.Margin = new Padding(2, 2, 2, 2);
            txbResultado.Multiline = true;
            txbResultado.Name = "txbResultado";
            txbResultado.ReadOnly = true;
            txbResultado.RightToLeft = RightToLeft.No;
            txbResultado.Size = new Size(943, 100);
            txbResultado.TabIndex = 0;
            txbResultado.Text = "0";
            txbResultado.TextAlign = HorizontalAlignment.Right;
            txbResultado.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Bahnschrift", 12F);
            button1.Location = new Point(17, 422);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(230, 90);
            button1.TabIndex = 1;
            button1.Text = "π";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnPI_Click;
            // 
            // btnPotencia
            // 
            btnPotencia.Font = new Font("Bahnschrift", 12F);
            btnPotencia.Location = new Point(255, 422);
            btnPotencia.Margin = new Padding(2, 2, 2, 2);
            btnPotencia.Name = "btnPotencia";
            btnPotencia.Size = new Size(230, 90);
            btnPotencia.TabIndex = 2;
            btnPotencia.Text = "^";
            btnPotencia.UseVisualStyleBackColor = true;
            btnPotencia.Click += btnPotencia_Click_1;
            // 
            // btnRaiz
            // 
            btnRaiz.Font = new Font("Bahnschrift", 12F);
            btnRaiz.Location = new Point(491, 422);
            btnRaiz.Margin = new Padding(2, 2, 2, 2);
            btnRaiz.Name = "btnRaiz";
            btnRaiz.Size = new Size(230, 90);
            btnRaiz.TabIndex = 3;
            btnRaiz.Text = "√";
            btnRaiz.UseVisualStyleBackColor = true;
            btnRaiz.Click += btnRaiz_Click;
            // 
            // btnDividir
            // 
            btnDividir.Font = new Font("Bahnschrift", 12F);
            btnDividir.Location = new Point(727, 422);
            btnDividir.Margin = new Padding(2, 2, 2, 2);
            btnDividir.Name = "btnDividir";
            btnDividir.Size = new Size(230, 90);
            btnDividir.TabIndex = 4;
            btnDividir.Text = "/";
            btnDividir.UseVisualStyleBackColor = true;
            btnDividir.Click += btnDividir_Click;
            // 
            // btnProd
            // 
            btnProd.Font = new Font("Bahnschrift", 12F);
            btnProd.Location = new Point(727, 519);
            btnProd.Margin = new Padding(2, 2, 2, 2);
            btnProd.Name = "btnProd";
            btnProd.Size = new Size(230, 90);
            btnProd.TabIndex = 8;
            btnProd.Text = "x";
            btnProd.UseVisualStyleBackColor = true;
            btnProd.Click += btnProd_Click;
            // 
            // btn9
            // 
            btn9.Font = new Font("Bahnschrift", 12F);
            btn9.Location = new Point(491, 519);
            btn9.Margin = new Padding(2, 2, 2, 2);
            btn9.Name = "btn9";
            btn9.Size = new Size(230, 90);
            btn9.TabIndex = 7;
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = true;
            btn9.Click += btn9_Click;
            // 
            // btn8
            // 
            btn8.Font = new Font("Bahnschrift", 12F);
            btn8.Location = new Point(255, 519);
            btn8.Margin = new Padding(2, 2, 2, 2);
            btn8.Name = "btn8";
            btn8.Size = new Size(230, 90);
            btn8.TabIndex = 6;
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = true;
            btn8.Click += btn8_Click;
            // 
            // btn7
            // 
            btn7.Font = new Font("Bahnschrift", 12F);
            btn7.Location = new Point(17, 519);
            btn7.Margin = new Padding(2, 2, 2, 2);
            btn7.Name = "btn7";
            btn7.Size = new Size(230, 90);
            btn7.TabIndex = 5;
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = true;
            btn7.Click += btn7_Click;
            // 
            // btnRestar
            // 
            btnRestar.Font = new Font("Bahnschrift", 12F);
            btnRestar.Location = new Point(727, 615);
            btnRestar.Margin = new Padding(2, 2, 2, 2);
            btnRestar.Name = "btnRestar";
            btnRestar.Size = new Size(230, 90);
            btnRestar.TabIndex = 12;
            btnRestar.Text = "-";
            btnRestar.UseVisualStyleBackColor = true;
            btnRestar.Click += btnRestar_Click;
            // 
            // btn6
            // 
            btn6.Font = new Font("Bahnschrift", 12F);
            btn6.Location = new Point(491, 615);
            btn6.Margin = new Padding(2, 2, 2, 2);
            btn6.Name = "btn6";
            btn6.Size = new Size(230, 90);
            btn6.TabIndex = 11;
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = true;
            btn6.Click += btn6_Click;
            // 
            // btn5
            // 
            btn5.Font = new Font("Bahnschrift", 12F);
            btn5.Location = new Point(255, 615);
            btn5.Margin = new Padding(2, 2, 2, 2);
            btn5.Name = "btn5";
            btn5.Size = new Size(230, 90);
            btn5.TabIndex = 10;
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = true;
            btn5.Click += btn5_Click;
            // 
            // btn4
            // 
            btn4.Font = new Font("Bahnschrift", 12F);
            btn4.Location = new Point(17, 615);
            btn4.Margin = new Padding(2, 2, 2, 2);
            btn4.Name = "btn4";
            btn4.Size = new Size(230, 90);
            btn4.TabIndex = 9;
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = true;
            btn4.Click += btn4_Click;
            // 
            // btnSumar
            // 
            btnSumar.Font = new Font("Bahnschrift", 12F);
            btnSumar.Location = new Point(727, 711);
            btnSumar.Margin = new Padding(2, 2, 2, 2);
            btnSumar.Name = "btnSumar";
            btnSumar.Size = new Size(230, 90);
            btnSumar.TabIndex = 16;
            btnSumar.Text = "+";
            btnSumar.UseVisualStyleBackColor = true;
            btnSumar.Click += btnSumar_Click;
            // 
            // btn3
            // 
            btn3.Font = new Font("Bahnschrift", 12F);
            btn3.Location = new Point(491, 711);
            btn3.Margin = new Padding(2, 2, 2, 2);
            btn3.Name = "btn3";
            btn3.Size = new Size(230, 90);
            btn3.TabIndex = 15;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            btn3.Click += btn3_Click;
            // 
            // btn2
            // 
            btn2.Font = new Font("Bahnschrift", 12F);
            btn2.Location = new Point(255, 711);
            btn2.Margin = new Padding(2, 2, 2, 2);
            btn2.Name = "btn2";
            btn2.Size = new Size(230, 90);
            btn2.TabIndex = 14;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += btn2_Click;
            // 
            // btn1
            // 
            btn1.Font = new Font("Bahnschrift", 12F);
            btn1.Location = new Point(17, 711);
            btn1.Margin = new Padding(2, 2, 2, 2);
            btn1.Name = "btn1";
            btn1.Size = new Size(230, 90);
            btn1.TabIndex = 13;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += btn1_Click;
            // 
            // btnResultado
            // 
            btnResultado.Font = new Font("Bahnschrift", 12F);
            btnResultado.Location = new Point(727, 808);
            btnResultado.Margin = new Padding(2, 2, 2, 2);
            btnResultado.Name = "btnResultado";
            btnResultado.Size = new Size(230, 90);
            btnResultado.TabIndex = 20;
            btnResultado.Text = "=";
            btnResultado.UseVisualStyleBackColor = true;
            btnResultado.Click += btnResultado_Click;
            // 
            // btnDecimal
            // 
            btnDecimal.Font = new Font("Bahnschrift", 12F);
            btnDecimal.Location = new Point(491, 808);
            btnDecimal.Margin = new Padding(2, 2, 2, 2);
            btnDecimal.Name = "btnDecimal";
            btnDecimal.Size = new Size(230, 90);
            btnDecimal.TabIndex = 19;
            btnDecimal.Text = ".";
            btnDecimal.UseVisualStyleBackColor = true;
            btnDecimal.Click += btnDecimal_Click_1;
            // 
            // btn0
            // 
            btn0.Font = new Font("Bahnschrift", 12F);
            btn0.Location = new Point(255, 808);
            btn0.Margin = new Padding(2, 2, 2, 2);
            btn0.Name = "btn0";
            btn0.Size = new Size(230, 90);
            btn0.TabIndex = 18;
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = true;
            btn0.Click += btn0_Click;
            // 
            // btnSigno
            // 
            btnSigno.Font = new Font("Bahnschrift", 12F);
            btnSigno.Location = new Point(17, 808);
            btnSigno.Margin = new Padding(2, 2, 2, 2);
            btnSigno.Name = "btnSigno";
            btnSigno.Size = new Size(230, 90);
            btnSigno.TabIndex = 17;
            btnSigno.Text = "+/-";
            btnSigno.UseVisualStyleBackColor = true;
            btnSigno.Click += btnSigno_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Bahnschrift", 12F);
            btnEliminar.Location = new Point(727, 328);
            btnEliminar.Margin = new Padding(2, 2, 2, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(230, 90);
            btnEliminar.TabIndex = 24;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Bahnschrift", 12F);
            btnClear.Location = new Point(491, 328);
            btnClear.Margin = new Padding(2, 2, 2, 2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(230, 90);
            btnClear.TabIndex = 23;
            btnClear.Text = "C";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnClearE
            // 
            btnClearE.Font = new Font("Bahnschrift", 12F);
            btnClearE.Location = new Point(255, 328);
            btnClearE.Margin = new Padding(2, 2, 2, 2);
            btnClearE.Name = "btnClearE";
            btnClearE.Size = new Size(230, 90);
            btnClearE.TabIndex = 22;
            btnClearE.Text = "CE";
            btnClearE.UseVisualStyleBackColor = true;
            btnClearE.Click += btnClearE_Click;
            // 
            // button11
            // 
            button11.Font = new Font("Bahnschrift", 12F);
            button11.Location = new Point(17, 328);
            button11.Margin = new Padding(2, 2, 2, 2);
            button11.Name = "button11";
            button11.Size = new Size(230, 90);
            button11.TabIndex = 21;
            button11.Text = "Historial";
            button11.UseVisualStyleBackColor = true;
            button11.Click += btnHistorial_Click;
            // 
            // txbCarry
            // 
            txbCarry.Font = new Font("Bahnschrift", 15.9000006F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txbCarry.Location = new Point(13, 6);
            txbCarry.Margin = new Padding(2, 2, 2, 2);
            txbCarry.Multiline = true;
            txbCarry.Name = "txbCarry";
            txbCarry.ReadOnly = true;
            txbCarry.RightToLeft = RightToLeft.No;
            txbCarry.Size = new Size(943, 63);
            txbCarry.TabIndex = 25;
            txbCarry.Text = "0";
            txbCarry.TextAlign = HorizontalAlignment.Right;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 912);
            Controls.Add(txbCarry);
            Controls.Add(btnEliminar);
            Controls.Add(btnClear);
            Controls.Add(btnClearE);
            Controls.Add(button11);
            Controls.Add(btnResultado);
            Controls.Add(btnDecimal);
            Controls.Add(btn0);
            Controls.Add(btnSigno);
            Controls.Add(btnSumar);
            Controls.Add(btn3);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Controls.Add(btnRestar);
            Controls.Add(btn6);
            Controls.Add(btn5);
            Controls.Add(btn4);
            Controls.Add(btnProd);
            Controls.Add(btn9);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(btnDividir);
            Controls.Add(btnRaiz);
            Controls.Add(btnPotencia);
            Controls.Add(button1);
            Controls.Add(txbResultado);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button btnPotencia;
        private Button btnRaiz;
        private Button btnDividir;
        private Button btnProd;
        private Button btn9;
        private Button btn8;
        private Button btn7;
        private Button btnRestar;
        private Button btn6;
        private Button btn5;
        private Button btn4;
        private Button btnSumar;
        private Button btn3;
        private Button btn2;
        private Button btnResultado;
        private Button btnDecimal;
        private Button btn0;
        private Button btnSigno;
        private Button btnEliminar;
        private Button btnClear;
        private Button btnClearE;
        private Button button11;
        public Button btn1;
        public TextBox txbResultado;
        public TextBox txbCarry;
    }
}
