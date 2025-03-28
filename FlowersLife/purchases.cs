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
        private TableLayoutPanel tableLayoutPanel1;
        private Panel scrollPanel;

        public purchases()
        {
            InitializeComponent();


            // Панель для прокрутки (размер 1200x810, координаты 626; 163)
            scrollPanel = new Panel
            {
                Size = new Size(1000, 810),
                Location = new Point(626, 115),
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(20)
            };
            this.Controls.Add(scrollPanel);

            // TableLayoutPanel для карточек товаров
            tableLayoutPanel1 = new TableLayoutPanel
            {
                ColumnCount = 3, // 3 колонки для карточек
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            // Настроим столбцы (по 33.3% ширины)
            for (int i = 0; i < 3; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3F));

            scrollPanel.Controls.Add(tableLayoutPanel1);
            tableLayoutPanel1.AutoSize = false;
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.Size = new Size(1160, 770);
            LoadCategories();
            LoadAllProducts();
        }

        private void LoadAllProducts()
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
                        int cardWidth = 200;
                        int cardHeight = 260;
                        int padding = 10;
                        int count = 0;

                        while (reader.Read())
                        {
                            string imagePath = reader["photo"]?.ToString();
                            string name = reader["nameProducts"]?.ToString();
                            decimal cost = reader.GetDecimal("cost");

                            // Загружаем изображение или заглушку
                            Image productImage = File.Exists(imagePath) ? Image.FromFile(imagePath) :
                                Image.FromFile("F:\\Учеба\\Разработка программных модулей 3 курс\\FlowersLife\\Resources\\placeholder.png");

                            // Карточка товара
                            Panel card = new Panel
                            {
                                Size = new Size(cardWidth, cardHeight),
                                Margin = new Padding(padding), // Отступы 20px
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.White
                            };

                            // Изображение товара
                            PictureBox pic = new PictureBox
                            {
                                Image = productImage,
                                SizeMode = PictureBoxSizeMode.Zoom,
                                Size = new Size(180, 140),
                                Location = new Point(10, 10)
                            };
                            card.Controls.Add(pic);

                            // Название товара
                            Label nameLabel = new Label
                            {
                                Text = name,
                                Location = new Point(10, 160),
                                Size = new Size(180, 40),
                                Font = new Font("Arial", 9, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            card.Controls.Add(nameLabel);

                            // Цена товара
                            Label priceLabel = new Label
                            {
                                Text = $"{cost:C} ₽",
                                Location = new Point(10, 210),
                                Size = new Size(180, 25),
                                Font = new Font("Arial", 10, FontStyle.Regular),
                                ForeColor = Color.Green,
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            card.Controls.Add(priceLabel);

                            // Добавляем карточку в таблицу
                            int row = count / 3;
                            int col = count % 3;

                            // Добавляем новую строку при необходимости
                            tableLayoutPanel1.RowCount = (count / 3) + 1;
                            if (tableLayoutPanel1.RowCount > tableLayoutPanel1.RowStyles.Count)
                            {
                                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            }


                            tableLayoutPanel1.Controls.Add(card, col, row);
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT codeCategory, title, photo FROM categories";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int categoryId = reader.GetInt32("codeCategory");
                            string title = reader["title"].ToString();
                            string imagePath = reader["photo"].ToString();

                            // Загружаем изображение или заглушку
                            Image categoryImage = File.Exists(imagePath) ? Image.FromFile(imagePath) :
                                Image.FromFile("F:\\Учеба\\Разработка программных модулей 3 курс\\FlowersLife\\Resources\\placeholder.png");

                            // Панель категории (контейнер)
                            Panel categoryPanel = new Panel
                            {
                                Size = new Size(180, 120),
                                Margin = new Padding(5),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.White
                            };

                            // Вложенный TableLayoutPanel для центрирования
                            TableLayoutPanel innerTable = new TableLayoutPanel
                            {
                                Dock = DockStyle.Fill,
                                RowCount = 2,
                                ColumnCount = 1
                            };
                            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60)); // Верхняя часть (изображение)
                            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40)); // Нижняя часть (текст)

                            // Изображение категории
                            PictureBox pic = new PictureBox
                            {
                                Image = categoryImage,
                                SizeMode = PictureBoxSizeMode.Zoom,
                                Size = new Size(55, 60),
                                Anchor = AnchorStyles.None,
                                Cursor = Cursors.Hand, // Изменяем курсор
                                Tag = categoryId // Передаём ID категории
                            };

                            // Название категории (многострочный текст)
                            Label nameLabel = new Label
                            {
                                Text = title,
                                AutoSize = false,
                                Dock = DockStyle.Fill,
                                Font = new Font("Arial", 9, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter,
                                MaximumSize = new Size(180, 50) // Ограничение высоты
                            };

                            // Добавляем клик на изображение для фильтрации товаров
                            pic.Click += (sender, e) =>
                            {
                                int selectedCategoryId = (int)((PictureBox)sender).Tag;
                                LoadProductsByCategory(selectedCategoryId);
                            };

                            // Добавляем элементы в таблицу
                            innerTable.Controls.Add(pic, 0, 0);
                            innerTable.Controls.Add(nameLabel, 0, 1);

                            // Добавляем вложенную таблицу в панель категории
                            categoryPanel.Controls.Add(innerTable);

                            // Добавляем панель в tableLayoutPanel2
                            tableLayoutPanel2.Controls.Add(categoryPanel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadProductsByCategory(int categoryId)
        {
            tableLayoutPanel1.Controls.Clear(); // Очищаем предыдущие товары

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT photo, nameProducts, cost FROM products WHERE codeCategory = @categoryId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@categoryId", categoryId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int cardWidth = 200;
                            int cardHeight = 260;
                            int padding = 10;
                            int count = 0;

                            while (reader.Read())
                            {
                                string imagePath = reader["photo"]?.ToString();
                                string name = reader["nameProducts"]?.ToString();
                                decimal cost = reader.GetDecimal("cost");

                                // Загружаем изображение или заглушку
                                Image productImage = File.Exists(imagePath) ? Image.FromFile(imagePath) :
                                    Image.FromFile("F:\\Учеба\\Разработка программных модулей 3 курс\\FlowersLife\\Resources\\placeholder.png");

                                // Карточка товара
                                Panel card = new Panel
                                {
                                    Size = new Size(cardWidth, cardHeight),
                                    Margin = new Padding(padding),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    BackColor = Color.White
                                };

                                // Изображение товара
                                PictureBox pic = new PictureBox
                                {
                                    Image = productImage,
                                    SizeMode = PictureBoxSizeMode.Zoom,
                                    Size = new Size(180, 140),
                                    Location = new Point(10, 10)
                                };
                                card.Controls.Add(pic);

                                // Название товара
                                Label nameLabel = new Label
                                {
                                    Text = name,
                                    Location = new Point(10, 160),
                                    Size = new Size(180, 40),
                                    Font = new Font("Arial", 9, FontStyle.Bold),
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                card.Controls.Add(nameLabel);

                                // Цена товара
                                Label priceLabel = new Label
                                {
                                    Text = $"{cost:C} ₽",
                                    Location = new Point(10, 210),
                                    Size = new Size(180, 25),
                                    Font = new Font("Arial", 10, FontStyle.Regular),
                                    ForeColor = Color.Green,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                card.Controls.Add(priceLabel);

                                // Добавляем карточку в таблицу
                                int row = count / 3;
                                int col = count % 3;

                                // Добавляем новую строку при необходимости
                                tableLayoutPanel1.RowCount = (count / 3) + 1;
                                if (tableLayoutPanel1.RowCount > tableLayoutPanel1.RowStyles.Count)
                                {
                                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                }

                                tableLayoutPanel1.Controls.Add(card, col, row);
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            new profile().Show();
        }
    }
}
