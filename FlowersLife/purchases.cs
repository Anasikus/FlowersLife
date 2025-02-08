using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FlowersLife
{
    public partial class purchases : Form
    {
        private string connectionString = "server=localhost;user=root;database=flowersLife;password=";

        public purchases()
        {
            InitializeComponent();

            // Включаем полосу прокрутки
            Panel mainPanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill // Панель занимает всю форму
            };
            this.Controls.Add(mainPanel);

            LoadAllProducts(mainPanel);
        }

        private void LoadAllProducts(Panel container)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT photo, nameProducts, cost FROM products";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Настройки расположения
                        int x = 40;
                        int y = 120; // Отступ сверху от первого элемента
                        int spacing = 10; // Отступ между карточками
                        int boxWidth = 150; // Ширина карточки
                        int boxHeight = 200; // Высота карточки
                        int maxPerRow = 2; // Карточек в строке

                        // Учет нижней панели
                        int lowerPanelOffset = 120; // Добавляем дополнительный отступ для нижней панели

                        int countInRow = 0; // Счетчик карточек в строке
                        int currentY = y;

                        while (reader.Read())
                        {
                            // Загружаем путь к изображению
                            string imagePath = reader["photo"] as string;
                            string name = reader["nameProducts"] as string;
                            decimal price = reader.GetDecimal("cost");

                            // Проверяем, существует ли изображение
                            Image image = null;
                            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                            {
                                image = Image.FromFile(imagePath);
                            }

                            // Создаем карточку
                            Panel productPanel = new Panel
                            {
                                Size = new Size(boxWidth, boxHeight),
                                Location = new Point(x, currentY),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            // PictureBox для изображения
                            PictureBox pictureBox = new PictureBox
                            {
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Size = new Size(130, 100),
                                Location = new Point(10, 10),
                                Image = image ?? Image.FromFile("F:\\Учеба\\Разработка программных модулей 3 курс\\FlowersLife\\Resources\\placeholder.png")
                            };
                            productPanel.Controls.Add(pictureBox);

                            // Label для имени
                            Label nameLabel = new Label
                            {
                                Text = name ?? "Название отсутствует",
                                AutoSize = false,
                                Size = new Size(130, 30),
                                Location = new Point(10, 120),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            productPanel.Controls.Add(nameLabel);

                            // Label для цены
                            Label priceLabel = new Label
                            {
                                Text = $"{price:C}",
                                AutoSize = false,
                                Size = new Size(130, 20),
                                Location = new Point(10, 160),
                                TextAlign = ContentAlignment.MiddleCenter,
                                ForeColor = Color.Green
                            };
                            productPanel.Controls.Add(priceLabel);

                            // Добавляем карточку в контейнер
                            container.Controls.Add(productPanel);

                            // Смещаем позицию для следующей карточки
                            countInRow++;
                            if (countInRow >= maxPerRow)
                            {
                                countInRow = 0;
                                x = 40; // Возвращаемся к началу строки
                                currentY += boxHeight + spacing;
                            }
                            else
                            {
                                x += boxWidth + spacing;
                            }
                        }

                        // Увеличиваем высоту контейнера, если карточки доходят до нижней панели
                        container.Height = currentY + boxHeight + lowerPanelOffset + 200;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            profile profile = new profile();
            purchases.Close();
            profile.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Incoming incoming = new Incoming();
            purchases purchases = new purchases();
            purchases.Close();
            incoming.Show();
        }

        private void purchases_Load(object sender, EventArgs e)
        {

        }
    }
}
