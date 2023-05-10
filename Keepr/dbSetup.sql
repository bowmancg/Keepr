-- Active: 1682460182546@@SG-codeworks-7498-mysql-master.servers.mongodirector.com@3306@sandbox
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  coverImg VARCHAR(255) COMMENT 'User cover image',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

ALTER TABLE accounts ADD coverImg VARCHAR(255);

SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId;

CREATE TABLE 
keeps(
id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
name VARCHAR(50) NOT NULL,
description VARCHAR(1000) NOT NULL,
img VARCHAR(255) NOT NULL,
views INT NOT NULL,
kept INT NOT NULL,
creatorId VARCHAR(255) NOT NULL,
FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT charset utf8mb4 COMMENT '';

DELETE FROM accounts WHERE id = '';

ALTER TABLE keeps MODIFY description VARCHAR(1000);

INSERT INTO
keeps (
  name,
  description,
  img,
  views,
  kept,
  creatorId
)
VALUES (
  'Pin Image',
  'One of the images I will keep in a collection.',
  'https://www.thespruceeats.com/thmb/P9OEyJww2o6b8_GMjQALTU6VY78=/450x300/filters:no_upscale():max_bytes(150000):strip_icc()/pasta-carbonara-recipe-5210168-hero-01-80090e56abc04ca19d88ebf7fad1d157.jpg',
  0,
  0,
  '64502070214f5e78470d10c8'
);

SELECT
k.*, a.*
FROM keeps k
JOIN accounts a ON a.id = k.creatorId
WHERE k.id = 81;

CREATE TABLE
vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(50) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(255) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT FALSE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT charset utf8mb4 COMMENT '';

DELETE FROM accounts WHERE id = '';

INSERT INTO
vaults(
  creatorId,
  name,
  description,
  img,
  isPrivate
)
VALUES (
  '64502070214f5e78470d10c8',
  'Vault No. 1',
  'This is a secure vault.',
  'https://www.jewelersmutual.com/sites/default/files/2021-08/different-types-of-vaults-blog.png',
  false
);

SELECT
v.*, a.*
FROM vaults v
JOIN accounts a ON a.id = v.creatorId
WHERE v.id = 61;

CREATE TABLE
vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) DEFAULT charset utf8mb4 COMMENT '';


DELETE FROM accounts WHERE id = '';

SELECT
vk.*,
vk.id vaultKeepId,
a.*
FROM vaultKeeps vk
JOIN accounts a ON a.id = vk.creatorId;

SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON v.creatorId = a.id;
    -- WHERE v.creatorId = @creatorId;