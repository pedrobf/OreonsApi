-- --------------------------------------------------------
-- Servidor:                     localhost
-- Versão do servidor:           8.0.13 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Copiando estrutura do banco de dados para oreons
CREATE DATABASE IF NOT EXISTS `oreons` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `oreons`;

-- Copiando estrutura para tabela oreons.category
CREATE TABLE IF NOT EXISTS `category` (
  `id` varchar(50) NOT NULL,
  `description` varchar(70) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela oreons.images
CREATE TABLE IF NOT EXISTS `images` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `image` bit(64) NOT NULL,
  `id_product` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_product` (`id_product`),
  CONSTRAINT `FK1_id_product` FOREIGN KEY (`id_product`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela oreons.products
CREATE TABLE IF NOT EXISTS `products` (
  `id` varchar(50) NOT NULL,
  `name` varchar(70) NOT NULL,
  `description` varchar(100) NOT NULL,
  `price` decimal(10,2) NOT NULL DEFAULT '0.00',
  `id_category` varchar(50) NOT NULL,
  `id_subcategory` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela oreons.subcategory
CREATE TABLE IF NOT EXISTS `subcategory` (
  `id` varchar(50) NOT NULL,
  `description` varchar(70) NOT NULL,
  `level` int(2) DEFAULT '0',
  `id_category` varchar(50) DEFAULT NULL,
  KEY `id_category` (`id_category`),
  KEY `id` (`id`),
  CONSTRAINT `id_category` FOREIGN KEY (`id_category`) REFERENCES `category` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
