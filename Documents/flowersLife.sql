-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Янв 31 2025 г., 09:25
-- Версия сервера: 8.0.30
-- Версия PHP: 8.0.22

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
-- Структура таблицы `products`
--

CREATE TABLE `products` (
  `idProducts` int NOT NULL,
  `nameProducts` text NOT NULL,
  `cost` text NOT NULL,
  `photo` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `products`
--

INSERT INTO `products` (`idProducts`, `nameProducts`, `cost`, `photo`) VALUES
(1, 'Букет с французкими розами и озотамнусом  FMART by FLOWERS LIFE', '500', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет1.1.png'),
(2, 'Букет с гортензией, розами и диантусами FMART by FLOWERS LIFE', '750', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет2.1.png'),
(3, 'Гермини и Роза в коробке FMART by FLOWERS LIFE', '1000', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет3.1.png'),
(4, 'Букет с розами, хризантемой и диантусом FMART by FLOWERS LIFE', '400', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет4.1.png'),
(5, 'Букет с лютиками и эустомами FMART by FLOWERS LIFE', '100', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет5.1.png'),
(6, 'Букет с матиолами, геориями и розами FMART by FLOWERS LIFE', '250', 'D:\\Разработка программных модулей 3 курс\\img\\Главная\\букет6.1.png'),
(7, 'Лилии', '600', NULL),
(8, 'Розы', '789', NULL),
(9, 'опсорлпр', '7587', NULL),
(10, 'щанвенк', '246', NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `role` varchar(25) NOT NULL DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `role`) VALUES
(1, '123', '123', 'user');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`idProducts`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `products`
--
ALTER TABLE `products`
  MODIFY `idProducts` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
