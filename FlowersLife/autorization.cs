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
        public static int CurrentUserId { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем данные, введенные пользователем
            var username = maskedTextBox1.Text;
            var password = textBox2.Text;

            // Проверяем, существует ли пользователь с такими данными
            bool isValidUser = CheckUserCredentials(username, password);

            if (isValidUser)
            {
                // Если пользователь найден, получаем его id
                int userId = GetUserIdByCredentials(username, password);
                CurrentUserId = userId; // Сохраняем id в глобальное свойство

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
        private int GetUserIdByCredentials(string username, string password)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            string query = "SELECT id FROM users WHERE username = @username AND password = @password";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int userId = Convert.ToInt32(command.ExecuteScalar());

                    return userId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                return -1; // Возвращаем -1 в случае ошибки
            }
        }

        // Метод для проверки данных пользователя в базе данных
        private bool CheckUserCredentials(string username, string password)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();

                    // Получаем количество пользователей с такими данными
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Если найден хотя бы один пользователь, возвращаем true
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                return false;
            }
        }

        // Метод для отображения профиля пользователя
        private void ShowUserProfile(int userId)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            // Запрос для получения данных профиля пользователя из таблицы clients
            string profileQuery = "SELECT * FROM clients WHERE idUsers = @userId";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(profileQuery, connection);
                    command.Parameters.AddWithValue("@userId", userId);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Если профиль существует, показываем форму профиля
                            profile userProfileForm = new profile();
                            this.Hide(); // Скрываем форму авторизации
                            userProfileForm.ShowDialog(); // Показываем профиль
                            this.Close(); // Закрываем форму авторизации
                        }
                        else
                        {
                            MessageBox.Show("Профиль не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
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

        // Подсказки в полях ввода
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
    }
}
