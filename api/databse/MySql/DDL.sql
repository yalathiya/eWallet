-- create database
 
CREATE DATABASE 
	eWallet_yashl;
    
-- user table 

CREATE TABLE `usr01` (
  `r01f01` int NOT NULL AUTO_INCREMENT COMMENT 'user id - primary key',
  `r01f03` varchar(255) DEFAULT NULL COMMENT 'password_hash',
  `r01f04` varchar(45) DEFAULT NULL COMMENT 'email',
  `r01f05` varchar(45) DEFAULT NULL COMMENT 'first name',
  `r01f06` varchar(45) DEFAULT NULL COMMENT 'last name',
  `r01f07` varchar(10) DEFAULT NULL COMMENT 'Mobile Number',
  `r01f08` datetime DEFAULT NULL COMMENT 'created on',
  `r01f09` datetime DEFAULT NULL COMMENT 'updated on',
  PRIMARY KEY (`r01f01`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='User Table	';

-- wallet table

CREATE TABLE `wlt01` (
  `t01f01` int NOT NULL AUTO_INCREMENT COMMENT 'wallet id ',
  `t01f02` int DEFAULT NULL COMMENT 'user id',
  `t01f03` decimal(10,2) DEFAULT NULL COMMENT 'current balance',
  `t01f04` varchar(3) DEFAULT NULL COMMENT 'currency',
  `t01f05` datetime DEFAULT NULL COMMENT 'Created on',
  `t01f06` datetime DEFAULT NULL COMMENT 'updated on',
  PRIMARY KEY (`t01f01`),
  KEY `t01f02_idx` (`t01f02`),
  CONSTRAINT `t01f02` FOREIGN KEY (`t01f02`) REFERENCES `usr01` (`r01f01`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Wallet Table';

-- transaction table

CREATE TABLE `tsn01` (
  `n01f01` int NOT NULL AUTO_INCREMENT COMMENT 'transaction id',
  `n01f02` int DEFAULT NULL COMMENT 'wallet id',
  `n01f03` int DEFAULT NULL COMMENT 'from user id',
  `n01f04` int DEFAULT NULL COMMENT 'to user id ',
  `n01f05` decimal(10,2) DEFAULT NULL COMMENT 'amount',
  `n01f06` varchar(1) DEFAULT NULL COMMENT 'transaction type',
  `n01f07` decimal(10,2) DEFAULT NULL COMMENT 'transaction fee',
  `n01f08` varchar(45) DEFAULT NULL COMMENT 'description of transaction',
  `n01f09` datetime DEFAULT NULL COMMENT 'created on ',
  `n01f10` decimal(10,2) DEFAULT NULL COMMENT 'Total Amount ',
  PRIMARY KEY (`n01f01`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='TransactionTable';

-- notification 

CREATE TABLE `not01` (
  `t01f01` int NOT NULL AUTO_INCREMENT COMMENT 'notification id',
  `t01f02` int DEFAULT NULL COMMENT 'user id',
  `t01f03` varchar(255) DEFAULT NULL COMMENT 'notification',
  `t01f04` tinyint DEFAULT NULL COMMENT 'is email notification',
  `t01f05` tinyint DEFAULT NULL COMMENT 'is sms notification',
  `t01f06` datetime DEFAULT NULL COMMENT 'created on',
  PRIMARY KEY (`t01f01`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Notification Table';

-- account 

CREATE TABLE `acc01` (
  `c01f01` int NOT NULL AUTO_INCREMENT COMMENT 'account number',
  `c01f02` int DEFAULT NULL COMMENT 'user id',
  `c01f03` decimal(10,2) DEFAULT NULL COMMENT 'current balance',
  `c01f04` enum('INR','YEN','USD') DEFAULT NULL COMMENT 'currency',
  `c01f05` datetime DEFAULT NULL COMMENT 'created on',
  `c01f06` datetime DEFAULT NULL COMMENT 'updated on',
  PRIMARY KEY (`c01f01`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Account Table';

-- deactivated accounts

CREATE TABLE `dac02` (
  `c02f01` int NOT NULL COMMENT 'user id',
  UNIQUE KEY `c02f01_UNIQUE` (`c02f01`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Deactivated Accounts ';

-- razorpay payments table

CREATE TABLE `raz01` (
  `z01f01` varchar(50) NOT NULL COMMENT 'Razorpay Payment Id',
  `z01f02` varchar(50) DEFAULT NULL COMMENT 'Razorpay Order Id ',
  `z01f03` varchar(45) DEFAULT NULL COMMENT 'Razorpay Signature',
  `z01f04` decimal(10,0) DEFAULT NULL COMMENT 'Amount',
  `z01f05` int DEFAULT NULL COMMENT 'Wallet Id ',
  `z01f06` varchar(45) DEFAULT NULL COMMENT 'Razorpay Status',
  `z01f07` tinyint DEFAULT NULL COMMENT 'is reflected in wallet table',
  PRIMARY KEY (`z01f01`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Razorpay Payments '

