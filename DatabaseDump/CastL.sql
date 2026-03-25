-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql_container:3306
-- Létrehozás ideje: 2026. Már 25. 00:12
-- Kiszolgáló verziója: 8.0.44
-- PHP verzió: 8.3.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `CastL`
--
CREATE DATABASE IF NOT EXISTS `CastL` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `CastL`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AuditLog`
--

CREATE TABLE `AuditLog` (
  `Id` int NOT NULL,
  `RecordId` int DEFAULT NULL,
  `TableName` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Operation` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ChangedBy` int DEFAULT NULL,
  `ChangedAt` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `AuditLog`
--

INSERT INTO `AuditLog` (`Id`, `RecordId`, `TableName`, `Operation`, `ChangedBy`, `ChangedAt`) VALUES
(1, 1, 'Users', 'INSERT', 1, '2026-01-12 23:09:26'),
(2, 2, 'Users', 'INSERT', 2, '2026-01-12 23:09:26'),
(3, 3, 'Users', 'INSERT', 3, '2026-01-12 23:09:26'),
(4, 4, 'Users', 'INSERT', 4, '2026-01-12 23:09:26'),
(5, 5, 'Users', 'INSERT', 5, '2026-01-12 23:09:26'),
(6, 6, 'Users', 'INSERT', 6, '2026-01-12 23:09:26'),
(7, 7, 'Users', 'INSERT', 7, '2026-01-12 23:09:26'),
(8, 8, 'Users', 'INSERT', 8, '2026-01-12 23:09:26'),
(9, 9, 'Users', 'INSERT', 9, '2026-01-12 23:09:26'),
(10, 10, 'Users', 'INSERT', 10, '2026-01-12 23:09:26'),
(11, 11, 'Users', 'INSERT', 11, '2026-01-12 23:09:26'),
(12, 12, 'Users', 'INSERT', 12, '2026-01-12 23:09:26'),
(13, 13, 'Users', 'INSERT', 13, '2026-01-12 23:09:26'),
(14, 14, 'Users', 'INSERT', 14, '2026-01-12 23:09:26'),
(15, 15, 'Users', 'INSERT', 15, '2026-01-12 23:09:26'),
(16, 16, 'Users', 'INSERT', 16, '2026-01-12 23:09:26'),
(17, 17, 'Users', 'INSERT', 17, '2026-01-12 23:09:26'),
(18, 18, 'Users', 'INSERT', 18, '2026-01-12 23:09:26'),
(19, 19, 'Users', 'INSERT', 19, '2026-01-12 23:09:26'),
(20, 20, 'Users', 'INSERT', 20, '2026-01-12 23:09:26'),
(21, 21, 'Users', 'INSERT', 21, '2026-01-13 06:59:55'),
(22, 22, 'Users', 'INSERT', 22, '2026-01-14 07:03:30'),
(23, 23, 'Users', 'INSERT', 23, '2026-01-14 07:05:16'),
(24, 24, 'Users', 'INSERT', 24, '2026-01-14 20:21:47'),
(25, 25, 'Users', 'INSERT', 25, '2026-01-14 20:22:00'),
(26, 26, 'Users', 'INSERT', 26, '2026-01-14 20:22:09'),
(27, 27, 'Users', 'INSERT', 27, '2026-01-14 20:22:09'),
(28, 28, 'Users', 'INSERT', 28, '2026-01-28 09:24:00'),
(29, 29, 'Users', 'INSERT', 29, '2026-02-01 10:58:16'),
(30, 22, 'Users', 'DELETE', 22, '2026-02-01 11:21:53'),
(31, 29, 'Users', 'DELETE', 29, '2026-02-01 11:32:57'),
(32, 30, 'Users', 'INSERT', 30, '2026-02-01 11:34:48'),
(33, 30, 'Users', 'DELETE', 30, '2026-02-01 11:52:36'),
(34, 31, 'Users', 'INSERT', 31, '2026-02-01 11:54:33'),
(35, 31, 'Users', 'DELETE', 31, '2026-02-01 11:54:48'),
(36, 32, 'Users', 'INSERT', 32, '2026-02-02 19:43:51'),
(37, 33, 'Users', 'INSERT', 33, '2026-02-02 20:11:54'),
(38, 34, 'Users', 'INSERT', 34, '2026-02-02 20:24:55'),
(39, 35, 'Users', 'INSERT', 35, '2026-02-02 20:31:36'),
(40, 36, 'Users', 'INSERT', 36, '2026-02-02 22:22:19'),
(41, 37, 'Users', 'INSERT', 37, '2026-02-03 08:47:08'),
(42, 38, 'Users', 'INSERT', 38, '2026-02-03 10:53:45'),
(43, 39, 'Users', 'INSERT', 39, '2026-02-04 08:09:16'),
(44, 40, 'Users', 'INSERT', 40, '2026-02-04 09:25:38'),
(45, 41, 'Users', 'INSERT', 41, '2026-02-04 10:07:00'),
(46, 42, 'Users', 'INSERT', 42, '2026-02-05 07:22:14'),
(47, 43, 'Users', 'INSERT', 43, '2026-02-05 07:38:19'),
(48, 44, 'Users', 'INSERT', 44, '2026-02-05 08:38:08'),
(49, 45, 'Users', 'INSERT', 45, '2026-02-09 10:21:08'),
(50, 23, 'Users', 'DELETE', 23, '2026-02-09 10:54:00'),
(51, 4, 'Users', 'DELETE', 4, '2026-02-09 10:55:03'),
(52, 3, 'Users', 'DELETE', 3, '2026-02-09 10:55:09'),
(53, 46, 'Users', 'INSERT', 46, '2026-02-11 19:35:10'),
(54, 46, 'Users', 'DELETE', 46, '2026-02-11 19:39:07'),
(55, 47, 'Users', 'INSERT', 47, '2026-02-11 19:39:13'),
(56, 47, 'Users', 'DELETE', 47, '2026-02-11 19:39:22'),
(57, 48, 'Users', 'INSERT', 48, '2026-02-11 19:39:23'),
(58, 48, 'Users', 'DELETE', 48, '2026-02-11 19:39:24'),
(59, 49, 'Users', 'INSERT', 49, '2026-02-15 19:37:53'),
(60, 49, 'Users', 'DELETE', 49, '2026-02-15 19:42:05'),
(61, 20, 'Users', 'DELETE', 20, '2026-02-16 07:49:37'),
(62, 11, 'Users', 'DELETE', 11, '2026-02-16 07:57:43'),
(63, 44, 'Users', 'DELETE', 44, '2026-02-16 09:06:34'),
(64, 42, 'Users', 'DELETE', 42, '2026-02-16 09:06:39'),
(65, 41, 'Users', 'DELETE', 41, '2026-02-16 09:06:42'),
(66, 40, 'Users', 'DELETE', 40, '2026-02-16 09:06:44'),
(67, 39, 'Users', 'DELETE', 39, '2026-02-16 09:06:46'),
(68, 38, 'Users', 'DELETE', 38, '2026-02-16 09:06:47'),
(69, 37, 'Users', 'DELETE', 37, '2026-02-16 09:06:49'),
(70, 36, 'Users', 'DELETE', 36, '2026-02-16 09:06:51'),
(71, 35, 'Users', 'DELETE', 35, '2026-02-16 09:06:53'),
(72, 34, 'Users', 'DELETE', 34, '2026-02-16 09:06:54'),
(73, 33, 'Users', 'DELETE', 33, '2026-02-16 09:06:56'),
(74, 32, 'Users', 'DELETE', 32, '2026-02-16 09:06:57'),
(75, 28, 'Users', 'DELETE', 28, '2026-02-16 09:06:59'),
(76, 27, 'Users', 'DELETE', 27, '2026-02-16 09:07:00'),
(77, 26, 'Users', 'DELETE', 26, '2026-02-16 09:07:11'),
(78, 25, 'Users', 'DELETE', 25, '2026-02-16 09:07:12'),
(79, 24, 'Users', 'DELETE', 24, '2026-02-16 09:07:14'),
(80, 21, 'Users', 'DELETE', 21, '2026-02-16 09:07:16'),
(81, 19, 'Users', 'DELETE', 19, '2026-02-16 09:07:17'),
(82, 18, 'Users', 'DELETE', 18, '2026-02-16 09:07:19'),
(83, 13, 'Users', 'DELETE', 13, '2026-02-16 09:16:10'),
(84, 1, 'Users', 'DELETE', 1, '2026-02-16 09:16:27'),
(85, 50, 'Users', 'INSERT', 50, '2026-02-16 09:21:19'),
(86, 50, 'Users', 'DELETE', 50, '2026-02-16 09:22:18'),
(87, 51, 'Users', 'INSERT', 51, '2026-02-16 09:23:09'),
(88, 43, 'Users', 'DELETE', 43, '2026-02-16 09:25:12'),
(89, 17, 'Users', 'DELETE', 17, '2026-02-16 09:25:15'),
(90, 16, 'Users', 'DELETE', 16, '2026-02-16 09:25:22'),
(91, 52, 'Users', 'INSERT', 52, '2026-02-17 09:11:57'),
(92, 53, 'Users', 'INSERT', 53, '2026-02-18 07:52:23'),
(93, 52, 'Users', 'DELETE', 52, '2026-02-19 07:31:43'),
(94, 53, 'Users', 'DELETE', 53, '2026-02-19 07:31:46'),
(95, 54, 'Users', 'INSERT', 54, '2026-02-19 07:44:58'),
(96, 8, 'Users', 'DELETE', 8, '2026-02-19 09:27:28'),
(97, 55, 'Users', 'INSERT', 55, '2026-02-21 21:43:26'),
(98, 55, 'Users', 'DELETE', 55, '2026-02-21 21:43:48'),
(99, 56, 'Users', 'INSERT', 56, '2026-02-21 21:44:51'),
(100, 56, 'Users', 'DELETE', 56, '2026-02-21 21:45:13'),
(101, 57, 'Users', 'INSERT', 57, '2026-02-21 21:45:21'),
(102, 57, 'Users', 'DELETE', 57, '2026-02-21 21:45:21'),
(103, 58, 'Users', 'INSERT', 58, '2026-02-21 21:45:29'),
(104, 59, 'Users', 'INSERT', 59, '2026-02-22 16:18:27'),
(105, 60, 'Users', 'INSERT', 60, '2026-02-22 16:33:27'),
(106, 6, 'Users', 'DELETE', 6, '2026-02-22 16:36:27'),
(107, 61, 'Users', 'INSERT', 61, '2026-02-22 17:44:20'),
(108, 61, 'Users', 'DELETE', 61, '2026-02-22 17:52:47'),
(109, 60, 'Users', 'DELETE', 60, '2026-02-22 17:58:59'),
(110, 62, 'Users', 'INSERT', 62, '2026-02-23 08:43:01'),
(111, 62, 'Users', 'DELETE', 62, '2026-02-23 08:43:42'),
(112, 63, 'Users', 'INSERT', 63, '2026-02-23 08:58:05'),
(113, 64, 'Users', 'INSERT', 64, '2026-02-23 09:45:11'),
(114, 59, 'Users', 'DELETE', 59, '2026-02-23 10:02:37'),
(115, 64, 'Users', 'DELETE', 64, '2026-02-23 10:07:31'),
(116, 65, 'Users', 'INSERT', 65, '2026-02-23 10:07:34'),
(117, 65, 'Users', 'DELETE', 65, '2026-02-23 10:07:48'),
(118, 66, 'Users', 'INSERT', 66, '2026-02-23 10:07:55'),
(119, 66, 'Users', 'DELETE', 66, '2026-02-23 10:08:20'),
(120, 67, 'Users', 'INSERT', 67, '2026-02-23 10:08:34'),
(121, 63, 'Users', 'DELETE', 63, '2026-02-23 10:12:15'),
(122, 67, 'Users', 'DELETE', 67, '2026-02-23 10:12:21'),
(123, 58, 'Users', 'DELETE', 58, '2026-02-23 10:12:25'),
(124, 54, 'Users', 'DELETE', 54, '2026-02-23 10:12:30'),
(125, 68, 'Users', 'INSERT', 68, '2026-02-23 10:53:29'),
(126, 69, 'Users', 'INSERT', 69, '2026-02-23 11:02:13'),
(127, 70, 'Users', 'INSERT', 70, '2026-02-23 19:13:54'),
(128, 71, 'Users', 'INSERT', 71, '2026-02-23 19:29:12'),
(129, 72, 'Users', 'INSERT', 72, '2026-02-23 19:36:44'),
(130, 72, 'Users', 'DELETE', 72, '2026-02-23 19:38:32'),
(131, 73, 'Users', 'INSERT', 73, '2026-02-23 19:39:17'),
(132, 74, 'Users', 'INSERT', 74, '2026-02-26 07:29:29'),
(133, 74, 'Users', 'DELETE', 74, '2026-02-26 07:38:15'),
(134, 75, 'Users', 'INSERT', 75, '2026-02-26 08:31:39'),
(135, 75, 'Users', 'DELETE', 75, '2026-03-02 07:02:48'),
(136, 76, 'Users', 'INSERT', 76, '2026-03-03 10:43:09'),
(137, 77, 'Users', 'INSERT', 77, '2026-03-03 10:46:17'),
(138, 78, 'Users', 'INSERT', 78, '2026-03-03 10:52:33'),
(139, 79, 'Users', 'INSERT', 79, '2026-03-03 20:44:35'),
(140, 80, 'Users', 'INSERT', 80, '2026-03-05 08:03:36'),
(141, 81, 'Users', 'INSERT', 81, '2026-03-09 09:18:07'),
(142, 82, 'Users', 'INSERT', 82, '2026-03-09 09:20:29'),
(143, 82, 'Users', 'DELETE', 82, '2026-03-09 09:21:19'),
(144, 83, 'Users', 'INSERT', 83, '2026-03-09 10:30:12'),
(145, 84, 'Users', 'INSERT', 84, '2026-03-10 08:02:27'),
(146, 2, 'Users', 'DELETE', 2, '2026-03-10 08:06:01'),
(147, 5, 'Users', 'DELETE', 5, '2026-03-10 08:06:01'),
(148, 7, 'Users', 'DELETE', 7, '2026-03-10 08:06:01'),
(149, 9, 'Users', 'DELETE', 9, '2026-03-10 08:06:01'),
(150, 10, 'Users', 'DELETE', 10, '2026-03-10 08:06:01'),
(151, 12, 'Users', 'DELETE', 12, '2026-03-10 08:06:01'),
(152, 14, 'Users', 'DELETE', 14, '2026-03-10 08:06:01'),
(153, 15, 'Users', 'DELETE', 15, '2026-03-10 08:06:01'),
(154, 45, 'Users', 'DELETE', 45, '2026-03-10 08:06:01'),
(155, 51, 'Users', 'DELETE', 51, '2026-03-10 08:06:01'),
(156, 68, 'Users', 'DELETE', 68, '2026-03-10 08:06:01'),
(157, 69, 'Users', 'DELETE', 69, '2026-03-10 08:06:01'),
(158, 70, 'Users', 'DELETE', 70, '2026-03-10 08:06:01'),
(159, 71, 'Users', 'DELETE', 71, '2026-03-10 08:06:01'),
(160, 73, 'Users', 'DELETE', 73, '2026-03-10 08:06:01'),
(161, 76, 'Users', 'DELETE', 76, '2026-03-10 08:06:01'),
(162, 77, 'Users', 'DELETE', 77, '2026-03-10 08:06:01'),
(163, 78, 'Users', 'DELETE', 78, '2026-03-10 08:06:01'),
(164, 79, 'Users', 'DELETE', 79, '2026-03-10 08:06:01'),
(165, 80, 'Users', 'DELETE', 80, '2026-03-10 08:06:01'),
(166, 81, 'Users', 'DELETE', 81, '2026-03-10 08:06:01'),
(167, 83, 'Users', 'DELETE', 83, '2026-03-10 08:06:01'),
(168, 85, 'Users', 'INSERT', 85, '2026-03-10 08:11:26'),
(169, 85, 'Users', 'DELETE', 85, '2026-03-10 08:13:27'),
(170, 86, 'Users', 'INSERT', 86, '2026-03-10 08:16:30'),
(171, 84, 'Users', 'DELETE', 84, '2026-03-10 08:21:23'),
(172, 86, 'Users', 'DELETE', 86, '2026-03-10 08:21:26'),
(173, 87, 'Users', 'INSERT', 87, '2026-03-10 08:22:32'),
(174, 87, 'Users', 'DELETE', 87, '2026-03-10 08:22:45'),
(175, 1, 'Users', 'INSERT', 1, '2026-03-10 08:25:44'),
(176, 2, 'Users', 'INSERT', 2, '2026-03-10 08:26:47'),
(177, 3, 'Users', 'INSERT', 3, '2026-03-10 08:49:18'),
(178, 3, 'Users', 'DELETE', 3, '2026-03-10 08:53:23'),
(179, 4, 'Users', 'INSERT', 4, '2026-03-10 08:55:00'),
(180, 4, 'Users', 'DELETE', 4, '2026-03-10 09:25:07'),
(181, 5, 'Users', 'INSERT', 5, '2026-03-10 09:26:09'),
(182, 5, 'Users', 'DELETE', 5, '2026-03-10 09:26:37'),
(183, 6, 'Users', 'INSERT', 6, '2026-03-10 09:27:31'),
(184, 6, 'Users', 'DELETE', 6, '2026-03-10 09:27:54'),
(185, 7, 'Users', 'INSERT', 7, '2026-03-11 08:21:04'),
(186, 7, 'Users', 'DELETE', 7, '2026-03-11 09:32:00'),
(187, 8, 'Users', 'INSERT', 8, '2026-03-12 07:13:01'),
(188, 9, 'Users', 'INSERT', 9, '2026-03-12 07:30:23'),
(189, 9, 'Users', 'DELETE', 9, '2026-03-12 07:51:41'),
(190, 10, 'Users', 'INSERT', 10, '2026-03-12 09:27:32'),
(191, 10, 'Users', 'DELETE', 10, '2026-03-12 09:31:03'),
(192, 11, 'Users', 'INSERT', 11, '2026-03-12 09:53:43'),
(193, 12, 'Users', 'INSERT', 12, '2026-03-12 09:54:05'),
(194, 13, 'Users', 'INSERT', 13, '2026-03-12 09:54:08'),
(195, 13, 'Users', 'DELETE', 13, '2026-03-12 10:13:46'),
(196, 14, 'Users', 'INSERT', 14, '2026-03-12 10:15:20'),
(197, 14, 'Users', 'DELETE', 14, '2026-03-12 10:18:45'),
(198, 15, 'Users', 'INSERT', 15, '2026-03-12 10:21:45'),
(199, 16, 'Users', 'INSERT', 16, '2026-03-12 10:49:53'),
(200, 17, 'Users', 'INSERT', 17, '2026-03-12 10:51:52'),
(201, 16, 'Users', 'DELETE', 16, '2026-03-12 10:56:24'),
(202, 15, 'Users', 'DELETE', 15, '2026-03-12 10:57:14'),
(203, 12, 'Users', 'DELETE', 12, '2026-03-12 11:00:55'),
(204, 18, 'Users', 'INSERT', 18, '2026-03-12 11:10:19'),
(205, 18, 'Users', 'DELETE', 18, '2026-03-12 11:10:53'),
(206, 19, 'Users', 'INSERT', 19, '2026-03-13 16:23:59'),
(207, 20, 'Users', 'INSERT', 20, '2026-03-16 06:41:15'),
(208, 20, 'Users', 'DELETE', 20, '2026-03-16 06:42:23'),
(209, 21, 'Users', 'INSERT', 21, '2026-03-17 11:43:48'),
(210, 19, 'Users', 'DELETE', 19, '2026-03-17 12:06:06'),
(211, 21, 'Users', 'DELETE', 21, '2026-03-18 07:49:10'),
(212, 22, 'Users', 'INSERT', 22, '2026-03-18 08:31:38'),
(213, 23, 'Users', 'INSERT', 23, '2026-03-19 08:02:42'),
(214, 11, 'Users', 'DELETE', 11, '2026-03-23 07:45:02');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `DefaultPictures`
--

CREATE TABLE `DefaultPictures` (
  `Id` int NOT NULL,
  `DefaultPictureUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `DefaultPictures`
