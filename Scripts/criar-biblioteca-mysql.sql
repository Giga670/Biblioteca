-- Esquema completo (instalação nova). Ajusta o nome da base se alterares o appsettings.json.

CREATE DATABASE IF NOT EXISTS bibliotecadb
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE bibliotecadb;

CREATE TABLE IF NOT EXISTS Autores (
  Id int NOT NULL AUTO_INCREMENT,
  Nome longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Nacionalidade longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Periodo longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Imagem longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Biografia longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Obras longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  CONSTRAINT PK_Autores PRIMARY KEY (Id)
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS Livros (
  Id int NOT NULL AUTO_INCREMENT,
  Titulo longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Imagem longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Autor longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  QtdPaginas int NOT NULL,
  DataPublicacao int NOT NULL,
  Genero longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Resumo longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  IdAutor int NULL,
  CONSTRAINT PK_Livros PRIMARY KEY (Id),
  KEY IX_Livros_IdAutor (IdAutor),
  CONSTRAINT FK_Livros_Autores_IdAutor FOREIGN KEY (IdAutor) REFERENCES Autores (Id) ON DELETE SET NULL
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
