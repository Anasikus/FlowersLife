using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FlowersLife
{
    public partial class autorization : Form
    {
        public autorization()
        {
            InitializeComponent();
            string mysglConn = "server = 127.0.0.1; user = root; database = flowersLife; password=";
            MySqlConnection mySglConnection = new MySqlConnection(mysglConn);
            try
            {
                mySglConnection.Open();
                MessageBox.Show("Connection success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySglConnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем данные, введенные пользователем
            var username = maskedTextBox1.Text;
            var password = textBox2.Text;

            // Проверяем, существует ли пользователь с такими данными
            bool isValidUser = CheckUserCredentials(username, password);

            if (isValidUser)
            {
                // Если пользователь найден, переходим на следующую форму
                MessageBox.Show("Авторизация успешна!", "Успех");

                // Создаем форму профиля или другую форму
                profile profile = new profile();
                this.Hide();  // Скрываем текущую форму авторизации
                profile.ShowDialog();  // Показываем форму профиля
                this.Close();  // Закрываем форму авторизации
            }
            else
            {
                // Если пользователь не найден, выводим ошибку
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Метод для проверки данных пользователя в базе данных
        private bool CheckUserCredentials(string username, string password)
        {
            // Строка подключения к базе данных MySQL
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            // Запрос для проверки, есть ли пользователь с таким логином и паролем
            string queryString = "SELECT id, username, password, role FROM users WHERE username = @username AND password = @password";

            try
            {
                // Создаем подключение к базе данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Создаем команду с SQL-запросом и параметрами
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Открываем соединение с базой данных
                    connection.Open();

                    // Выполняем запрос и получаем результаты
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Проверяем, есть ли такой пользователь
                        if (reader.HasRows)
                        {
                            // Если нашли пользователя, то возвращаем true
                            return true;
                        }
                        else
                        {
                            // Если не нашли, возвращаем false
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок подключения
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                return false;
            }
        }

        private void autorization_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (ClientSize.Width - panel1.Width) / 2,
                (ClientSize.Height - panel1.Height) / 2
            );
            textBox2.Text = "Введите пароль";
            textBox2.ForeColor = Color.Gray;
            textBox2.UseSystemPasswordChar = false; // Подсказка без звездочек
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            index index = new index();
            index.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            index index = new index();
            index.Close();
        }
        //Подсказки в полях ввода
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Введите пароль")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.UseSystemPasswordChar = true; // Включаем отображение звездочек
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;
                textBox2.UseSystemPasswordChar = false; // Отключаем звездочки для подсказки
            }
        }
        //-----------------------
    }
}
