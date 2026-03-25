-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql_container:3306
-- Létrehozás ideje: 2026. Már 25. 00:13
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
-- Adatbázis: `Auth`
--
CREATE DATABASE IF NOT EXISTS `Auth` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `Auth`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `AspNetRoles`
--

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('aa80e9f4-fbf0-4009-8694-a5cb94f7225e', 'Admin', 'ADMIN', NULL),
('b76846bf-5fe0-4496-9916-9ad5f8d6c203', 'User', 'USER', NULL);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `AspNetUserRoles`
--

INSERT INTO `AspNetUserRoles` (`UserId`, `RoleId`) VALUES
('304dccf3-0084-4239-aca6-fa7f878fe108', 'aa80e9f4-fbf0-4009-8694-a5cb94f7225e'),
('a88034b7-305f-4c5a-ade6-6f58156d9af5', 'aa80e9f4-fbf0-4009-8694-a5cb94f7225e'),
('ad616545-5c5f-46a5-a301-2c0fec77c521', 'aa80e9f4-fbf0-4009-8694-a5cb94f7225e');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `FullName` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `FullName`) VALUES
('304dccf3-0084-4239-aca6-fa7f878fe108', 'Unity', 'UNITY', 'unity@gmail.com', 'UNITY@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAECI3sn6hpnsEXVhsSAvXJEaDyos9UaqP+u/cVWqt5s5T3+KCosEKuK63l1DfzyKPAw==', '7LIJMIKZAZU6AE6A4CEMOFOSGRDFGB2I', 'db08f98b-f5fd-480c-9b31-42739646bb5a', NULL, 0, 0, NULL, 1, 0, 'Unity'),
('4b6b27bb-4e4c-4b15-b841-ba532db4c5bd', 'TestUser', 'TESTUSER', 'lapostyanm@kkszki.hu', 'LAPOSTYANM@KKSZKI.HU', 0, 'AQAAAAIAAYagAAAAEMjza0M5YVISFu0F1NYyTO5H94F1pAZ+sA2sl3hpSE3C2DpAE7esxT5nxT00+792IQ==', 'IPKMALFFAMSEB4HQNDQBAD37R6CILF4O', '10c04586-179f-4c32-8924-4eb30f2c8f0c', NULL, 0, 0, NULL, 1, 0, 'TestUser'),
('8cb0c932-87fc-4166-bae2-f65188b0b224', 'Majom', 'MAJOM', 'tothk3@kkszki.hu', 'TOTHK3@KKSZKI.HU', 0, 'AQAAAAIAAYagAAAAED1MVyIMXgvchFtj8KIPxXi0QX1MWE3ZAVb8cKvZMcU9E8NyJDjB7xS+xhxXFvE3ww==', 'AOSQ7X7IXMLIBTLA76RISQ6Z44RHI2CL', '65834d74-a748-4f9f-a931-31c1c5312e6f', NULL, 0, 0, NULL, 1, 0, 'Majom'),
('a88034b7-305f-4c5a-ade6-6f58156d9af5', 'Dongesz', 'DONGESZ', 'dongesz@gmail.com', 'DONGESZ@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAECaIEVJ6TgWO27zBHihtz3MWKHEhbGhsyAYBx7OtFYEESVo98h8W+mX5Qj/dBAjQjA==', 'EVEIEEHA2QKLEAG5AHBKMASGJAL66NMJ', 'd30af51c-6026-458f-9674-b406e40c6e06', NULL, 0, 0, NULL, 1, 0, 'Majoros Mate Martin'),
('ad616545-5c5f-46a5-a301-2c0fec77c521', 'Admin', 'ADMIN', 'Admin@gmail.com', 'ADMIN@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAECuipsX5GfGRGGNSc274WqttBnq4B03b5agT70zGWK/DdkiL0EIKR61W8UOX+D7AcA==', '7747F6JRG3DN3XC34N32LIKUDFRQYJRE', '43c928c7-de32-4667-b263-d6245a4e74f1', NULL, 0, 0, NULL, 1, 0, 'Admin'),
('bfdce24e-dd5a-4062-8569-0fcfefd03347', 'martiin', 'MARTIIN', 'lapostyanm@kkszki.hu', 'LAPOSTYANM@KKSZKI.HU', 0, 'AQAAAAIAAYagAAAAENErIgi6RJtv7VVH27M/amqymZ68qdTG+pPktZYW/frUFMx6Us12hrFdgkCI45KIGA==', '4OXKX7VSZAQJ5KPE3QS7AD2DWR5E2GFI', '29c4376d-7b7c-460f-927c-9e996d6c63fe', NULL, 0, 0, NULL, 1, 0, 'martiin');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
('20260126072045_newmig', '8.0.23'),
('20260126072308_fullnamemig', '8.0.23'),
('20260126072918_fullnamemig2', '8.0.23'),
('20260128065503_mig1', '8.0.23'),
('20260201130809_mg1', '8.0.23'),
('20260201131253_mg2', '8.0.23');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- A tábla indexei `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- A tábla indexei `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- A tábla indexei `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- A tábla indexei `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- A tábla indexei `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- A tábla indexei `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- A tábla indexei `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
