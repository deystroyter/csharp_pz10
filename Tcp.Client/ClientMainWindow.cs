using System;
using System.Windows.Forms;
using SomeProject.Library.Client;
using SomeProject.Library;

namespace SomeProject.TcpClient
{
    public partial class ClientMainWindow : Form
    {
        public ClientMainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик кнопки Send Message
        /// </summary>
        private void OnMsgBtnClick(object sender, EventArgs e)
        {
            Client client = new Client();
            Result res = client.SendMessageToServer(textBox.Text).Result;
            if(res == Result.OK)
            {
                textBox.Text = "";
                labelRes.Text = "Message was sent succefully!";
            }
            else
            {
                labelRes.Text = "Cannot send the message to the server.";
            }
            timer.Interval = 2000;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            labelRes.Text = "";
            timer.Stop();
        }
        /// <summary>
        /// Обработчик кнопки OpenFile
        /// </summary>
        private void openFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                @fileTextBox.Text = @openFileDialog1.FileName;
            }
            else
            {
                UserPath_Error();
            }
            openFileDialog1.Dispose();
        }

        /// <summary>
        /// Выдаёт ошибку, если файла не существует
        /// </summary>
        private void UserPath_Error()
        {
            MessageBox.Show("Пожалуйста, укажите существующий путь!", "Path Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            @fileTextBox.Text = @"D:\";

        }
        /// <summary>
        /// Обработчик кнопки Send File
        /// </summary>
        private void sendFileBtn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(fileTextBox.Text))
            {
                Client client = new Client();
                Result res = client.SendFileToServer(fileTextBox.Text).Result;
                if (res == Result.OK)
                {
                    textBox.Text = "";
                    labelRes.Text = "File was sent succefully!";
                }
                else
                {
                    labelRes.Text = "Cannot send file to the server.";
                }
            }
            else
            {
                UserPath_Error();
            }
        }
    }
}
