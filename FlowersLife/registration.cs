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
    public partial class registration : Form
    {
        public registration()
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
            var user = maskedTextBox1.Text;  // Номер телефона
            var password = textBox2.Text;  // Пароль
            var returnPassword = textBox3.Text;  // Повтор пароля
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            if (password != returnPassword)
            {
                MessageBox.Show("Пароли не совпадают! Пожалуйста, повторите ввод.", "Ошибка");
                return;
            }

            // Проверка, если телефон уже зарегистрирован
            string checkQuery = $"SELECT COUNT(*) FROM users WHERE username = '{user}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                    connection.Open();

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Данный номер телефона занят");
                        return;
                    }

                    // Вставка нового пользователя в таблицу users
                    string insertQuery = $"INSERT INTO users (username, password) VALUES ('{user}', '{password}')";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    int result = insertCommand.ExecuteNonQuery();

                    if (result == 1)
                    {
                        // Получаем id нового пользователя
                        string selectIdQuery = $"SELECT id FROM users WHERE username = '{user}'";
                        MySqlCommand selectIdCommand = new MySqlCommand(selectIdQuery, connection);
                        int userId = Convert.ToInt32(selectIdCommand.ExecuteScalar());

                        // Создание профиля в таблице clients
                        string createProfileQuery = $"INSERT INTO clients (idUsers) VALUES ({userId})";
                        MySqlCommand createProfileCommand = new MySqlCommand(createProfileQuery, connection);
                        createProfileCommand.ExecuteNonQuery();

                        MessageBox.Show("Аккаунт успешно создан и профиль добавлен!", "Успех!");
                        autorization autorization = new autorization();
                        this.Hide();
                        autorization.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Аккаунт не создан!");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}");
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Ошибка формата: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }


        private void registration_Load(object sender, EventArgs e)
        {
            //Центровка панели
            registration registration = new registration();
            panel1.Location = new Point(
            (registration.ClientSize.Width - panel1.Width) / 2,
            (registration.ClientSize.Height - panel1.Height) / 2
            );
            //
            textBox2.Text = "Придумайте пароль";
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = "Повторите пароль";
            textBox3.ForeColor = Color.Gray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index index = new index();
            index.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            index index = new index();
            index.Close();
            this.Close();
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Придумайте пароль";
                textBox2.ForeColor = Color.Gray;
                textBox2.UseSystemPasswordChar = false;
            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Придумайте пароль")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.UseSystemPasswordChar = true;
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Повторите пароль";
                textBox3.ForeColor = Color.Gray;
                textBox3.UseSystemPasswordChar = false;
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Повторите пароль")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
                textBox3.UseSystemPasswordChar = true; // Включаем отображение звездочек
            }
        }
    }
}
