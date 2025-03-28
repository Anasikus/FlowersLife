-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 28 2025 г., 18:50
-- Версия сервера: 8.0.30
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `flowersLife`
--

-- --------------------------------------------------------

--
-- Структура таблицы `categories`
--

CREATE TABLE `categories` (
  `codeCategory` int NOT NULL,
  `title` text NOT NULL,
  `photo` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `categories`
--

INSERT INTO `categories` (`codeCategory`, `title`, `photo`) VALUES
(1, 'Цветы поштучно', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\букет_невесты.png'),
(2, 'Монобукеты', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\для_декора.png'),
(3, 'Букеты из сухоцветов', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\монобукеты.png'),
(4, 'Мягкие игрушки', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\мягие_игрушки.png'),
(5, 'Букет невесты', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\подарочные.png'),
(6, 'Сладкие наборы', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\сертификат.png'),
(7, 'Подарочные наборы', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\сладкие_наборы.png'),
(8, 'Цветы для декора', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\сухоцветы.png'),
(9, 'Подарочные сертификаты', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\categories\\Цветы_поштучно.png');

-- --------------------------------------------------------

--
-- Структура таблицы `clients`
--

CREATE TABLE `clients` (
  `id` int NOT NULL,
  `photo` text,
  `surname` text,
  `name` text,
  `patronymic` text,
  `dateOfBirth` date DEFAULT NULL,
  `mail` text,
  `idUsers` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `clients`
--

INSERT INTO `clients` (`id`, `photo`, `surname`, `name`, `patronymic`, `dateOfBirth`, `mail`, `idUsers`) VALUES
(4, 'C:\\Users\\Ananasik\\Pictures\\maxresdefault.jpg', NULL, 'Hu', NULL, NULL, '123@123', 8),
(5, 'C:\\Users\\Ananasik\\Pictures\\i.jpg', NULL, 'Anastasia', NULL, NULL, 'ad@gmail.com', 9);

-- --------------------------------------------------------

--
-- Структура таблицы `products`
--

CREATE TABLE `products` (
  `idProducts` int NOT NULL,
  `nameProducts` text NOT NULL,
  `cost` text NOT NULL,
  `photo` text,
  `codeCategories` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `products`
--

INSERT INTO `products` (`idProducts`, `nameProducts`, `cost`, `photo`, `codeCategories`) VALUES
(1, 'Букет с французкими розами и озотамнусом  FMART by FLOWERS LIFE', '1000', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет1.1.png', 2),
(2, 'Букет с гортензией, розами и диантусами FMART by FLOWERS LIFE', '1345', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет2.1.png', 2),
(3, 'Гермини и Роза в коробке FMART by FLOWERS LIFE', '1356', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет3.1.png', 2),
(4, 'Букет с розами, хризантемой и диантусом FMART by FLOWERS LIFE', '1356', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет4.1.png', 2),
(5, 'Букет с лютиками и эустомами FMART by FLOWERS LIFE', '1357', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет5.1.png', 2),
(6, 'Букет с матиолами, геориями и розами FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная \\букет6.1.png', 2),
(7, 'Букет с альстромериями  FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет7.png', 2),
(8, 'Букет с розами, пионами и гортензией FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет8.png', 2),
(9, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет8.png', 2),
(10, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет8.png', 2),
(11, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет9.png', 2),
(12, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет10.png', 2),
(13, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет7.png', 2),
(14, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет7.png', 2),
(15, 'Букет с тюльпанами, гвоздикой и брунием FMART by FLOWERS LIFE', '1234', 'F:\\Учеба\\Разработка программных модулей 3 курс\\img\\Главная\\букет7.png', 2),
(16, 'Сертификат на 500 рублей', '500', NULL, 9),
(17, 'Сертификат на 1000 рублей', '1000', NULL, 9),
(18, 'Сертификат на 1500 рублей', '1500', NULL, 9),
(19, 'Сертификат на 2000 рублей', '2000', NULL, 9),
(20, 'Сертификат на 5000 рублей', '5000', NULL, 9);

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `role` varchar(255) DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `role`) VALUES
(8, ' +7 (999) 999-99-90', '123', 'user'),
(9, ' +7 (999) 999-99-99', '123', 'user');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`codeCategory`);

--
-- Индексы таблицы `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `idUsers` (`idUsers`);

--
-- Индексы таблицы `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`idProducts`),
  ADD KEY `codeCategories` (`codeCategories`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `categories`
--
ALTER TABLE `categories`
  MODIFY `codeCategory` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT для таблицы `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `products`
--
ALTER TABLE `products`
  MODIFY `idProducts` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `clients`
--
ALTER TABLE `clients`
  ADD CONSTRAINT `clients_ibfk_1` FOREIGN KEY (`idUsers`) REFERENCES `users` (`id`);

--
-- Ограничения внешнего ключа таблицы `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `products_ibfk_1` FOREIGN KEY (`codeCategories`) REFERENCES `categories` (`codeCategory`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