--

INSERT INTO `DefaultPictures` (`Id`, `DefaultPictureUrl`) VALUES
(1, 'https://dongesz.com/images/Default_1.png'),
(2, 'https://dongesz.com/images/Default_2.png'),
(3, 'https://dongesz.com/images/Default_3.png'),
(4, 'https://dongesz.com/images/Default_4.png'),
(5, 'https://dongesz.com/images/Default_5.png'),
(6, 'https://dongesz.com/images/Default_6.png'),
(7, 'https://dongesz.com/images/Default_7.png'),
(8, 'https://dongesz.com/images/Default_8.png');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `News`
--

CREATE TABLE `News` (
  `Id` int NOT NULL,
  `Title` text NOT NULL,
  `Image` text NOT NULL,
  `Date` date NOT NULL,
  `Content` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `News`
--

INSERT INTO `News` (`Id`, `Title`, `Image`, `Date`, `Content`) VALUES
(1, 'CastL Tower Defense – v1', '42785b38-689f-4d2e-8449-cc44fc1c817d.png', '2026-03-10', 'The first stable version of our medieval‑themed tower defense game, CastL, is now available! This release already includes full user login support, so you can track your scores and stats on your personal profile. The first level is fully playable: build towers, defend the castle against incoming enemy waves, and aim for the highest score on the leaderboard!'),
(2, 'New Tower Defense Map Added!', 'dd39b4c0-8db5-46c2-a79f-bba387ebfa3e.png', '2026-03-12', 'A brand new battlefield has arrived! Defend your path against waves of enemies on this freshly designed map featuring twisting roads and strategic tower placements. Plan your defenses carefully, upgrade your towers, and stop the invaders before they reach the end. Jump in now and test your strategy on the new terrain!'),
(3, 'Defeat!', 'b80edef2-5cc0-41cc-a7b4-a2470c89f944.png', '0001-01-01', 'This time you were not able to stop the enemies, but you can always try again. Start a new game and aim for a better result!'),
(4, 'Victory!', '208c4656-f42b-4f39-81e7-313d5be735e4.png', '0001-01-01', 'The game is over, and your final score is now displayed on the board. Check your results and try to beat them in the next round!');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Scoreboard`
--

CREATE TABLE `Scoreboard` (
  `Id` int NOT NULL,
  `UserId` int NOT NULL,
  `TotalScore` int DEFAULT '0',
  `TotalKills` int DEFAULT NULL,
  `LastUpdate` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `Scoreboard`
--

INSERT INTO `Scoreboard` (`Id`, `UserId`, `TotalScore`, `TotalKills`, `LastUpdate`) VALUES
(1, 1, 8510, 707, '2026-03-18 08:29:26.896439'),
(2, 2, 625, 61, '2026-03-10 19:14:51.014574'),
(8, 8, 1325, 67, '2026-03-16 06:27:46.999533'),
(17, 17, 0, 0, '2026-03-12 10:51:52.498707'),
(22, 22, 150, 100, '2026-03-19 07:39:57.592679'),
(23, 23, 150, 100, '2026-03-19 08:17:42.880948');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Users`
--

CREATE TABLE `Users` (
  `Id` int NOT NULL,
  `Name` varchar(100) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `Email` varchar(255) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `Bio` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `DefaultPictureUrl` int DEFAULT NULL,
  `CustomPictureUrl` longtext COLLATE utf8mb4_hungarian_ci,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  `UserType` text COLLATE utf8mb4_hungarian_ci NOT NULL,
  `AuthUserId` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `Users`
--

INSERT INTO `Users` (`Id`, `Name`, `Email`, `Bio`, `DefaultPictureUrl`, `CustomPictureUrl`, `CreatedAt`, `UpdatedAt`, `UserType`, `AuthUserId`) VALUES
(1, 'Admin', 'Admin@gmail.com', 'Admin message2', 3, '467b59a3-607e-47d3-8a01-1a618060fc27.png', '2026-03-10 08:25:44.887692', '2026-03-10 09:34:40.440888', 'Admin', 'ad616545-5c5f-46a5-a301-2c0fec77c521'),
(2, 'Dongesz', 'dongesz@gmail.com', '', 1, '4024ad67-a617-49c6-ba1f-fa6f3993e40b.png', '2026-03-10 08:26:47.245959', '2026-03-23 07:51:34.785708', 'User', 'a88034b7-305f-4c5a-ade6-6f58156d9af5'),
(8, 'Unity', 'unity@gmail.com', '', 6, '4de8046a-1040-47f7-b9c0-93cab7619fed.png', '2026-03-12 07:13:01.539927', '2026-03-23 07:49:56.467517', 'Admin', '304dccf3-0084-4239-aca6-fa7f878fe108'),
(17, 'Majom', 'tothk3@kkszki.hu', '', 3, '9a9bc514-9b5e-4ff4-b1d1-bd93a5eb10fe.jpg', '2026-03-12 10:51:52.472061', '2026-03-23 07:51:06.058619', 'User', '8cb0c932-87fc-4166-bae2-f65188b0b224'),
(22, 'martiin', 'lapostyanm@kkszki.hu', '', 2, '0e208cba-2eb3-405d-91af-6a1ff55f7063.png', '2026-03-18 08:31:38.669728', '2026-03-18 08:31:38.669728', 'User', 'bfdce24e-dd5a-4062-8569-0fcfefd03347'),
(23, 'TestUser', 'lapostyanm@kkszki.hu', '', 7, NULL, '2026-03-19 08:02:42.789025', '2026-03-19 08:02:42.789025', 'User', '4b6b27bb-4e4c-4b15-b841-ba532db4c5bd');

--
-- Eseményindítók `Users`
--
DELIMITER $$
CREATE TRIGGER `user_delete` BEFORE DELETE ON `Users` FOR EACH ROW BEGIN
    INSERT INTO AuditLog (RecordId, TableName, Operation, ChangedBy) VALUES (OLD.Id, 'Users', 'DELETE', OLD.Id);
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `user_insert` AFTER INSERT ON `Users` FOR EACH ROW BEGIN
    INSERT INTO AuditLog (RecordId, TableName, Operation, ChangedBy) VALUES (NEW.Id, 'Users', 'INSERT', NEW.Id);
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `users_default_pfp` BEFORE INSERT ON `Users` FOR EACH ROW BEGIN
DECLARE pic_count INT;
    IF NEW.DefaultPictureUrl IS NULL THEN
        SELECT COUNT(*) INTO pic_count FROM DefaultPictures;

        SET NEW.DefaultPictureUrl = FLOOR(RAND() * pic_count) + 1;
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20251117180446_Initial', '9.0.10'),
('20260126072045_newmig', '8.0.23'),
('20260126072308_fullnamemig', '8.0.23'),
('20260126072918_fullnamemig2', '8.0.23'),
('20260128065503_mig1', '8.0.23'),
('20260201130809_mg1', '8.0.23'),
('20260202100000_AddAuthUserIdToUsers', '9.0.10'),
('20260202120000_DropUserTypeAndPasswordHashFromUsers', '9.0.10');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `AuditLog`
--
ALTER TABLE `AuditLog`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `RecordId` (`RecordId`);

--
-- A tábla indexei `DefaultPictures`
--
ALTER TABLE `DefaultPictures`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `News`
--
ALTER TABLE `News`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `Scoreboard`
--
ALTER TABLE `Scoreboard`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Scoreboard_UserId` (`UserId`);

--
-- A tábla indexei `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Users_AuthUserId` (`AuthUserId`),
  ADD KEY `fk_users_defaultpicture` (`DefaultPictureUrl`);

--
-- A tábla indexei `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `AuditLog`
--
ALTER TABLE `AuditLog`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=215;

--
-- AUTO_INCREMENT a táblához `DefaultPictures`
--
ALTER TABLE `DefaultPictures`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `News`
--
ALTER TABLE `News`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `Scoreboard`
--
ALTER TABLE `Scoreboard`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT a táblához `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `Scoreboard`
--
ALTER TABLE `Scoreboard`
  ADD CONSTRAINT `Scoreboard_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `Users`
--
ALTER TABLE `Users`
  ADD CONSTRAINT `fk_users_defaultpicture` FOREIGN KEY (`DefaultPictureUrl`) REFERENCES `DefaultPictures` (`Id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
