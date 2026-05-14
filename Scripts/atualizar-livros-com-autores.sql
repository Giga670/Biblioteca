-- Executa se já tiveres a tabela Livros antiga (sem Autores / sem IdAutor).
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

SET @col_exists := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Livros' AND COLUMN_NAME = 'IdAutor'
);
SET @sql := IF(@col_exists = 0,
  'ALTER TABLE Livros ADD COLUMN IdAutor int NULL',
  'SELECT 1');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @idx_exists := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS
  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Livros' AND INDEX_NAME = 'IX_Livros_IdAutor'
);
SET @sql2 := IF(@idx_exists = 0,
  'CREATE INDEX IX_Livros_IdAutor ON Livros (IdAutor)',
  'SELECT 1');
PREPARE stmt2 FROM @sql2;
EXECUTE stmt2;
DEALLOCATE PREPARE stmt2;

SET @fk_exists := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
  WHERE CONSTRAINT_SCHEMA = DATABASE()
    AND TABLE_NAME = 'Livros'
    AND CONSTRAINT_NAME = 'FK_Livros_Autores_IdAutor'
);
SET @sql3 := IF(@fk_exists = 0,
  'ALTER TABLE Livros ADD CONSTRAINT FK_Livros_Autores_IdAutor FOREIGN KEY (IdAutor) REFERENCES Autores (Id) ON DELETE SET NULL',
  'SELECT 1');
PREPARE stmt3 FROM @sql3;
EXECUTE stmt3;
DEALLOCATE PREPARE stmt3;
