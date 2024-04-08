-- create database
 
CREATE DATABASE 
	eWallet_yashl;
    
-- user table 

CREATE TABLE `ewallet_yashl`.`usr01` (
  `r01f01` INT NOT NULL COMMENT 'user id - primary key',
  `r01f02` INT NOT NULL DEFAULT 101 COMMENT 'wallet id - unique ',
  `r01f03` VARCHAR(255) NULL COMMENT 'password_hash',
  `r01f04` VARCHAR(45) NULL COMMENT 'email',
  `r01f05` VARCHAR(45) NULL COMMENT 'first name',
  `r01f06` VARCHAR(45) NULL COMMENT 'last name',
  `r01f07` VARCHAR(10) NULL COMMENT 'Mobile Number',
  `r01f08` DATETIME NULL COMMENT 'created on',
  `r01f09` DATETIME NULL COMMENT 'updated on',
  `r01f10` BOOLEAN NULL COMMENT 'is deleted',
  `r01f11` DATETIME NULL COMMENT 'deletion time',
  UNIQUE INDEX `r01f02_UNIQUE` (`r01f02` ASC) VISIBLE,
  PRIMARY KEY (`r01f01`))
COMMENT = 'User Table	';

-- wallet table

CREATE TABLE `wlt01` (
  `t01f01` int NOT NULL COMMENT 'wallet id ',
  `t01f02` int DEFAULT NULL COMMENT 'user id',
  `t01f03` decimal(10,2) DEFAULT NULL COMMENT 'current balance',
  `t01f04` enum('INR','YEN','USD') DEFAULT NULL COMMENT 'currency',
  `t01f05` datetime DEFAULT NULL COMMENT 'Created on',
  `t01f06` datetime DEFAULT NULL COMMENT 'updated on',
  PRIMARY KEY (`t01f01`)
) COMMENT='Wallet Table';

-- transaction table

CREATE TABLE `tsn01` (
  `n01f01` int NOT NULL DEFAULT '1001' COMMENT 'transaction id',
  `n01f02` int DEFAULT NULL COMMENT 'wallet id',
  `n01f03` int DEFAULT NULL COMMENT 'from user id',
  `n01f04` int DEFAULT NULL COMMENT 'to user id ',
  `n01f05` decimal(10,2) DEFAULT NULL COMMENT 'amount',
  `n01f06` enum("Deposit","Transfer", "Withdraw") DEFAULT NULL COMMENT 'transaction type',
  `n01f07` decimal(10,2) DEFAULT NULL COMMENT 'transaction fee',
  `n01f08` varchar(45) DEFAULT NULL COMMENT 'description of transaction',
  `n01f09` datetime DEFAULT NULL COMMENT 'created on ',
  PRIMARY KEY (`n01f01`)
) COMMENT='TransactionTable';

-- notification 

CREATE TABLE `ewallet_yashl`.`not01` (
  `t01f01` INT NOT NULL COMMENT 'notification id',
  `t01f02` INT NULL COMMENT 'user id',
  `t01f03` VARCHAR(255) NULL COMMENT 'notification',
  `t01f04` DATETIME NULL COMMENT 'created on ',
  PRIMARY KEY (`t01f01`))
COMMENT = 'Notification Table';

-- account 

CREATE TABLE `acc01` (
  `c01f01` int NOT NULL COMMENT 'account number',
  `c01f02` int DEFAULT NULL COMMENT 'user id',
  `c01f03` decimal(10,2) DEFAULT NULL COMMENT 'current balance',
  `c01f04` enum('INR','YEN','USD') DEFAULT NULL COMMENT 'currency',
  `c01f05` datetime DEFAULT NULL COMMENT 'created on',
  `c01f06` datetime DEFAULT NULL COMMENT 'updated on',
  PRIMARY KEY (`c01f01`)
) COMMENT='Account Table'





