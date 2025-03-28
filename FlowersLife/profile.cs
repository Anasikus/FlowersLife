using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowersLife
{
    public partial class profile : Form
    {
        public profile()
        {
            InitializeComponent();
            LoadUserProfile();
            panel1.Location = new Point(
                              (ClientSize.Width - panel1.Width) / 2,
                              (ClientSize.Height - panel1.Height) / 2
            );
            panel3.Location = new Point(
                            (ClientSize.Width - panel3.Width) / 2,
                            (ClientSize.Height - panel3.Height) / 2
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bonuses bonuses = new bonuses();
            this.Hide();
            bonuses.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            orders orders = new orders();
            this.Close();
            orders.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            index ar = new index();
            this.Close();
            ar.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            this.Close();
            purchases.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
        }

        private void profile_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Выберите фотографию";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;

                    // Отображаем изображение в PictureBox
                    pictureBox1.ImageLocation = selectedImagePath;
                    pictureBox2.ImageLocation = selectedImagePath;

                    // Сохраняем путь в БД
                    SaveUserProfileImage(autorization.CurrentUserId, selectedImagePath);
                }
            }
        }

        private void SaveUserProfileImage(int userId, string imagePath)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE clients SET photo = @photo WHERE idUsers = @userId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@photo", imagePath);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
            LoadUserProfile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var name = nameBox.Text;
            var email = mailBox.Text;
            var phone = maskedTextBox1.Text;

            int userId = autorization.CurrentUserId;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isUpdated = UpdateProfile(userId, name, email, phone);

            if (isUpdated)
            {
                MessageBox.Show("Данные успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserProfile(); // Подгружаем обновленные данные сразу
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panel1.Visible = true;
            panel3.Visible = false;
        }


        private void LoadUserProfile()
        {
            if (autorization.CurrentUserId <= 0)
            {
                MessageBox.Show("Ошибка: пользователь не авторизован!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = autorization.CurrentUserId;
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT c.name, c.mail, u.username, c.photo 
                             FROM clients c 
                             INNER JOIN users u ON c.idUsers = u.id 
                             WHERE c.idUsers = @userId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string mail = reader["mail"].ToString();
                                string username = reader["username"].ToString();
                                string photoPath = reader["photo"].ToString();

                                label1.Text = name;
                                label8.Text = mail;
                                label9.Text = username;

                                // Загружаем фото, если оно есть
                                if (!string.IsNullOrEmpty(photoPath))
                                {
                                    pictureBox1.ImageLocation = photoPath;
                                    pictureBox2.ImageLocation = photoPath;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Профиль пользователя не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки профиля: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool UpdateProfile(int userId, string name, string email, string phone)
        {
            // Строка подключения к базе данных
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=flowersLife;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Транзакция для выполнения двух запросов: обновление в таблице clients и users
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Обновляем данные в таблице clients
                            string updateClientsQuery = "UPDATE clients SET name = @name, mail = @mail WHERE idUsers = @userId";
                            MySqlCommand commandClients = new MySqlCommand(updateClientsQuery, connection, transaction);
                            commandClients.Parameters.AddWithValue("@name", name);
                            commandClients.Parameters.AddWithValue("@mail", email);
                            commandClients.Parameters.AddWithValue("@userId", userId);

                            // Выполняем команду для обновления данных в таблице clients
                            commandClients.ExecuteNonQuery();

                            // Обновляем данные в таблице users (телефон)
                            string updateUsersQuery = "UPDATE users SET username = @phone WHERE id = @userId";
                            MySqlCommand commandUsers = new MySqlCommand(updateUsersQuery, connection, transaction);
                            commandUsers.Parameters.AddWithValue("@phone", phone);
                            commandUsers.Parameters.AddWithValue("@userId", userId);

                            // Выполняем команду для обновления данных в таблице users
                            commandUsers.ExecuteNonQuery();

                            // Подтверждаем транзакцию
                            transaction.Commit();

                            return true; // Данные успешно обновлены
                        }
                        catch (Exception ex)
                        {
                            // В случае ошибки откатываем транзакцию
                            transaction.Rollback();
                            MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                return false;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
            LoadUserProfile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    