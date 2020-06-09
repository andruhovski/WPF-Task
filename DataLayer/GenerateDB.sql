SET GLOBAL max_allowed_packet = 16777216;
CREATE TABLE `demodatabase`.`Cards` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Parent` INT NOT NULL,
  `Header` NVARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`));
  
  CREATE TABLE `demodatabase`.`CardResources` (
    `Id`      INT AUTO_INCREMENT  NOT NULL,
    `Kind`    INT NOT NULL,
    `Card_Id` INT NULL,
    CONSTRAINT `PK_CardResources` PRIMARY KEY (`Id` ASC),
    CONSTRAINT `FK_CardResources_Card_Id` FOREIGN KEY (`Card_Id`) REFERENCES Cards (`Id`)
);

CREATE INDEX `IX_Card_Id`
    ON CardResources(`Card_Id` ASC);
    
CREATE TABLE ImageResource (
    `Id`          INT             NOT NULL,
    `Image`       LONGBLOB  NULL,
    `Description` LONGTEXT   NULL,
    CONSTRAINT `PK_ImageResource` PRIMARY KEY (`Id` ASC),
    CONSTRAINT `FK_ImageResource_CardResources_Id` FOREIGN KEY (`Id`) REFERENCES CardResources (`Id`)
);

CREATE  INDEX `IX_Id`
    ON ImageResource(`Id` ASC);    
    
CREATE TABLE StringTable (
    `Id` INT NOT NULL,
    CONSTRAINT `PK_StringTable` PRIMARY KEY (`Id` ASC),
    CONSTRAINT `FK_StringTable_CardResources_Id` FOREIGN KEY (`Id`) REFERENCES CardResources (`Id`)
);


CREATE INDEX `IX_Id`
    ON StringTable(`Id` ASC);
    
CREATE TABLE `Rows` (
    `Id`                     INT            AUTO_INCREMENT  NOT NULL,
    `Title`                  LONGTEXT  NULL,
    `StringTableResource_Id` INT            NULL,
    CONSTRAINT `PK_Rows` PRIMARY KEY (`Id` ASC),
    CONSTRAINT `FK_Rows_StringTable_StringTableResource_Id` FOREIGN KEY (`StringTableResource_Id`) REFERENCES StringTable (`Id`)
);


CREATE  INDEX `IX_StringTableResource_Id`
    ON `Rows`(`StringTableResource_Id` ASC);
    