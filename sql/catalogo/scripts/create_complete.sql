USE rumoxdb;

CREATE TABLE `Categorias` (
  `Id` char(36) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  `DataHoraCriacao` datetime NOT NULL,
  `DataHoraAlteracao` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `unq_nome` (`Nome`)
);

CREATE TABLE `Produtos` (
  `Id` char(36) NOT NULL,
  `CategoriaId` char(36) NOT NULL,
  `Codigo` bigint(20) unsigned NOT NULL,
  `Descricao` varchar(200) NOT NULL,
  `InformacoesAdicionais` text NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `DataHoraCriacao` datetime NOT NULL,
  `DataHoraAlteracao` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `unq_codigo` (`Codigo`) USING BTREE,
  KEY `FK_Produtos_Categorias` (`CategoriaId`),
  CONSTRAINT `FK_Produtos_Categorias` FOREIGN KEY (`CategoriaId`) REFERENCES `Categorias` (`Id`)
);